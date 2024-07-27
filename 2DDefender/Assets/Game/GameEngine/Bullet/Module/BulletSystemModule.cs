using UnityEngine;
using Modules.GameManager;

namespace GameEngine.Bullet
{
    public sealed class BulletSystemModule : ModuleBase
    {
        [Service, Listener]
        private BulletTracker bulletTracker = new BulletTracker();
        
        [SerializeField, Service, Listener]
        private BulletLifeController bulletController;
        
        //Will be injected and instantiated by Zenject and used through Service
        [Zenject.Inject, Service]
        private BulletObjectInPool.Factory bulletPoolFactory;
    } 
}
