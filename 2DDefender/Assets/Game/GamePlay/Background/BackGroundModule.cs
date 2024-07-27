using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Background
{
    internal sealed class BackGroundModule : ModuleBase
    {
        [SerializeField, Listener]
        private MoveWorldBackground moveLevelBackground;
    } 
}