using System;
using GameEngine.Common;
using GamePlay.Common;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal interface IMoveInput
    {
        event Action<float> OnSetMoveDirection;
    }
    
    internal sealed class MoveController : IGameInitListener, IGameStartListener, IGameFinishListener, IGameFixedUpdateListener
    {
        private IMoveInput _iInput;
        private PlayerObject _playerObject;
        private Moveable _playerMoveable;
        private float _horizontalCurrentMoveDirection;
        private ScreenBounds _screenBounds;
        private Func<Vector2, bool> _newPositionIsAcceptableToMove;

        [Inject]
        internal void Construct(IMoveInput iInput, ScreenBounds screenBounds, PlayerObject playerSpawner)
        {
            _iInput = iInput;
            _playerObject = playerSpawner;
            _screenBounds = screenBounds;
            _newPositionIsAcceptableToMove = (Vector2 newPosition) => _screenBounds.InHorizontalBounds(newPosition.x);
        }

        void IGameInitListener.OnInit() => _playerMoveable = _playerObject.GetComponent<Moveable>();

        void IGameStartListener.OnStartGame() => _iInput.OnSetMoveDirection += SetMoveDirection;

        void IGameFinishListener.OnFinishGame() => _iInput.OnSetMoveDirection -= SetMoveDirection;

        //Call in Update Cycle
        private void SetMoveDirection(float horizontalDirection)
        {
            _horizontalCurrentMoveDirection = horizontalDirection;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            Vector2 offsetMoveRigidbody = new Vector2(_horizontalCurrentMoveDirection, 0) * fixedDeltaTime;
            _playerMoveable.CheckNewPositionBeforeMoveRigidbody(offsetMoveRigidbody, _newPositionIsAcceptableToMove);
        }
    }
}