using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class PlayerModule : ModuleBase
    {
        [SerializeField, Service, Listener]
        private PlayerObject playerObject;

        [SerializeField, Listener]
        private AttackController attackPlayerController;

        [Listener]
        private MoveController moveController = new MoveController();

        [Service, Listener]
        private KeyboardMoveInput keyboardMoveInput = new KeyboardMoveInput();

        [Service, Listener]
        private KeyboardAttackInput keyboardAttackInput = new KeyboardAttackInput();

        [Listener]
        private PlayerLifeController playerControllerGame = new PlayerLifeController();
    }
}