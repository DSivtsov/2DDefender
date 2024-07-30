using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Player
{
    public class RotateRifle : MonoBehaviour
    {
    
        [SerializeField] private Transform _transformRifle;
        [Header("Demo")]
        [SerializeField] private int angleZ;

        private AttackController _attackController;


        [Inject]
        internal void Construct(AttackController attackController)
        {
            _attackController = attackController;
        }
        
        public void TurnRifleOnTarget()
        {
            _transformRifle.rotation = GetAngleShoot();
        }

        public void TurnRifleOnIdle()
        {
            _transformRifle.rotation = Quaternion.identity;
        }

        private Quaternion GetAngleShoot() => Quaternion.FromToRotation(Vector3.up, _attackController.ShootDirection);
    }
}