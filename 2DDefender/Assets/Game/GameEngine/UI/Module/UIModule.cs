using Modules.GameManager;
using UnityEngine;

namespace GameEngine.UI
{
    public sealed class UIModule : ModuleBase
    {
        [SerializeField, Service]
        private HealthView healthView;
        
        [Listener]
        private HealthViewAdapter healthViewAdapter = new HealthViewAdapter();
        
        [SerializeField, Service]
        private EnemyView enemyView;
        
        [Listener]
        private EnemyViewAdapter enemyViewAdapter = new EnemyViewAdapter();
    }
}