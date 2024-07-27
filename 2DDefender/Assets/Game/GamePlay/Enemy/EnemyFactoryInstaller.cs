using UnityEngine;
using Zenject;

namespace GamePlay.Enemy
{
    public sealed class EnemyFactoryInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _prefabEnemy;
        [SerializeField] private Transform _transformPoolEnemy;
        [SerializeField] private int _enemyPoolCount = 10;
        [SerializeField] private EnemyGeneratorPositions _generatorPositions;
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, EnemyObjectInPool, EnemyObjectInPool.Factory>()
                .WithFactoryArguments(_generatorPositions)
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