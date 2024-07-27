using System;
using GameEngine.Common;
using UnityEngine;

namespace GamePlay.Common
{
    public sealed class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<GameObject> OnDied;
        
        [SerializeField] private int _hitPoints;

        public bool IsNotDied => _hitPoints > 0;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0) OnDied?.Invoke(this.gameObject);
        }
    }
}