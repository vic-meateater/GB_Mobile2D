using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonReward;
        [SerializeField] private Button _buttonBuyItem;
        [SerializeField] private Button _buttonGarage;


        public void Init(UnityAction startGame, UnityAction settingsGame, UnityAction getReward, UnityAction buyItem, UnityAction enterGarage)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsGame);
            _buttonReward.onClick.AddListener(getReward);
            _buttonBuyItem.onClick.AddListener(buyItem);
            _buttonGarage.onClick.AddListener(enterGarage);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonReward.onClick.RemoveAllListeners();
            _buttonBuyItem.onClick.RemoveAllListeners();
            _buttonGarage.onClick.RemoveAllListeners();
        }
    }
}
