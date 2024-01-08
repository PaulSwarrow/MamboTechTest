namespace StarterAssets.Game.Data
{
    public struct ObjectStat
    {
        public int Value;
        public int MaxValue;

        public ObjectStat(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Provides amount of missing points
        /// </summary>
        public int Delta => MaxValue - Value;
    }
}