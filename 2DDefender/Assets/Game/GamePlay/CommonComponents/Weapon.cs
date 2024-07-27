using UnityEngine;

namespace GamePlay.Common
{
    internal sealed class Weapon : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;

        internal Vector2 Position => _firePoint.position;

        internal Quaternion Rotation => _firePoint.rotation;
    }

}