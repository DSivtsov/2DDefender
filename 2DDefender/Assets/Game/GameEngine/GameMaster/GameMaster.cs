using System;
using GameEngine.UI;
using Modules.GameManager;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEngine.GameMaster
{
    public class GameMaster : MonoBehaviour, IGameStartListener
    {
        public event Action<int> OnChangeEnemyNumber ;
        [SerializeField] private bool _randomNumberEnemies;
        [SerializeField, HideIf("_randomNumberEnemies")] private int _numberEnemies;
        
        [ShowIfGroup("_randomNumberEnemies")]
        [BoxGroup("_randomNumberEnemies/Number Enemies")] [SerializeField] private int _minNumberEnemies;
        [BoxGroup("_randomNumberEnemies/Number Enemies")] [SerializeField] private int _maxNumberEnemies;

        private StartFinishUIAdapter _startFinishUIAdapter;
        private GameManager _gameManager;
        private int _killedEnemies;
        
        [Inject]
        public void Constructor (GameManager gameManager, StartFinishUIAdapter startFinishUIAdapter)
        {
            _gameManager = gameManager;
            _startFinishUIAdapter = startFinishUIAdapter;
        }

        void IGameStartListener.OnStartGame()
        {
            RestartGameMaster();
            if (_randomNumberEnemies) 
                _numberEnemies = GetTargetNumberKilledEnemies();
            OnChangeEnemyNumber?.Invoke(_numberEnemies);
        }

        private int GetTargetNumberKilledEnemies() => Random.Range(_minNumberEnemies, _maxNumberEnemies + 1);

        private void RestartGameMaster()
        {
            _killedEnemies = 0;
        }

        public void CharacterDeath()
        {
            _gameManager.FinishGame();
            _startFinishUIAdapter.OnLooseGame();
        }

        public void KilledEnemy()
        {
            _killedEnemies++;
            OnChangeEnemyNumber?.Invoke(_numberEnemies - _killedEnemies);
            if (_killedEnemies >= _numberEnemies )
            {
                _gameManager.FinishGame();
                _startFinishUIAdapter.OnWinGame();
            }
        }
    }
}
