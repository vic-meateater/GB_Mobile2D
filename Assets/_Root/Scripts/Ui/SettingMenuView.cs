using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;

    private UnityAction _backToMain;

    public void Init(UnityAction backToMain)
    {
        _backToMain = backToMain;
        _buttonBack.onClick.AddListener(backToMain);
    }

    public void OnDestroy() =>
        _buttonBack.onClick.RemoveListener(_backToMain);
}
