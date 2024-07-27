using System;
using UnityEngine;

namespace GameEngine.Common
{
    public interface IDamageable
    {
        event Action<GameObject> OnDied;
        bool IsNotDied { get; }
        void TakeDamage(int damage);
    }
}