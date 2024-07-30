using System;

namespace Modules.Utils
{
    /*
     * work in First or Second Quadrant only and if Target moved down along Axis Y
     */
    public class TorpedoTrianglePartial
    {
        private float _speedTarget;
        private float _speedTorpedo;
        private float _deltaX;
        private float _deltaY;
        private float _fractionSpeed;
        private float _fractionDistance;
        private SolveQuadratic _solveQuadratic;

        public TorpedoTrianglePartial(float speedTarget, float speedTorpedo, float deltaX, float deltaY)
        {
            _speedTarget = speedTarget;
            _speedTorpedo = speedTorpedo;
            _deltaX = deltaX;
            _deltaY = deltaY;
            _fractionSpeed = - Math.Abs(_speedTarget) / _speedTorpedo;
            _fractionDistance = _deltaY / _deltaX;
            float a = _fractionDistance * _fractionDistance + 1;
            float b = 2 * _fractionDistance * _fractionSpeed;
            float c = _fractionSpeed * _fractionSpeed - 1;
            _solveQuadratic = new SolveQuadratic(a, b, c);
        }

        public (float angle, float deltaT) GetSolution()
        {
            if (_solveQuadratic.IsNoRoot)
            {
                throw new NotImplementedException();
            }
            // foreach (float root in _solveQuadratic.GetRoots())
            //     Debug.Log($"[SolveTorpedoTriangle]: root={root} deltaT={_deltaX / (_speedTorpedo * root)}" +
            //               $" angle={Mathf.Rad2Deg * (float)System.Math.Asin(root)}");;
            foreach (float root in _solveQuadratic.GetRoots())
            {
                if (!(root > 0)) continue;
                
                float deltaT = _deltaX / (_speedTorpedo * root);
                float angle = (float)System.Math.Asin(root); 
                return (angle, deltaT);
            }
            
            throw new NotImplementedException();
        }
    }
}