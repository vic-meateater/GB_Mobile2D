using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Feature.Inventory.Items
{
    internal class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;

        [SerializeField] private GameObject _selectedBackground;
        [SerializeField] private GameObject _unselectedBackground;

        private UnityAction _unityAction;

        private void OnDestroy() => DeInit();

        public void Init(IItem item, UnityAction clickAction)
        {
            _icon.sprite = item.Info.Icon;
            _text.text = item.Info.Title;
            _unityAction = clickAction;
            _button.onClick.AddListener(_unityAction);
        }

        public void DeInit()
        {
            _icon.sprite = null;
            _text.text = string.Empty;
            _button.onClick.RemoveListener(_unityAction);
        }

        public void Select() => SetSelected(true);

        public void Unselect() => SetSelected(false);

        private void SetSelected(bool isSelect)
        {
            _selectedBackground.SetActive(isSelect);
            _unselectedBackground.SetActive(!isSelect);
        }
    }
}

