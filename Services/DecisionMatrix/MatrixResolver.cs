using CFKC.VPV.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CFKC.VPV.Services.DecisionMatrix
{

    // First iteration very brute force not alot of effort was put into this first
    // implementation because there are still many missing use-cases or business requirements
    // as well as incomplete data
    
    public class MatrixResolver
    {
        
        public Dictionary<string, bool> Answers { get { return _answers; } }

        public Dictionary<string, int> Uses { get { return _uses; } }

        private MatrixResolverOptions _options = new MatrixResolverOptions();

        private int _parcelId;

        private Dictionary<string, bool> _answers;

        private Dictionary<string, int> _uses = new Dictionary<string, int>
        {
            {"Green infrastructure", 0 },
            {"Side lot", 0 },
            {"Redevelopment", 0 },
            {"Pocket park", 0 },
            {"Hold", 0 }
        };

        private Dictionary<string, BinaryUsePath> _criteria = new Dictionary<string, BinaryUsePath>
        {
                {
                    "Adjacent to residential?",
                    new BinaryUsePath(trueUses: new[] { "Side lot", "Pocket park" },
                                      falseUses: new[]{ "Hold", "Green infrastructure" })
                },
                {
                    "Adjacent to vacant?",
                    new BinaryUsePath(trueUses: new[] { "Hold", "Green infrastructure" },
                                      falseUses: new[]{ "Side lot", "Pocket park" })
                },
                {
                    "Has soil contamination?",
                    new BinaryUsePath(trueUses: new[] { "Green infrastructure" },
                                      falseUses: new[]{ "Hold", "Pocket park", "Side lot", "Redevelopment" })
                },
                {
                    "In a floodplain?",
                    new BinaryUsePath(trueUses: new[] { "Green infrastructure" },
                                      falseUses: new[]{ "Hold", "Pocket park", "Side lot", "Redevelopment" })
                },
                {
                    "Larger than 1000sqft?",
                    new BinaryUsePath(trueUses: new[] { "Side lot", "Hold" },
                                      falseUses: new[]{ "Redevelopment", "Pocket park", "Green infrastructure" })
                },
                {
                    "Park within half mile?",
                    new BinaryUsePath(trueUses: new[] { "Green infrastructure", "Side lot", "Redevelopment", "Hold" },
                                      falseUses: new[]{ "Pocket park" })
                }
        };

        public void AddMatrixData(MatrixAnswers answers)
        {
            _parcelId = answers.ParcelId;

            _answers = new Dictionary<string, bool>
            {
                { "Adjacent to residential?", answers.AdjacentToResidential == 1 },
                { "Adjacent to vacant?", answers.AdjacentToVacant == 1 },
                { "Has soil contamination?", answers.HasSoilContamination == 1 },
                { "In a floodplain?", answers.InFloodplain == 1 },
                { "Larger than 1000sqft?", answers.LargerThan1000Sqft == 1 },
                { "Park within half mile?", answers.ParkWithinHalfMile == 1 }
            };
        }

        public MatrixDecisionStats ComputeBestUse(Action<MatrixResolverOptions> optionsAction = null)
        {

            optionsAction?.Invoke(_options);

            foreach (var a in _answers)
            {
                var question = a.Key;

                var answer = a.Value;

                var path = _criteria[question];

                ScorePath(path, answer);
            }
            
            var bestUses = Uses.Where(x => x.Value == Uses.Values.Max())
                              .Select(v => v.Key);

            return new MatrixDecisionStats(_parcelId, bestUses, Uses, Answers);
        }
        
        public void ScorePath(BinaryUsePath usePath, bool concreteAnswer)
        {
            if (concreteAnswer)
            {
                IncrementScore(usePath.TrueUses);
                DecrementScore(usePath.FalseUses);
            }
            else
            {
                IncrementScore(usePath.FalseUses);
                DecrementScore(usePath.TrueUses);
            }
        }

        private void IncrementScore(string[] usePath)
        {
            foreach (var use in usePath)
            {
                _uses[use]++;
            }
        }

        private void DecrementScore(string[] usePath)
        {
            if (_options.DecrementalComputations)
            {
                foreach (var use in usePath)
                {
                    _uses[use]--;
                }
            }
        }

    }

    public class BinaryUsePath
    {
        public string[] TrueUses { get; }

        public string[] FalseUses { get; }

        public BinaryUsePath(string[] trueUses, string[] falseUses)
        {
            TrueUses = trueUses;

            FalseUses = falseUses;
        }
        
        
    }
}
