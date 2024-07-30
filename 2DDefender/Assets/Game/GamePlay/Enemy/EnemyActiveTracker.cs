using System;
using System.Collections.Generic;
using System.Linq;
using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Enemy
{
    internal sealed class EnemyActiveTracker : IGameFixedUpdateListener
    {
        public GameObject[] Enemies => _cacheEnemies;
        
        private readonly Dictionary<GameObject, IEnemyFixUpdateListeners[]> _dictActiveEnemies = new ();

        private IEnemyFixUpdateListeners[] _cacheEnemyFixUpdateListeners;
        private GameObject[] _cacheEnemies;

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            foreach (var listener in _cacheEnemyFixUpdateListeners)
                listener.OnFixedUpdate(fixedDeltaTime);
        }

        private int _count = 0;
        internal void AddEnemyToActiveList(GameObject enemy)
        {
            if (_dictActiveEnemies.ContainsKey(enemy))
                throw new NotSupportedException("Attempt to add enemy to Active list which exist in the list");
            
            enemy.name = $"Bomber_{_count++}";
            
            IEnemyFixUpdateListeners[] fixUpdateListeners = enemy.GetComponents<IEnemyFixUpdateListeners>();
            _dictActiveEnemies.Add(enemy, fixUpdateListeners ?? Array.Empty<IEnemyFixUpdateListeners>());
            
            UpdateCaches();
        }

        internal void RemoveEnemyFromActiveList(GameObject enemy)
        {
            if (!_dictActiveEnemies.ContainsKey(enemy))
                throw new NotSupportedException("Attempt to del enemy from Active list which doesn't exist in the list");
            
            _dictActiveEnemies.Remove(enemy);

            UpdateCaches();
        }

        private void UpdateCaches()
        {
            _cacheEnemyFixUpdateListeners = _dictActiveEnemies.Values.SelectMany(i => i).ToArray();
            _cacheEnemies = _dictActiveEnemies.Keys.ToArray();
        }
    }
}