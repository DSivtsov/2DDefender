using GameEngine.UI;
using Modules.GameManager;
using Modules.StartStopGameUI;
using UnityEngine;

namespace GameEngine.GameMaster
{
    public sealed class GameMasterModule : ModuleBase
    {
        [SerializeField, Service]
        private StartFinishUIPopup  startFinishUIPopup;

        [Listener, Service]
        private readonly StartFinishUIAdapter startFinishUIAdapter = new StartFinishUIAdapter();
        
        [SerializeField, Service, Listener]
        private GameMaster gameMaster;
    } 
}
