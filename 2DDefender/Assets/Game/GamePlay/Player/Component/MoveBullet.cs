using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Player
{
    public sealed class MoveBullet : MonoBehaviour
    {
        [ShowInInspector,ReadOnly] private float _speedBullet;
        
        private Transform _transform;
        private Vector3 _velocity;

        private void Awake()
        {
            _transform = transform;
            _speedBullet = 0;
            _velocity = _speedBullet * Vector3.up;
        }
        
        void Update()
        {
            _transform.Translate( _velocity * Time.deltaTime, Space.Self);
        }

        public void SetSpeedBullet(float speedBullet)
        {
            _speedBullet = speedBullet;
            _velocity = _speedBullet * Vector3.up;
        }
    }
}