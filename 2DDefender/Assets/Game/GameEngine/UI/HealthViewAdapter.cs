using GameEngine.Common;
using GamePlay.Player;
using Modules.GameManager;
using UnityEngine;

namespace GameEngine.UI
{
    public class HealthViewAdapter : MonoBehaviour, IGameInitListener, IGameStartListener, IGameFinishListener
    {
        private HealthView _healthView;
        private IDamageable _iDamageable;

        private void Awake()
        {
            _healthView = GetComponent<HealthView>();
            SetHealth(0);
        }
        
        [Inject]
        public void Constructor (PlayerObject playerObject)
        {
            _iDamageable = playerObject.GetComponent<IDamageable>();
        }

        void IGameInitListener.OnInit()
        {
            SetHealth(_iDamageable.HitPoints);
        }

        void IGameStartListener.OnStartGame()
        {
            _iDamageable.OnChangedHitPoints += SetHealth;
        }

        void IGameFinishListener.OnFinishGame()
        {
            _iDamageable.OnChangedHitPoints += SetHealth;
        }

        private void SetHealth(int heath)
        {
            if (heath > 99)
            {
                _healthView.SetViewHeath("99");
                Debug.LogError("[HealthViewAdapter] field health overflow > 99");
            }
            else
            {
                _healthView.SetViewHeath($"{heath:00}");
            }
        }
    }
}