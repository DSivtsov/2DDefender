using Modules.GameManager;

namespace Modules.StartStopGameUI
{
    public sealed class StartFinishUIAdapter : IGameInitListener, IGameFinishListener
    {   
        private IStartStopGamePopup _iStartStopGamePopup;
        
        [Inject]
        public void Constructor (GameManager.GameManager gameManager, IStartStopGamePopup isStartStopGamePopup)
        {
            _iStartStopGamePopup = isStartStopGamePopup;
            _iStartStopGamePopup.OnStartGame += gameManager.StartGame;
        }

        void IGameInitListener.OnInit()
        {
            _iStartStopGamePopup.OnBeforeGame();
        }

        void IGameFinishListener.OnFinishGame()
        {
            _iStartStopGamePopup.OnAfterFinishGame();
        }
    }
}