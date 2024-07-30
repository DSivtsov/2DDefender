using System;
using Modules.GameManager;
using UnityEngine;
using GameEngine.Bullet;
using GamePlay.Common;
using GamePlay.Enemy;
using Modules.Utils;

namespace GamePlay.Player
{
    internal interface IAttackController
    {
        event Action<bool> OnShooting;
    }
    
    [Serializable]
    internal sealed class AttackController : IGameInitListener, IGameFixedUpdateListener, IAttackController
    {
        public event Action<bool> OnShooting;
        public Vector3 ShootDirection => _shootDirection;
        
        [SerializeField] private BulletConfigSO _bulletPlayerConfig;
        [SerializeField,Range(1,40)] private float _shootDistance = 1;
        [SerializeField,Range(1,5)] private float _shootDelay = 3;
        
        private BulletLifeController _bulletSpawner;
        private PlayerObject _playerObject;
        private Weapon _playerWeapon;
        private Vector3 _aimingPosition;
        private Vector3 _targetPosition;
        private Vector3 _targetVelocity;
        private float _speedBullet;
        private Vector3 _shootDirection;
        private EnemyActiveTracker _enemyActiveTracker;
        private float _sqrtShootDistance;
        private GameObject _closestEnemy;
        private Timer _fireCountdown;
        
        [Inject]
        internal void Construct(PlayerObject playerObject, BulletLifeController bulletSpawner, EnemyActiveTracker enemyActiveTracker)
        {
            _enemyActiveTracker = enemyActiveTracker;
            _playerObject = playerObject;
            _bulletSpawner = bulletSpawner;
            _speedBullet = _bulletPlayerConfig.BulletSpeed;
            _sqrtShootDistance = _shootDistance * _shootDistance;
        }

        void IGameInitListener.OnInit()
        {
            _playerWeapon = _playerObject.GetComponent<Weapon>();
            _fireCountdown = new Timer(_shootDelay, Time.fixedDeltaTime);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float _)
        {
            if (_fireCountdown.IsTimeNotFinish()) return;
            
            _aimingPosition = _playerWeapon.AimingPosition;
            
            if (!GetClosestEnemy()) return;
            
            _targetVelocity = GetTargetVelocity();
            _targetPosition = _closestEnemy.transform.position;

            if (TryAttackTarget())
            {
                _fireCountdown.StartTimer();
            }
        }

        private Vector3 GetTargetVelocity()
        {
            EnemyMoveController enemyMoveController = _closestEnemy.GetComponent<EnemyMoveController>();
            if (!enemyMoveController)
                throw new NotSupportedException("[AttackController]: Can't get EnemyMoveController");
            
            float enemySpeed = enemyMoveController.EnemySpeed;
            return Vector3.down * enemySpeed;
        }

        private bool TryAttackTarget()
        {
            _shootDirection = TorpedoTriangleSolver.GetTorpedoVelocity(_targetVelocity, _speedBullet,
                _targetPosition,  _aimingPosition);
            
            if (_shootDirection != Vector3.zero)
            {
                //OnShooting?.Invoke(true);
                Vector2 vectorVelocityNorm = _shootDirection.normalized;
                _bulletSpawner.ShootBullet(_bulletPlayerConfig, _aimingPosition, vectorVelocityNorm);
                
                return true;
            }
            else
            {
                Debug.LogWarning("Can't Hit Target");
                return false;
            }
        }

        private bool GetClosestEnemy()
        {
            GameObject[] arrEnemies = _enemyActiveTracker.Enemies;
            int numberEnemies = arrEnemies.Length;
            if (numberEnemies == 0) return false;
            
            float minSqrtDistance = float.MaxValue;
            int idxClosestEnemyInRange = -1;
            for (int idx = 0; idx < numberEnemies; idx++)
            {
                Vector3 enemyPosition = arrEnemies[idx].transform.position;
                float sqrtDistance = (enemyPosition - _aimingPosition).sqrMagnitude;
                if (sqrtDistance > _sqrtShootDistance)
                {
                    continue;
                }

                if (sqrtDistance < minSqrtDistance)
                {
                    minSqrtDistance = sqrtDistance;
                    idxClosestEnemyInRange = idx;
                }
            }

            if (idxClosestEnemyInRange < 0) return false;
            
            _closestEnemy = arrEnemies[idxClosestEnemyInRange];
            return true;
        }
    }
}
