using UnityEngine;
using GamePlay.Common;

namespace GamePlay.Enemy
{
    internal sealed class EnemyMoveController : MonoBehaviour, IEnemyFixUpdateListeners
    {
        internal bool IsReached { get; private set; } = false;

        private Moveable _enemyMoveable;

        private Vector2 _destination;

        private void Awake()
        {
            _enemyMoveable = GetComponent<Moveable>();
            if (!_enemyMoveable)
                throw new System.NotSupportedException("Not Found Moveable");
        }

        internal void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        void IEnemyFixUpdateListeners.OnFixedUpdate(float fixedDeltaTime)
        {
            MoveEnemyRigidbody(fixedDeltaTime);
        }

        private void MoveEnemyRigidbody(float fixedDeltaTime)
        {
            if (!IsReached)
            {
                Vector2 vector = _destination - (Vector2) transform.position;
                if (vector.magnitude <= 0.25f)
                {
                    IsReached = true;
                    return;
                }
                Vector2 direction = vector.normalized * fixedDeltaTime;
                _enemyMoveable.MoveRigidbody(direction);
            }
        }
    }
}
