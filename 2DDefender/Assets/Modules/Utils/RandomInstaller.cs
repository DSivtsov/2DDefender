using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;

namespace Modules.Utils
{
    public class RandomInstaller : MonoInstaller
    {
        [Tooltip("Set 0 for random Seed")]
        [SerializeField] int _selectedSeed;

        public override void InstallBindings()
        {
            Container.Bind<Random>().FromNew().AsSingle().WithArguments(_selectedSeed).NonLazy();
        }
    } 
}
