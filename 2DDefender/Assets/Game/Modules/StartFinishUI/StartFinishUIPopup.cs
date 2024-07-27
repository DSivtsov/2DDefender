using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Modules.StartStopGameUI
{
    public interface IStartStopGamePopup
    {
        void OnBeforeGame();
        void OnAfterFinishGame();
        event Action OnStartGame;
    }
    public enum ShowTypeUI
    {
        NOTHING = 0,
        INTRO_WINDOWS = 1,
        COUNT_DOWN = 2,
        GAME_OVER = 3,
    }
    public sealed class StartFinishUIPopup : MonoBehaviour, IStartStopGamePopup
    {
        [SerializeField] private GameObject _cameraUI;
        [SerializeField] private Transform _interfaceInfoTransform;
        [SerializeField] private Transform _countingTransform;
        [SerializeField] private Transform _gameOver;
        [SerializeField] private Button _buttonStart;
        [FormerlySerializedAs("_activate")]
        [FormerlySerializedAs("_activateCountDownBeforeStart")]
        [Header("CountDownBeforeStart")]
        [SerializeField] private bool _activateCounting = true;
        [SerializeField] private int _beginCountingNumber = 3;
        [SerializeField] private int _endCountingNumber = 0;
        [SerializeField] private float _countingInterval = 1F;
        
        private AnimateCountDown _animateCountDown;
        private InputEventAnyKeyAndMouseClick _inputEventStartGame;
        
        public event Action OnStartGame;
        
        //Internal Initialization of components of the MonoBehaviour object
        private void Awake()
        {
            _inputEventStartGame = new InputEventAnyKeyAndMouseClick(_buttonStart);
            InitAndActivateCounting();
            //set initial State
            ShowUI(false,false,false);
        }

        private void InitAndActivateCounting()
        {
            if (_activateCounting)
            {
                _animateCountDown = new AnimateCountDown(_countingTransform.GetComponentInChildren<TextMeshProUGUI>(),
                    _beginCountingNumber, _endCountingNumber, _countingInterval);
                _inputEventStartGame.OnInputEvent += StartCountDown;
                _animateCountDown.OnCountDownFinished += StartGame;
            }
            else
                _inputEventStartGame.OnInputEvent += StartGame;
        }

        void IStartStopGamePopup.OnBeforeGame()
        {
            ShowUI(ShowTypeUI.INTRO_WINDOWS);
            _inputEventStartGame.ActivateEvent();
        }
        
        private void StartCountDown()
        {
            ShowUI(ShowTypeUI.COUNT_DOWN);
            _animateCountDown.StartCountDown();
        }
        
        private void StartGame()
        {
            ShowUI(ShowTypeUI.NOTHING);
            OnStartGame?.Invoke();
        }
        
        void IStartStopGamePopup.OnAfterFinishGame()
        {
            ShowUI(ShowTypeUI.GAME_OVER);
            Debug.LogWarning("Game over");
        }

        private void ShowUI(ShowTypeUI showTypeUI)
        {
            switch (showTypeUI)
            {
                case ShowTypeUI.NOTHING:
                    ShowUI(false,false,false);
                    break;
                case ShowTypeUI.INTRO_WINDOWS:
                    ShowUI(true,false,false);
                    break;
                case ShowTypeUI.COUNT_DOWN:
                    ShowUI(false,true,false);
                    break;
                case ShowTypeUI.GAME_OVER:
                    ShowUI(false,false,true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showTypeUI), showTypeUI, null);
            }
        }

        private void ShowUI(bool activeInfo, bool activeCounting, bool activateGameOver)
        {
            _interfaceInfoTransform.gameObject.SetActive(activeInfo);
            _countingTransform.gameObject.SetActive(activeCounting);
            _gameOver.gameObject.SetActive(activateGameOver);
            _cameraUI.SetActive(activeInfo || activeCounting || activateGameOver);
        }
    }
}
