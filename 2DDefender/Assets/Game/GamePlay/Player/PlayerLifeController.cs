using GameEngine.Common;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class PlayerLifeController : IGameStartListener, IGameFinishListener, IGameInitListener
    {
        private GameManager _gameManager;
        private PlayerObject _playerObject;
        private IDamageable _damageable;

        [Inject]
        internal void Construct(GameManager gameManager, PlayerObject playerObject)
        {
            _gameManager = gameManager;
            _playerObject = playerObject;
        }

        void IGameInitListener.OnInit() => _damageable = _playerObject.GetComponent<IDamageable>();
        void IGameStartListener.OnStartGame() => _damageable.OnDied += OnCharacterDeath;

        void IGameFinishListener.OnFinishGame() => _damageable.OnDied -= OnCharacterDeath;

        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    } 
}
