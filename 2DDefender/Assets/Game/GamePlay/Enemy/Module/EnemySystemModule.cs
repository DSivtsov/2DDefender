using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Enemy
{
    internal sealed class EnemySystemModule : ModuleBase
    {
        [SerializeField, Service, Listener]
        private EnemySpawnController enemySpawnController;

        [SerializeField, Service]
        private EnemyRandomSpeed enemyRandomSpeed;

        [SerializeField, Service, Listener]
        private EnemyLifeController enemyLifeController;

        //Will be injected and instantiated by Zenject and used through Service
        [Zenject.Inject, Service]
        private EnemyObjectInPool.Factory enemyPool;

        [Service, Listener]
        private EnemyActiveTracker enemyTracker = new EnemyActiveTracker();
    }
}
