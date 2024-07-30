using System;
using UnityEngine;

namespace GamePlay.Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class MoveableByRB : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        private Rigidbody2D _rigidbodyObj;

        private void Awake()
        {
            _rigidbodyObj = GetComponent<Rigidbody2D>();
        }

        internal void MoveRigidbody(Vector2 offsetToMoveRigidbody) => _rigidbodyObj.MovePosition(GetNewPosition(offsetToMoveRigidbody));

        private Vector2 GetNewPosition(Vector2 offsetToMoveRigidbody) => _rigidbodyObj.position + offsetToMoveRigidbody * _speed;

        internal void CheckNewPositionBeforeMoveRigidbody(Vector2 offsetToMoveRigidbody, Func<Vector2, bool> newPositionIsAcceptable)
        {
            Vector2 newPosition = GetNewPosition(offsetToMoveRigidbody);
            if (newPositionIsAcceptable(newPosition))
            {
                //_rigidbodyObj.MovePosition(newPosition);
                _rigidbodyObj.velocity = offsetToMoveRigidbody * _speed;
            } 
        }
    }
}
