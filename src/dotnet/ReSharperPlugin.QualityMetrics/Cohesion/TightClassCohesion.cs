namespace ReSharperPlugin.QualityMetrics.Cohesion
{
    public class TightClassCohesion
    {
        private const int ErrorThreshold = 50;
        private const int WarningThreshold = 80;

        public int NumberOfDirectConnections { get; private set; }
        public int NumberOfPossibleConnections { get; private set; }
        public int PercentageValue => (int) (100 * Value);
        public double Value => NumberOfDirectConnections / (double) NumberOfPossibleConnections;
        public bool IsError => PercentageValue < ErrorThreshold;
        public bool IsWarning => PercentageValue >= ErrorThreshold && PercentageValue < WarningThreshold;
        public bool IsOk => PercentageValue >= WarningThreshold;

        public static TightClassCohesion Create(int nbDirectConnections, int nbPossibleConnections)
        {
            return new TightClassCohesion
            {
                NumberOfDirectConnections = nbDirectConnections,
                NumberOfPossibleConnections = nbPossibleConnections
            };
        }
    }
}
