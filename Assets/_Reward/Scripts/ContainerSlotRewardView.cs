using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rewards
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _сurrencyIcon;
        [SerializeField] private TMP_Text _textDays;
        [SerializeField] private TMP_Text _countReward;


        public void SetData(Reward reward, int countDay, bool isSelected)
        {
            _сurrencyIcon.sprite = reward.CurrencyIcon;
            _textDays.text = $"Day {countDay}";
            _countReward.text = reward.CountCurrency.ToString();

            UpdateBackground(isSelected);
        }


        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}