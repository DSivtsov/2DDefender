using System;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class KeyboardMoveInput : IGameUpdateListener, IMoveInput, IWalkAnimationState
    {
        public event Action<Vector2> OnSetMoveDirection;
        public event Action<State> OnSetWalkAnimationState;

        void IGameUpdateListener.OnUpdate(float _)
        {
            this.HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.A))
            {
                SetMoveDirection(Vector2.left);
                SetAnimationState(State.WalkLeft);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SetMoveDirection(Vector2.right);
                SetAnimationState(State.WalkRight);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                SetMoveDirection(Vector2.up);
                SetAnimationState(State.WalkUp);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SetMoveDirection(Vector2.down);
                SetAnimationState(State.WalkDown);
            }
            else
            {
                SetMoveDirection(Vector2.zero);
                SetAnimationState(State.Idle);
            }
        }

        private void SetMoveDirection(Vector2 direction)
        {
            OnSetMoveDirection?.Invoke(direction);
        }

        private void SetAnimationState(State state)
        {
            OnSetWalkAnimationState?.Invoke(state);
        }
    }
}