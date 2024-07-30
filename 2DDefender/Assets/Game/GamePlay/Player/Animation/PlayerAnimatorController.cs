using System;
using System.Data;
using Modules.GameManager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Player
{
    internal interface IWalkAnimationState
    {
        event Action<State> OnSetWalkAnimationState;
    }

    public class PlayerAnimatorController : IGameStartListener, IGameFinishListener, IGameInitListener
    {
        private static readonly int GoIdle = Animator.StringToHash("GoIdle");
        private static readonly int GoUp = Animator.StringToHash("GoUp");
        private static readonly int GoDown = Animator.StringToHash("GoDown");
        private static readonly int GoSide = Animator.StringToHash("GoSide");
        private static readonly int Fire = Animator.StringToHash("Fire");
        private readonly Vector3 _riflemanRotateAlongX = new Vector3(-1,1,1);
        private readonly Vector3 _riflemanNormalRotation = Vector3.one;
    
        private Animator _animator;
        private Transform _riflemanTransform;
        private RotateRifle _rotateRifle;
        private bool _rotatedAlongX;
        private IWalkAnimationState _iWalkAnimationState;
        private PlayerObject _playerObject;
        private State _currentState;
        private IAttackController _attackController;
        private bool _isShooting;
        
        [Inject]
        internal void Construct(IWalkAnimationState iAnimationState, PlayerObject playerSpawner, IAttackController attackController)
        {
            _attackController = attackController;
            _iWalkAnimationState = iAnimationState;
            _playerObject = playerSpawner;
        }
        
        void IGameInitListener.OnInit()
        {
            _animator = _playerObject.GetComponent<Animator>();
            _rotateRifle = _playerObject.GetComponent<RotateRifle>();
            _riflemanTransform = _playerObject.transform;
            _currentState = State.Idle;
            _rotatedAlongX = false;
        }

        void IGameStartListener.OnStartGame()
        {
            _iWalkAnimationState.OnSetWalkAnimationState += SetWalkAnimationState;
            _attackController.OnShooting += Shooting;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _iWalkAnimationState.OnSetWalkAnimationState -= SetWalkAnimationState;
            _attackController.OnShooting -= Shooting;

            SetWalkAnimationState(State.Idle);
        }

        private void Shooting(bool active)
        {
            if (active)
            {
                _isShooting = true;
                SetShootAnimationState();
            }
            else
            {
                _isShooting = false;
                _currentState = State.Idle;
                _rotateRifle.TurnRifleOnIdle();
            }
        }

        [Button]
        public void SetWalkAnimationState(State state)
        {
            if (_currentState == state || _isShooting) return;
            _currentState = state;
            
            switch (state)
            {
                case State.Idle:
                    _animator.SetTrigger(GoIdle);
                    break;
                case State.WalkUp:
                    _animator.SetTrigger(GoUp);
                    break;
                case State.WalkDown:
                    _animator.SetTrigger(GoDown);
                    break;
                case State.WalkLeft:
                    _animator.SetTrigger(GoSide);
                    break;
                case State.WalkRight:
                    RotateAlongX();
                    _rotatedAlongX = true;
                    _animator.SetTrigger(GoSide);
                    return;
                default:
                    throw new DataException($"[RiflemanAnimationController] Doesn't support AnimationState[{state}]");
            }
            ReturnNormalRotation();
        }
    
        [Button]
        public void SetShootAnimationState()
        {
            _rotateRifle.TurnRifleOnTarget();
            _animator.SetTrigger(Fire);
            
            ReturnNormalRotation();
        }
        
        private void RotateAlongX() => _riflemanTransform.localScale = _riflemanRotateAlongX;

        private void ReturnNormalRotation()
        {
            if (!_rotatedAlongX) return;
            
            _rotatedAlongX = false;
            _riflemanTransform.localScale = _riflemanNormalRotation;
        }
    }
}