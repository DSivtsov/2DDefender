using System;
using System.Collections;
using Asyncoroutine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Modules.StartStopGameUI
{
    /// <summary>
    /// Get inputs to generate event OnInputEvent 
    /// </summary>
    public class InputEventAnyKeyAndMouseClick
    {
        private readonly Button _button;
        private bool _waitPressAnyKey;
        public event Action OnInputEvent;

        public InputEventAnyKeyAndMouseClick(Button button)
        {
            _button = button;
        }

        public async void ActivateEvent()
        {
            _button.onClick.AddListener(() =>
            {
                _waitPressAnyKey = false;
                InvokeStartGame();
            });
            _waitPressAnyKey = true;
            await CoroutineGetPressAnyKey();
        }
        private IEnumerator CoroutineGetPressAnyKey()
        {
            while (_waitPressAnyKey)
            {
                if (Keyboard.current.anyKey.isPressed)
                {
                    InvokeStartGame();
                    yield break;
                }
                yield return null;
            }
        }

        private void InvokeStartGame() => OnInputEvent?.Invoke();
    }
}