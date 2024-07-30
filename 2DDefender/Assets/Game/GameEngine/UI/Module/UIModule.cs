using Modules.GameManager;
using UnityEngine;

namespace GameEngine.UI
{
    public sealed class UIModule : ModuleBase
    {
        [SerializeField, Listener]
        private HealthViewAdapter healthViewAdapter;
    }
}