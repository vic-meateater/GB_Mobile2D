using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonShakeComponent : MonoBehaviour
{
    private bool _isTweening = false;
    private Button _button;
    private Tween _shakeTween;
    private Vector3 _originalPosition;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _originalPosition = _button.transform.position;
    }

    [ContextMenu("Start Tween")]
    private void StartTween()
    {
        if (_isTweening) return;
        _shakeTween = _button.transform.DOShakePosition(1f, 10f, 20, 90f, false, true);
        _isTweening = true;
    }

    [ContextMenu("Stop Tween")]
    private void StopTween()
    {
        if (!_isTweening) return;
        _shakeTween.Kill();
        _button.transform.position = _originalPosition;
        _isTweening = false;
    }
}
