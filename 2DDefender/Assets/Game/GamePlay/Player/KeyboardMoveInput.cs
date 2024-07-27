using System;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class KeyboardMoveInput : IGameUpdateListener, IMoveInput
    {
        public event Action<float> OnSetMoveDirection;

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            this.HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                SetMoveDirection(-1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                SetMoveDirection(1);
            }
            else
            {
                SetMoveDirection(0);
            }
        }

        private void SetMoveDirection(float horizontalDirection)
        {
            OnSetMoveDirection?.Invoke(horizontalDirection);
        }
    }
}