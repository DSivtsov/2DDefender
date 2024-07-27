using System;
using UnityEngine;
using GameEngine.Common;
using GamePlay.Common;

namespace GamePlay.Enemy
{
    internal sealed class EnemyAttackController : MonoBehaviour, IEnemyFixUpdateListeners
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private EnemyMoveController enemyMoveAgent;
        [SerializeField] private float _countdown;

        private GameObject _target;
        private float _currentTime;
        internal event Action<Vector2, Vector2> OnFire;

        internal void SetTarget(GameObject target) => _target = target;

        private void Awake()
        {
            _currentTime = _countdown;
        }

        void IEnemyFixUpdateListeners.OnFixedUpdate(float fixedDeltaTime) => EnemyFireController(fixedDeltaTime);

        private void EnemyFireController(float fixedDeltaTime)
        {
            if (enemyMoveAgent.IsReached)
            {
                if (_target.GetComponent<IDamageable>().IsNotDied)
                {
                    _currentTime -= fixedDeltaTime;
                    if (!PassCountDown) return;
                    Fire();
                    _currentTime += _countdown;
                }
            }
        }

        private bool PassCountDown => _currentTime <= 0;

        private void Fire()
        {
            Vector2 startPosition = _weapon.Position;
            Vector2 vectorVelocityNorm = ((Vector2)_target.transform.position - startPosition).normalized;
            OnFire?.Invoke(startPosition, vectorVelocityNorm);
        }
    } 
}
