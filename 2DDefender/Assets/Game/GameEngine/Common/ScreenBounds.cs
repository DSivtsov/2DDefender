using UnityEngine;

namespace GameEngine.Common
{
    public sealed class ScreenBounds : MonoBehaviour
    {
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;
        [SerializeField] private Transform downBorder;
        [SerializeField] private Transform topBorder;

        private float _leftBorderPosX;
        private float _rightBorderPosX;
        private float _downBorderPosY;
        private float _topBorderPosY;

        private void Awake()
        {
            _leftBorderPosX = leftBorder.position.x;
            _rightBorderPosX = rightBorder.position.x;
            _downBorderPosY = downBorder.position.y;
            _topBorderPosY = topBorder.position.y;
        }

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _leftBorderPosX
                   && positionX < _rightBorderPosX
                   && positionY > _downBorderPosY
                   && positionY < _topBorderPosY;
        }

        public bool InHorizontalBounds(float positionX)
        {
            return positionX > _leftBorderPosX
                   && positionX < _rightBorderPosX;
        }
    }
}