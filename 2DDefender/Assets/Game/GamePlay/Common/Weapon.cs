using UnityEngine;

namespace GamePlay.Common
{
    internal sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Transform _aimingPosition;
        
        internal Vector3 AimingPosition => _aimingPosition.position;
        internal Vector3 BulletPosition => _firePoint.position;

        internal Quaternion Rotation => _firePoint.rotation;
    }

}