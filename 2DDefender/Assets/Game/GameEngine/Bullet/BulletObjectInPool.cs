using System;
using UnityEngine;
using Zenject;

namespace GameEngine.Bullet
{
    internal sealed class BulletObjectInPool : MonoBehaviour, IPoolable<BulletConfigSO, Vector2, Vector2, IMemoryPool>, IDisposable
    {
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private int _damage;
        internal int Damage => _damage;
        
        IMemoryPool _pool;
        internal event Action<BulletObjectInPool, GameObject> OnHitGameObject;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            OnHitGameObject?.Invoke(this,col.gameObject);
        }

        internal sealed class Factory : PlaceholderFactory<BulletConfigSO, Vector2, Vector2, BulletObjectInPool> { }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(BulletConfigSO bulletConfig, Vector2 weaponPosition, Vector2 vectorVelocityNorm, IMemoryPool pool)
        {
            _pool = pool;
            _rigidbody.velocity = vectorVelocityNorm * bulletConfig.speed;
            this.transform.position = weaponPosition;
            _spriteRenderer.color = bulletConfig.color;
            this.gameObject.layer = (int)bulletConfig.physicsLayer;
            _damage = bulletConfig.damage;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
    }
}