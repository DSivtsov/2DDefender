using System;
using System.Collections;
using Asyncoroutine;
using Modules.GameManager;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemySpawnController : IGameStartListener, IGameFinishListener
    {
        [MinMaxSlider(1, 10, true)]
        [SerializeField] private Vector2 _delaySpawnMinMax = new Vector2(1,2);

        private EnemyLifeController _enemySpawner;
        private bool _continueEnemySpawning;

        [Inject]
        internal void Construct(EnemyLifeController enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }
        
        async void IGameStartListener.OnStartGame()
        {
            _continueEnemySpawning = true;
            await ContinuouslySpawnEnemy();
        }

        void IGameFinishListener.OnFinishGame()
        {
            _continueEnemySpawning = false;

            _enemySpawner.DestroyAllEnemiesObject();
        }

        private IEnumerator ContinuouslySpawnEnemy()
        {
            do
            {
                _enemySpawner.SpawnEnemy();
                yield return new WaitForSeconds(GetNextSpawnDelay());
            } while (_continueEnemySpawning);
        }

        private float GetNextSpawnDelay() => Random.Range(_delaySpawnMinMax.x, _delaySpawnMinMax.y);
    }
}