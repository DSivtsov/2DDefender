using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Modules.StartStopGameUI
{
    public interface IStartStopGamePopup
    {
        void OnBeforeGame();
        void OnAfterFinishGame(ShowTypeUI showUI);
        event Action OnStartGame;
    }
    public enum ShowTypeUI
    {
        NOTHING = 0,
        INTRO_WINDOWS = 1,
        COUNT_DOWN = 2,
        GAME_OVER_LOOSE = 3,
        GAME_OVER_WIN = 4,
    }
    public sealed class StartFinishUIPopup : MonoBehaviour, IStartStopGamePopup
    {
        [SerializeField] private GameObject _cameraUI;
        [SerializeField] private Transform _interfaceInfoTransform;
        [SerializeField] private Transform _countingTransform;
        [SerializeField] private Transform _gameOverParent;
        [SerializeField] private Transform _looseWin;
        [SerializeField] private Transform _victoryWin;
        [SerializeField] private Button _buttonStart;
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
            _inputEventStartGame = new InputEventAnyKeyAndMouseClick();
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
            _inputEventStartGame.ActivateEvent(_buttonStart);
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
        
        void IStartStopGamePopup.OnAfterFinishGame(ShowTypeUI showTypeUI)
        {
            Button restartButton;
            switch (showTypeUI)
            {
                case ShowTypeUI.GAME_OVER_LOOSE:
                    _looseWin.gameObject.SetActive(true);
                    _victoryWin.gameObject.SetActive(false);
                    restartButton = _looseWin.GetComponentInChildren<Button>();
                    break;
                case ShowTypeUI.GAME_OVER_WIN:
                    _looseWin.gameObject.SetActive(false);
                    _victoryWin.gameObject.SetActive(true);
                    restartButton = _victoryWin.GetComponentInChildren<Button>();
                    break;
                default:
                    throw new NotSupportedException($"[StartFinishUIPopup] OnAfterFinishGame({showTypeUI}) not supported");
            }
            ShowUI(showTypeUI);
            _inputEventStartGame.ActivateEvent(restartButton);
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
                case ShowTypeUI.GAME_OVER_LOOSE:
                case ShowTypeUI.GAME_OVER_WIN:
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
            _gameOverParent.gameObject.SetActive(activateGameOver);
            _cameraUI.SetActive(activeInfo || activeCounting || activateGameOver);
        }
    }
}
