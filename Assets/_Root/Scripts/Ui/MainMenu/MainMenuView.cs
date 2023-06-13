using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonPlayRewarded;
        [SerializeField] private Button _buttonPurchase;
        public void Init(UnityAction startGame, UnityAction openSettings, UnityAction playRewarded, UnityAction buyGoldToken)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(openSettings);
            _buttonPlayRewarded.onClick.AddListener(playRewarded);
            _buttonPurchase.onClick.AddListener(buyGoldToken);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
        }
    }
}