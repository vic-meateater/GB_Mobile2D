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


        public void Init(UnityAction startGame, UnityAction settingsGame, UnityAction getReward, UnityAction buyItem)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsGame);
            _buttonReward.onClick.AddListener(getReward);
            _buttonBuyItem.onClick.AddListener(buyItem);
        }

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}
