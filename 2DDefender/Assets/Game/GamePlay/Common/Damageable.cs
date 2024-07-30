using System;
using GameEngine.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Common
{
    public sealed class Damageable : MonoBehaviour, IDamageable
    {
        public event Action<GameObject> OnDied;
        public event Action<int> OnChangedHitPoints;

        [SerializeField] private int _hitPoints;

        public int HitPoints => _hitPoints;
        
        public bool IsNotDied => _hitPoints > 0;

        [Button]
        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                _hitPoints = 0;
                OnDied?.Invoke(this.gameObject);
            }
            OnChangedHitPoints?.Invoke(_hitPoints);
        }

        void IDamageable.SetInitialHitPoints(int initialHitPoints)
        {
            if (initialHitPoints > 0)
            {
                _hitPoints = initialHitPoints;
                OnChangedHitPoints?.Invoke(_hitPoints);
            }
            else
                throw new NotSupportedException("[Damageable] Not Suppored initialHitPoints <= 0");
        }
    }
}