using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class PlayerModule : ModuleBase
    {
        [SerializeField, Service, Listener] private PlayerObject playerObject;

        [SerializeField, Service, Listener] private AttackController attackPlayerController;

        [SerializeField, Listener] private MoveController moveController;

        [Service, Listener] private KeyboardMoveInput keyboardMoveInput = new KeyboardMoveInput();

        [Service, Listener] private KeyboardAttackInput keyboardAttackInput = new KeyboardAttackInput();

        [Listener] private PlayerLifeController playerControllerGame = new PlayerLifeController();
        
        [Listener] private PlayerAnimatorController PlayerAnimationController = new PlayerAnimatorController();
        
        [SerializeField] private RotateRifle rotateRifle;

        [SerializeField, Service] private DefendedWallObject defendedWallObject;
    }
}