using System;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class KeyboardAttackInput : IGameUpdateListener, IAttackInput
    {
        public event Action OnFireRequired;

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            this.HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFireRequired?.Invoke();
            }
        }
    } 
}
