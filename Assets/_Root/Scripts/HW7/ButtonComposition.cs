using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class ButtonComposition : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Ease _curveEase = Ease.Linear;
    [SerializeField] private float _duration = 0.6f;
    [SerializeField] private float _strength = 30f;

    private Button _button;
    private RectTransform _rectTransform;

    private void OnValidate() => InitComponents();
    private void Awake() => InitComponents();
    private void InitComponents()
    {
        _button ??= GetComponent<Button>();
        _rectTransform ??= GetComponent<RectTransform>();
    }

    private void Start() => _button.onClick.AddListener(OnButtonClick);
    private void OnDestroy() => _button.onClick.RemoveAllListeners();
    private void OnButtonClick() => ActivateAnimation();


    private void ActivateAnimation()
    {
        _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
    }
}
