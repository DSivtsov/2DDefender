using System;
using GameEngine.Common;
using UnityEngine;
using Zenject;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemyObjectInPool : MonoBehaviour, IPoolable<GameObject, float, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        internal EnemyAttackController AttackController { get; private set; }
        internal IDamageable DamageableController { get; private set; }
        internal static int PoolNumActive { get; private set; }

        internal class Factory : PlaceholderFactory<GameObject, float, EnemyObjectInPool>
        {
            public static EnemySpawnPositions GeneratorPositions { get; private set; }
            public static int InitialHitPoints { get; private set; }
            public Factory(EnemySpawnPositions generatorPositions, int initialHitPoints)
            {
                GeneratorPositions = generatorPositions;
                InitialHitPoints = initialHitPoints;
            }
        }
        
        public void OnDespawned()   
        {
            _pool = null;
            PoolNumActive--;
        }

        public void OnSpawned(GameObject target, float enemySpeed, IMemoryPool pool)
        {
            _pool = pool;
            PoolNumActive++;

            transform.position = Factory.GeneratorPositions.RandomSpawnPosition();

            GetComponent<EnemyMoveController>().SetEnemySpeed(enemySpeed);
            
            AttackController = GetComponent<EnemyAttackController>();
            AttackController.SetTarget(target);

            DamageableController = GetComponent<IDamageable>();
            DamageableController.SetInitialHitPoints(Factory.InitialHitPoints);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
    }
}