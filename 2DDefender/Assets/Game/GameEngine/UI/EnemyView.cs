using TMPro;
using UnityEngine;

namespace GameEngine.UI
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textHealth;

        public void SetEnemyCount(string valueHealth)
        {
            _textHealth.text = valueHealth;
        }
    }
}
