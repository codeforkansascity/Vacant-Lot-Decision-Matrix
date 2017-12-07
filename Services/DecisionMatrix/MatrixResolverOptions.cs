namespace CFKC.VPV.Services.DecisionMatrix
{

    /// <summary>
    /// Matrix resolver configuration options object
    /// </summary>
    public class MatrixResolverOptions
    {
        private bool _decrementalComputations;

        public bool DecrementalComputations { get { return _decrementalComputations; } }

        public void UseDecrementalComputations()
        {
            _decrementalComputations = true;
        }
    }
}