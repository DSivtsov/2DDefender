using System;
using UnityEngine;

namespace GamePlay.Enemy
{
    internal sealed class EnemyAttackController : MonoBehaviour, IEnemyFixUpdateListeners
    {
        internal event Action<GameObject, GameObject> OnHitWall;

        private Collider2D _colliderAttacker;
        private Collider2D _wallCollider;
        private GameObject _target;

        internal void SetTarget(GameObject target)
        {
            _target = target;
            _wallCollider = _target.GetComponent<CompositeCollider2D>();
        }

        private void Awake()
        {
            _colliderAttacker = GetComponent<Collider2D>();
        }

        void IEnemyFixUpdateListeners.OnFixedUpdate(float _) => EnemyHitWallController();

        private void EnemyHitWallController()
        {
            if (_colliderAttacker.IsTouching(_wallCollider))
            {
                OnHitWall?.Invoke(_target, gameObject);
            }
        }
    } 
}
