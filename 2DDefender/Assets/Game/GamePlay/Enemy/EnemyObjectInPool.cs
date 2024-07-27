using System;
using GameEngine.Common;
using UnityEngine;
using Zenject;

namespace GamePlay.Enemy
{
    [Serializable]
    internal sealed class EnemyObjectInPool : MonoBehaviour, IPoolable<GameObject, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        internal EnemyMoveController MoveController { get; private set; }
        internal EnemyAttackController AttackController { get; private set; }
        internal IDamageable DamageableController { get; private set; }
        internal static int PoolNumActive { get; private set; }

        internal class Factory : PlaceholderFactory<GameObject, EnemyObjectInPool>
        {
            public static EnemyGeneratorPositions GeneratorPositions { get; private set; }

            public Factory(EnemyGeneratorPositions generatorPositions)
            {
                GeneratorPositions = generatorPositions;
            }
        }
        
        public void OnDespawned()   
        {
            _pool = null;
            PoolNumActive--;
        }

        public void OnSpawned(GameObject target, IMemoryPool pool)
        {
            _pool = pool;
            PoolNumActive++;

            transform.position = Factory.GeneratorPositions.RandomSpawnPosition();
            
            MoveController = GetComponent<EnemyMoveController>(); 
            MoveController.SetDestination(Factory.GeneratorPositions.RandomAttackPosition());

            AttackController = GetComponent<EnemyAttackController>();
            AttackController.SetTarget(target);

            DamageableController = GetComponent<IDamageable>();
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
    }


}