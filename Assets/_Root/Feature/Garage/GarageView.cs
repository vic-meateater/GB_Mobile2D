using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Feature.Garage
{
    internal interface IGarageView
    {
        void Init(UnityAction apply, UnityAction back);
        void Deinit();
    }
    internal class GarageView : MonoBehaviour, IGarageView
    {
        [SerializeField] private Button _buttonApply;
        [SerializeField] private Button _buttonBack;

        private void OnDestroy() => Deinit();

        public void Init(UnityAction apply, UnityAction back)
        {
            _buttonApply.onClick.AddListener(apply);
            _buttonApply.onClick.AddListener(back);
        }
        public void Deinit()
        {
            _buttonApply.onClick.RemoveAllListeners();
            _buttonBack.onClick.RemoveAllListeners();
        }
    }
}
