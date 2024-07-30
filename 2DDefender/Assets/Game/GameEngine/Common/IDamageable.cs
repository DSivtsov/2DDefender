using System;
using UnityEngine;

namespace GameEngine.Common
{
    public interface IDamageable
    {
        event Action<GameObject> OnDied;
        event Action<int> OnChangedHitPoints;
        bool IsNotDied { get; }
        int HitPoints { get; }
        void TakeDamage(int damage);
        void SetInitialHitPoints(int initialHitPoints);
    }
}