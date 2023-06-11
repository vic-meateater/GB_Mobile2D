using UnityEngine.Events;

namespace Feature.Inventory.Items
{
    internal interface IItemView
    {
        void Init(IItem item, UnityAction clickAction);
        void Select();
        void Unselect();
    }
}