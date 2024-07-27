using System;
using UnityEngine;
using Modules.GameManager;
using GameEngine.Bullet;
using GamePlay.Player;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemyLifeController
    {
        [Header("Spawn")]
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private BulletConfigSO _bulletEnemyConfig;
        
        private EnemyObjectInPool.Factory _enemyPoolFactory;
        private BulletLifeController _bulletSpawner;
        private EnemyActiveTracker _enemyTracker;
        private PlayerObject _playerObject;
        
        [Inject]
        internal void Construct(EnemyObjectInPool.Factory enemyPoolFactory, BulletLifeController bulletSpawner,
            PlayerObject playerObject,EnemyActiveTracker enemyTracker)
        {
            _playerObject = playerObject;
            _enemyPoolFactory = enemyPoolFactory;
            _bulletSpawner = bulletSpawner;
            _enemyTracker = enemyTracker;
        }

        internal void SpawnEnemy()
        {
            EnemyObjectInPool enemy = _enemyPoolFactory.Create(_playerObject.gameObject);
            enemy.transform.SetParent(_worldTransform);

            ConnectEnemyToAgents(enemy);
            _enemyTracker.AddEnemyToActiveList(enemy.gameObject);
        }

        private void ConnectEnemyToAgents(EnemyObjectInPool enemy)
        {
            enemy.DamageableController.OnDied += Destroy;
            enemy.AttackController.OnFire += Fire;
        }

        private void DisconnectEnemyFromAgents(EnemyObjectInPool enemy)
        {
            enemy.DamageableController.OnDied -= Destroy;
            enemy.AttackController.OnFire -= Fire;
        }

        private void Fire(Vector2 position, Vector2 vectorVelocityNorm)
        {
            _bulletSpawner.ShootBullet(_bulletEnemyConfig, position, vectorVelocityNorm);
        }

        private void Destroy(GameObject enemyObject)
        {
            EnemyObjectInPool enemy = enemyObject.GetComponent<EnemyObjectInPool>();
            DisconnectEnemyFromAgents(enemy);
            enemy.Dispose();
            _enemyTracker.RemoveEnemyFromActiveList(enemyObject);
        }
    }
}