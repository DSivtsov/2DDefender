using GameEngine.Common;
using Modules.GameManager;
using UnityEngine;

namespace GamePlay.Player
{
    public class DefendedWallObject : MonoBehaviour
    {
        public GameObject Wall => _wall;
        
        [SerializeField] private GameObject _wall;
        [SerializeField] private int _damageByBlowUp = 10;
        
        private PlayerObject _playerObject;
    
        [Inject]
        internal void Construct(PlayerObject playerObject)
        {
            _playerObject = playerObject;
        }

        public void HitWallBlowUp()
        {
            _playerObject.GetComponent<IDamageable>().TakeDamage(_damageByBlowUp);
        }
    }
}