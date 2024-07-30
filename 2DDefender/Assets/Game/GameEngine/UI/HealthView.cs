using TMPro;
using UnityEngine;

namespace GameEngine.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textHealth;

        public void SetViewHeath(string valueHealth)
        {
            _textHealth.text = valueHealth;
        }
    }
}
