using System;

namespace DP.Models
{
    [Serializable]
    public struct Gold
    {
        public Gold(int value)
        {
            Value = value;
        }

        public int Value;
    }
}
