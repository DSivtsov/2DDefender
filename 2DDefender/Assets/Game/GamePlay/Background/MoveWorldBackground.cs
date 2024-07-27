using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Background
{
    internal sealed class MoveWorldBackground : MonoBehaviour, IGameFixedUpdateListener
    {   
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
            _positionX = _myTransform.position.x;
            _positionZ = _myTransform.position.z;
        }

        private void Move(float fixedDeltaTime)
        {
            if (_myTransform.position.y <= _endPositionY)
                _myTransform.position = new Vector3(_positionX, _startPositionY, _positionZ);

            _myTransform.position -= new Vector3(_positionX, _movingSpeedY * fixedDeltaTime, _positionZ);
        }

        void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime) => Move(fixedDeltaTime);
    }
}