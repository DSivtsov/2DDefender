using System;
using Modules.GameManager;
using UnityEngine;
using GameEngine.Bullet;
using GamePlay.Common;

namespace GamePlay.Player
{
    internal interface IAttackInput
    {
        event Action OnFireRequired;
    }

    [Serializable]
    internal sealed class AttackController : IGameInitListener, IGameFixedUpdateListener, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private BulletConfigSO _bulletPlayerConfig;
        
        private IAttackInput _iInput;
        private BulletLifeController _bulletSpawner;
        private PlayerObject _playerObject;
        private Weapon _playerWeapon;
        private bool _fireRequired;

        [Inject]
        internal void Construct(IAttackInput iInput, PlayerObject playerObject, BulletLifeController bulletSpawner)
        {
            _iInput = iInput;
            _playerObject = playerObject;
            _bulletSpawner = bulletSpawner;
        }

        void IGameInitListener.OnInit() => _playerWeapon = _playerObject.GetComponent<Weapon>();
        void IGameStartListener.OnStartGame() => _iInput.OnFireRequired += SetFireRequired;

        void IGameFinishListener.OnFinishGame() => _iInput.OnFireRequired -= SetFireRequired;

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false; 
            }
        }

        //Set in Update Cycle
        private void SetFireRequired()
        {
            _fireRequired = true;
        }

        private void OnFlyBullet()
        {
            Vector2 vectorVelocityNorm = (Vector2)(_playerWeapon.Rotation * Vector3.up);
            _bulletSpawner.ShootBullet(_bulletPlayerConfig, _playerWeapon.Position, vectorVelocityNorm);
        }
    }
}
