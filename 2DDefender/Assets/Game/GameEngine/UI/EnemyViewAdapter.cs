using Modules.GameManager;
using UnityEngine;

namespace GameEngine.UI
{
    public class EnemyViewAdapter : IGameInitListener, IGameStartListener, IGameFinishListener
    {
        private EnemyView _enemyView;
        private GameMaster.GameMaster _gameMaster;

        [Inject]
        public void Constructor (GameMaster.GameMaster gameMaster, EnemyView enemyView)
        {
            _gameMaster = gameMaster;
            _enemyView = enemyView;
        }

        void IGameInitListener.OnInit()
        {
            SetEnemyCount(99);
        }

        void IGameStartListener.OnStartGame()
        {
            _gameMaster.OnChangeEnemyNumber += SetEnemyCount;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _gameMaster.OnChangeEnemyNumber += SetEnemyCount;
        }

        private void SetEnemyCount(int enemyCount)
        {
            if (enemyCount > 99)
            {
                _enemyView.SetEnemyCount("99");
                Debug.LogError("[HealthViewAdapter] field enemyCount overflow > 99");
            }
            else
            {
                _enemyView.SetEnemyCount($"{enemyCount:00}");
            }
        }
    }
}