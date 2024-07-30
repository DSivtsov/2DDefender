using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemyRandomSpeed 
    {
        [MinMaxSlider(1, 5, true)]
        [SerializeField] private Vector2 _speedMinMax = new Vector2(1,2);

        internal float GetRandomSpeed() => Random.Range(_speedMinMax.x,_speedMinMax.y);
    }
}