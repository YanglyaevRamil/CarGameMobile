using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _buttonBack;

        public void Init(UnityAction openSettings)
        {
            _buttonBack.onClick.AddListener(openSettings);
        }

        public void OnDestroy()
        {
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}