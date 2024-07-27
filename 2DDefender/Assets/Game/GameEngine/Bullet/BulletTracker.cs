using System;
using System.Collections.Generic;
using UnityEngine;
using Modules.GameManager;
using GameEngine.Common;

namespace GameEngine.Bullet
{
    internal sealed class BulletTracker : IGameFixedUpdateListener, IGameFinishListener
    {
        private ScreenBounds _screenBounds;
        private readonly HashSet<BulletObjectInPool> _trackedBullets = new();
        private readonly List<BulletObjectInPool> _cache = new();
        private BulletLifeController _bulletController;

        [Inject]
        internal void Construct(ScreenBounds screenBounds, BulletLifeController bulletController)
        {
            _screenBounds = screenBounds;
            _bulletController = bulletController;
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float _) => CheckBulletsOutScreenBounds();

        void IGameFinishListener.OnFinishGame() => RemoveAllActiveBullets();

        internal void AddBulletToTracked(BulletObjectInPool bullet)
        {
            if (_trackedBullets.Add(bullet))
            {
                bullet.OnHitGameObject += HitGameObject;
            }
            else
                throw new NotSupportedException("Something wrong: Attempt to add the bullet that exist in _activeBullets");
        }

        private void HitGameObject(BulletObjectInPool bulletObject, GameObject hitGameObject)
        {
            RemoveBulletFromTracked(bulletObject);
            BulletDealDamage(bulletObject.Damage, hitGameObject);
        }

        private void BulletDealDamage(int damage, GameObject hitGameObject)
        {
            hitGameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }

        private void RemoveAllActiveBullets()
        {
            _cache.Clear();
            _cache.AddRange(_trackedBullets);
            for (int i = 0, count = _cache.Count; i < count; i++)
                RemoveBulletFromTracked(_cache[i]);
        }

        private void CheckBulletsOutScreenBounds()
        {
            _cache.Clear();
            _cache.AddRange(_trackedBullets);
            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_screenBounds.InBounds(bullet.transform.position))
                    RemoveBulletFromTracked(bullet);
            }
        }

        private void RemoveBulletFromTracked(BulletObjectInPool bulletObject)
        {
            if (_trackedBullets.Remove(bulletObject))
            {
                bulletObject.OnHitGameObject -= HitGameObject;
                _bulletController.ReturnBulletToPool(bulletObject);
            }
            else
                throw new NotSupportedException("Something wrong: Attempt to remove the bullet that not exist");
        }
    }

}