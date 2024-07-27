using System;
using System.Collections;
using Asyncoroutine;
using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemySpawnController : IGameStartListener, IGameFinishListener
    {
        [SerializeField] private int _maximumNumEnemies = 3;
        [SerializeField] private float _delayBetweenSpawn = 1f;
        [Tooltip("Delay after died of first enemy before start Respawn")]
        [SerializeField] private float _delaySpawnAfterDiedFirst = 3f;

        private EnemyLifeController _enemySpawner;
        private bool _continueEnemySpawning;
        internal int MaximumNumEnemies => _maximumNumEnemies;


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
        }

        private IEnumerator ContinuouslySpawnEnemy()
        {
            do
            {
                if (EnemyObjectInPool.PoolNumActive < _maximumNumEnemies)
                {
                    _enemySpawner.SpawnEnemy();
                    yield return new WaitForSeconds(_delayBetweenSpawn);
                }
                else
                {
                    yield return new WaitForSeconds(_delaySpawnAfterDiedFirst);
                }
            } while (_continueEnemySpawning);
        }
    }
}