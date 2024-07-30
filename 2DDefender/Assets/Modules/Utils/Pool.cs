using System.Collections.Generic;
using Zenject;

namespace GameEngine.Bullet
{
    /*
     * Bullet Pool based on Zenject PlaceholderFactory<T>
     */
    public class Pool<T>
    {
        private int _initialCount;
        private readonly Queue<T> _pool = new();
        private readonly PlaceholderFactory<T> _bulletObjectFactory;
        
        public Pool(PlaceholderFactory<T>  bulletObjectFactory)
        {
            _bulletObjectFactory = bulletObjectFactory;
        }

        public Pool<T> InitSize(int size)
        {
            _initialCount = size;
            return this;
        }

        public void InstantiateInitialBulletsInPool()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                EnqueueNewBullet();
            }
        }

        private void EnqueueNewBullet()
        {
            _pool.Enqueue(CreateBullet());
        }

        private T CreateBullet() => _bulletObjectFactory.Create();

        public T GetBullet()
        {
            T @object;
            if (!_pool.TryDequeue(out @object))
            {
                @object = CreateBullet();
            }
            return @object;
        }

        public void ReturnBullet(T @object)
        {
            _pool.Enqueue(@object);
        }
    }
}