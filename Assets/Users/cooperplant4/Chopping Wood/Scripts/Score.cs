namespace cooperplant4
{
    public readonly struct Score
    {
        public readonly int points;
        public readonly string reason;

        public Score(int points, string reason)
        {
            this.points = points;
            this.reason = reason;
        }

        public static readonly Score None = new Score(0, "");

        public override string ToString()
        {
            return $"{points} points for reason: {reason}";
        }
    }
}
