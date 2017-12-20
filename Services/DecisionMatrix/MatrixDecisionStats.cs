using System.Collections.Generic;
using System.Linq;

namespace CFKC.VPV.Services.DecisionMatrix
{

    // Brute force first implementation see notes on MatrixResolver

    public class MatrixDecisionStats
    {
        public int ParcelId { get; set; }
        public IEnumerable<string> BestUses { get; }
        public IEnumerable<UsageScore> UsageScores { get; }
        public IEnumerable<QuestionAnswer> ConcreteAnswers { get; }

        public MatrixDecisionStats(int parcelId, 
                                   IEnumerable<string> bestUses, 
                                   Dictionary<string, int> useScores, 
                                   Dictionary<string, bool> concreteAnswers)
        {
            ParcelId = parcelId;

            BestUses = bestUses;

            UsageScores = useScores.Select(x => new UsageScore(x.Key, x.Value)).ToArray();

            ConcreteAnswers = concreteAnswers.Select(q => new QuestionAnswer(q.Key, q.Value)).ToArray();
        }
        
    }

    public class QuestionAnswer
    {
        public string Question { get; }

        public string Answer { get; }

        public QuestionAnswer(string question, bool answer)
        {
            Question = question;
            Answer = answer ? "Yes" : "No";
        }
    }

    public class UsageScore
    {
        public string Use { get; }

        public int Score { get; }

        public UsageScore(string use, int score)
        {
            Use = use;

            Score = score;
        }
    }
}