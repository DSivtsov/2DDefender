using UnityEngine;
using Sirenix.OdinInspector;

namespace GamePlay.Enemy
{
    internal sealed class EnemyMoveController : MonoBehaviour, IEnemyFixUpdateListeners
    {
        [ShowInInspector,ReadOnly] private float _speed = 2f;
        private Rigidbody2D _rigidbodyObj;

        public float EnemySpeed => _speed;
        
        private void Awake()
        {
            _rigidbodyObj = GetComponent<Rigidbody2D>();
        }

        private Vector2 _destination;

        void IEnemyFixUpdateListeners.OnFixedUpdate(float fixedDeltaTime)
        {
            MoveEnemyRigidbody(fixedDeltaTime);
        }

        private void MoveEnemyRigidbody(float fixedDeltaTime)
        {
            Vector2 newPosition = _rigidbodyObj.position + fixedDeltaTime * _speed * Vector2.down;
            _rigidbodyObj.MovePosition(newPosition);
        }

        public void SetEnemySpeed(float speed) => _speed = speed;
    }
}
