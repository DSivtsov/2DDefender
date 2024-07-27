using Game.Modules.Utils;
using UnityEngine;

namespace Test
{
    public sealed class AttackTarget : MonoBehaviour
    {

        [SerializeField] private MoveBullet _prefabBullet;
        [SerializeField] private float _speedBullet;
        [SerializeField] private MoveTarget _target;

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                TryAttackTarget();
            }
        }

        private bool TryAttackTarget()
        {
            Vector3 shootDirection = TorpedoTriangleSolver.GetTorpedoVelocity(_target.Velocity, _speedBullet,
                _target.Position,  transform.position);

            if (shootDirection != Vector3.zero)
            {
                MoveBullet moveBullet = Instantiate(_prefabBullet);
                moveBullet.SetSpeedBullet(_speedBullet);
                moveBullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, shootDirection);
                return true;
            }
            else
            {
                Debug.LogWarning("Can't Hit Target");
                return false;
            }
        }
    }
}