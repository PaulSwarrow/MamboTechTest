namespace Game.Data
{
    public struct ObjectStatValue
    {
        public int Current;
        public int Max;

        public ObjectStatValue(int current, int max)
        {
            Current = current;
            Max = max;
        }

        /// <summary>
        /// Provides amount of missing points
        /// </summary>
        public int Delta => Max - Current;
    }
}