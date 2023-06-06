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


        public void Init(UnityAction startGame, UnityAction settingsGame, UnityAction getReward)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settingsGame);
            _buttonReward.onClick.AddListener(getReward);
        }

        public void OnDestroy() =>
            _buttonStart.onClick.RemoveAllListeners();
    }
}
