using Modules.GameManager;
using UnityEngine;

namespace Modules.StartStopGameUI
{
    public sealed class UIModule : ModuleBase
    {
        [SerializeField, Service]
        private StartFinishUIPopup  startFinishUIPopup;

        [Listener]
        private readonly StartFinishUIAdapter startFinishUIAdapter = new StartFinishUIAdapter();
    }
}