using GameEngine.Common;
using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    public enum State
    {
        Idle,
        WalkUp,
        WalkDown,
        WalkLeft,
        WalkRight,
        Fire,
    }

    public sealed class PlayerObject : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private int _initialHitPoints;
        private IDamageable _damageable;

        private void Awake()
        {
            gameObject.SetActive(false);
            _damageable = GetComponent<IDamageable>();
            _initialHitPoints = _damageable.HitPoints;
        }

        void IGameStartListener.OnStartGame()
        {
            gameObject.SetActive(true);
            _damageable.SetInitialHitPoints(_initialHitPoints);
        }

        void IGameFinishListener.OnFinishGame()
        {
            gameObject.SetActive(false);
        }
    }
}