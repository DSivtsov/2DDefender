using System;
using Asyncoroutine;
using GameEngine.GameMaster;
using UnityEngine;
using Modules.GameManager;
using GamePlay.Player;

namespace GamePlay.Enemy
{
    [Serializable]
    public sealed class EnemyLifeController
    {
        [Header("Spawn")]
        [SerializeField] private Transform _worldTransform;

        [SerializeField] private GameObject _grenade;
        private EnemyObjectInPool.Factory _enemyPoolFactory;
        private EnemyActiveTracker _enemyTracker;
        private DefendedWallObject _defendedWallObject;
        private GameMaster _gameMaster;
        private EnemyRandomSpeed _enemyRandomSpeed;
        private WaitForSeconds _waitForSeconds;

        [Inject]
        internal void Construct(EnemyObjectInPool.Factory enemyPoolFactory, EnemyActiveTracker enemyTracker,
            DefendedWallObject defendedWallObject, GameMaster gameMaster, EnemyRandomSpeed enemyRandomSpeed)
        {
            _enemyRandomSpeed = enemyRandomSpeed;
            _gameMaster = gameMaster;
            _defendedWallObject = defendedWallObject;
            _enemyPoolFactory = enemyPoolFactory;
            _enemyTracker = enemyTracker;
            _waitForSeconds = new WaitForSeconds(1f);
        }

        internal void SpawnEnemy()
        {
            EnemyObjectInPool enemy = _enemyPoolFactory.Create(_defendedWallObject.Wall, _enemyRandomSpeed.GetRandomSpeed());
            enemy.transform.SetParent(_worldTransform);

            ConnectEnemyToAgents(enemy);
            _enemyTracker.AddEnemyToActiveList(enemy.gameObject);
        }

        private void ConnectEnemyToAgents(EnemyObjectInPool enemy)
        {
            enemy.DamageableController.OnDied += Destroy;
            enemy.DamageableController.OnDied += CountKilledEnemies;
            enemy.AttackController.OnHitWall += HitWall;
        }

        private void DisconnectEnemyFromAgents(EnemyObjectInPool enemy)
        {
            enemy.DamageableController.OnDied -= Destroy;
            enemy.DamageableController.OnDied -= CountKilledEnemies;
            enemy.AttackController.OnHitWall -= HitWall;
        }

        private void CountKilledEnemies(GameObject _) => _gameMaster.KilledEnemy();

        private void HitWall(GameObject _, GameObject enemyObject)
        {
            Destroy(enemyObject);
            BlowUP(enemyObject);
        }

        private void BlowUP(GameObject enemyObject)
        {
            GameObject gameObjectBlowUp = UnityEngine.Object.Instantiate(_grenade, enemyObject.transform.position, Quaternion.identity);
            ResultBlowUp(gameObjectBlowUp);
        }

        private async void  ResultBlowUp(GameObject grenade)
        {
            await _waitForSeconds;
            _defendedWallObject.HitWallBlowUp();
            await _waitForSeconds;
            UnityEngine.Object.Destroy(grenade);
        }
        
        private void Destroy(GameObject enemyObject)
        {
            EnemyObjectInPool enemy = enemyObject.GetComponent<EnemyObjectInPool>();
            DisconnectEnemyFromAgents(enemy);
            enemy.Dispose();
            _enemyTracker.RemoveEnemyFromActiveList(enemyObject);
        }

        public void DestroyAllEnemiesObject()
        {
            foreach (GameObject gameObjectEnemy in _enemyTracker.Enemies)
                Destroy(gameObjectEnemy);
        }
    }
}