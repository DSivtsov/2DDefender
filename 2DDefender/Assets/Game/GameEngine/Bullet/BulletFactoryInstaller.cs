using UnityEngine;
using Zenject;

namespace GameEngine.Bullet
{
    public sealed class BulletFactoryInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _prefabBullet;
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private int _bulletPoolCount = 20;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BulletConfigSO, Vector2, Vector2, BulletObjectInPool, BulletObjectInPool.Factory>()
                .FromMonoPoolableMemoryPool(x =>
                {
                    x
                        .WithInitialSize(_bulletPoolCount)
                        .FromComponentInNewPrefab(_prefabBullet)
                        .UnderTransform(_parentTransform);
                });
        }
    }
}