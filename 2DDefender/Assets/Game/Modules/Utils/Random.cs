using UnityEngine;

namespace Modules.Utils
{
    public class Random
    {
        private readonly System.Random _random;

        public Random(int selectedSeed)
        {
            if (selectedSeed == 0)
            {
                _random = new System.Random();
                int newSeed = _random.Next();
                Debug.Log($"INFO: Will use NEWSeed={newSeed}");
                _random = new System.Random(newSeed);
            }
            else
            {
                Debug.Log($"INFO: Will use _selectedSeed={selectedSeed}");
                _random = new System.Random(selectedSeed);
            }
        }

        public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

        public float NextSingle() => (float)_random.NextDouble();
    }
}
