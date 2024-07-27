using System;
using System.Collections.Generic;
using System.Linq;
using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Enemy
{
    internal sealed class EnemyActiveTracker : IGameFixedUpdateListener
    {        
        private Dictionary<GameObject, IEnemyFixUpdateListeners[]> _dictActiveEnemies;
        
        [Inject]
        internal void Construct(EnemySpawnController enemySpawnController)
        {
            _dictActiveEnemies = new Dictionary<GameObject, IEnemyFixUpdateListeners[]>(enemySpawnController.MaximumNumEnemies);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            foreach (IEnemyFixUpdateListeners[] fixUpdateListeners in _dictActiveEnemies.Select(enemyPair => enemyPair.Value))
            {
                foreach (var listener in fixUpdateListeners)
                    listener.OnFixedUpdate(fixedDeltaTime);
            }
        }

        internal void AddEnemyToActiveList(GameObject enemy)
        {
            if (_dictActiveEnemies.ContainsKey(enemy))
                throw new NotSupportedException("Attempt to add enemy to Active list which exist in the list");

            IEnemyFixUpdateListeners[] fixUpdateListeners = enemy.GetComponents<IEnemyFixUpdateListeners>();
            _dictActiveEnemies.Add(enemy, fixUpdateListeners ?? Array.Empty<IEnemyFixUpdateListeners>());
        }

        internal void RemoveEnemyFromActiveList(GameObject enemy)
        {
            if (!_dictActiveEnemies.ContainsKey(enemy))
                throw new NotSupportedException("Attempt to del enemy from Active list which doesn't exist in the list");
            
            _dictActiveEnemies.Remove(enemy);
        }
    }
}