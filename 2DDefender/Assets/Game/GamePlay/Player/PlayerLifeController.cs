using GameEngine.Common;
using GameEngine.GameMaster;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class PlayerLifeController : IGameStartListener, IGameFinishListener, IGameInitListener
    {
        private PlayerObject _playerObject;
        private IDamageable _damageable;
        private GameMaster _gameMaster;

        [Inject]
        internal void Construct(PlayerObject playerObject, GameMaster gameMaster)
        {
            _gameMaster = gameMaster;
            _playerObject = playerObject;
        }

        void IGameInitListener.OnInit() => _damageable = _playerObject.GetComponent<IDamageable>();
        void IGameStartListener.OnStartGame() => _damageable.OnDied += OnCharacterDeath;

        void IGameFinishListener.OnFinishGame() => _damageable.OnDied -= OnCharacterDeath;

        private void OnCharacterDeath(GameObject _)
        {
            _gameMaster.CharacterDeath();
        }
    } 
}
