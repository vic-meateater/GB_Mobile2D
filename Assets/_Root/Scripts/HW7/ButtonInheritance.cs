using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class ButtonInheritance : Button
{
    public static string CustomComponent => nameof(_customComponent);
    
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _customComponent = 0.6f;



    protected override void Awake()
    {
        base.Awake();
    }
    
    protected override void OnValidate()
    {
        base.OnValidate();
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        Debug.Log("OnPointerClick");
    }
}


