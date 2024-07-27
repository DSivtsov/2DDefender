//#define TRACE_WARNINGS
using System;
using UnityEngine;

namespace Game.Modules.Utils
{
    public class TorpedoTriangleSolver
    {

        public static Vector3 GetTorpedoVelocity(Vector3 targetVelocity, float torpedoSpeed, Vector3 targetPosition, Vector3 attackerPosition)
        {
            Vector3 DirectionToTargetNorm = (targetPosition - attackerPosition).normalized;
            float targetOrthoSpeed = Vector3.Dot(targetVelocity, DirectionToTargetNorm);
            Vector3 targetOrthoVelocity = targetOrthoSpeed * DirectionToTargetNorm;
            Vector3 targetTangVelocity = targetVelocity - targetOrthoVelocity;

            float targetTangSpeed = targetTangVelocity.magnitude;

            if (targetTangSpeed > torpedoSpeed)
            {
#if TRACE_WARNINGS
                Debug.LogWarning($"Can't hit targetTangSpeed[{targetTangSpeed}] > torpedoSpeed[{torpedoSpeed}]");
#endif
                return Vector3.zero;
            }
            
            float torpedoTangSpeed = targetTangSpeed;
            
            float torpedoOrthoSpeed =
                (float)Math.Sqrt(torpedoSpeed * torpedoSpeed - torpedoTangSpeed * torpedoTangSpeed);

            if (targetOrthoSpeed >= torpedoOrthoSpeed)
            {
#if TRACE_WARNINGS
                Debug.LogWarning($"Can't hit targetOrthoSpeed[{targetOrthoSpeed}] > torpedoOrthoSpeed[{torpedoOrthoSpeed}]");
#endif
                return Vector3.zero;
            }
            
            Vector3 torpedoOrthoVelocity = torpedoOrthoSpeed * DirectionToTargetNorm;

            Vector3 torpedoVelocity = targetTangVelocity + torpedoOrthoVelocity;

            return torpedoVelocity;
        }

    }
}