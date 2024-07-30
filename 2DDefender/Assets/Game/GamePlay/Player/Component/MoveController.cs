using System;
using GameEngine.Common;
using GamePlay.Common;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal interface IMoveInput
    {
        event Action<Vector2> OnSetMoveDirection;
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class MoveController : MonoBehaviour, IGameInitListener, IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        [SerializeField] private float _speed = 5f;
        
        private Rigidbody2D _rigidbodyObj;
        private IMoveInput _iInput;
        private IAttackController _attackController;
        private Vector2 _currentMoveDirection;
        private bool _isShooting;
        void IGameInitListener.OnInit() =>  _rigidbodyObj = GetComponent<Rigidbody2D>();

        [Inject]
        internal void Construct(IMoveInput iInput, IAttackController attackController)
        {
            _attackController = attackController;
            _iInput = iInput;
        }

        void IGameStartListener.OnStartGame()
        {
            _iInput.OnSetMoveDirection += SetMoveDirection;
            _attackController.OnShooting += Shooting;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _iInput.OnSetMoveDirection -= SetMoveDirection;
            _attackController.OnShooting -= Shooting;
            
            _rigidbodyObj.velocity = Vector2.zero;
        }

        private void Shooting(bool active)
        {
            _isShooting = active;
        }

        private void SetMoveDirection(Vector2 direction)
        {
            _currentMoveDirection = direction;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float _)
        {
            if (_isShooting)
            {
                _rigidbodyObj.velocity = Vector2.zero;
            }
            else
            {
                _rigidbodyObj.velocity = _currentMoveDirection * _speed;
            }
        }
    }
}