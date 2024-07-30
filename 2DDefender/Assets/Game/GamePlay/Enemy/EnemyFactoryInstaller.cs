using GameEngine.Common;
using UnityEngine;
using Zenject;

namespace GamePlay.Enemy
{
    public sealed class EnemyFactoryInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _prefabEnemy;
        [SerializeField] private Transform _transformPoolEnemy;
        [SerializeField] private int _enemyPoolCount = 10;
        [SerializeField] private EnemySpawnPositions _generatorPositions;
        public override void InstallBindings()
        {
            int hitPoints = _prefabEnemy.GetComponent<IDamageable>().HitPoints;
            
            Container.BindFactory<GameObject, float, EnemyObjectInPool, EnemyObjectInPool.Factory>()
                .WithFactoryArguments(_generatorPositions, hitPoints)
                .FromMonoPoolableMemoryPool(x =>
                {
                    x
                        .WithInitialSize(_enemyPoolCount)
                        .FromComponentInNewPrefab(_prefabEnemy)
                        .UnderTransform(_transformPoolEnemy);
                });
        }
    }
}