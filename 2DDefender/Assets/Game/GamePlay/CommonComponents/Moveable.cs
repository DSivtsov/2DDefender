using System;
using UnityEngine;

namespace GamePlay.Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Moveable : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        private Rigidbody2D _rigidbodyObj;

        private void Awake()
        {
            _rigidbodyObj = GetComponent<Rigidbody2D>();
        }

        internal void MoveRigidbody(Vector2 offsetToMoveRigidbody) => _rigidbodyObj.MovePosition(GetNewPosition(offsetToMoveRigidbody));

        private Vector2 GetNewPosition(Vector2 offsetToMoveRigidbody) => _rigidbodyObj.position + offsetToMoveRigidbody * _speed;

        /// <summary>
        /// Check that a new position after move (based on the offset) will be acceptable
        /// </summary>
        /// <param name="offsetToMoveRigidbody"></param>
        /// <param name="newPositionIsAcceptable">condition for checking a new position</param>
        internal void CheckNewPositionBeforeMoveRigidbody(Vector2 offsetToMoveRigidbody, Func<Vector2, bool> newPositionIsAcceptable)
        {
            Vector2 newPosition = GetNewPosition(offsetToMoveRigidbody);
            if (newPositionIsAcceptable(newPosition))
                _rigidbodyObj.MovePosition(newPosition); 
        }
    }
}
