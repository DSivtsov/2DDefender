using UnityEngine;
using Modules.GameManager;

namespace GamePlay.Player
{
    internal sealed class PlayerObject : MonoBehaviour, IGameStartListener
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        void IGameStartListener.OnStartGame()
        {
            gameObject.SetActive(true);
        }
    }
}