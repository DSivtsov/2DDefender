using System;
using Modules.GameManager;
using UnityEngine;

namespace GameEngine.Bullet
{
    [Serializable]
    public sealed class BulletLifeController
    {
        [SerializeField] private Transform _worldTransform;

        private BulletTracker _bulletTracker;
        private BulletObjectInPool.Factory _bulletPoolFactory;

        [Inject]
        internal void Construct(BulletTracker bulletTracker, BulletObjectInPool.Factory bulletPoolFactory)
        {
            _bulletTracker = bulletTracker;
            _bulletPoolFactory = bulletPoolFactory;
        }

        public void ShootBullet(BulletConfigSO bulletConfig, Vector2 weaponPosition, Vector2 vectorVelocityNorm)
        {
            BulletObjectInPool bullet = _bulletPoolFactory.Create(bulletConfig, weaponPosition, vectorVelocityNorm);
            bullet.transform.SetParent(_worldTransform);
            _bulletTracker.AddBulletToTracked(bullet);
        }

        internal void ReturnBulletToPool(BulletObjectInPool bulletObject)
        {
            bulletObject.Dispose();
        }
    }
}