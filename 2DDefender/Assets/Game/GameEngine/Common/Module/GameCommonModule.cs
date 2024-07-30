using UnityEngine;
using Modules.GameManager;

namespace GameEngine.Common
{
    public sealed class GameCommonModule : ModuleBase
    {
        [SerializeField, Service]
        private ScreenBounds _screenBounds;
    } 
}
