using Modules.GameManager;
using Modules.StartStopGameUI;
using UnityEngine;

namespace GameEngine.UI
{
    public sealed class StartFinishUIAdapter : IGameInitListener
    {   
        private IStartStopGamePopup _iStartStopGamePopup;
        
        [Inject]
        public void Constructor (GameManager gameManager, IStartStopGamePopup isStartStopGamePopup)
        {
            _iStartStopGamePopup = isStartStopGamePopup;
            _iStartStopGamePopup.OnStartGame += gameManager.StartGame;
        }

        void IGameInitListener.OnInit()
        {
            _iStartStopGamePopup.OnBeforeGame();
        }

        public void OnLooseGame() => _iStartStopGamePopup.OnAfterFinishGame(ShowTypeUI.GAME_OVER_LOOSE);
        
        public void OnWinGame() => _iStartStopGamePopup.OnAfterFinishGame(ShowTypeUI.GAME_OVER_WIN);
    }
}