using UnityEngine;

namespace Rewards
{
    internal class CurrencyView : MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        private const string CoinKey = nameof(CoinKey);

        [SerializeField] public CurrencySlotView _currencyWood;
        [SerializeField] public CurrencySlotView _currentDiamond;
        [SerializeField] public CurrencySlotView _currencyCoin;


        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }

        private int Diamond
        {
            get => PlayerPrefs.GetInt(DiamondKey);
            set => PlayerPrefs.SetInt(DiamondKey, value);
        }

        private int Coin
        {
            get => PlayerPrefs.GetInt(CoinKey);
            set => PlayerPrefs.SetInt(CoinKey, value);
        }



        private void Start()
        {
            UpdateRewardsData();
        }

        public void AddWood(int value)
        {
            Wood += value;
            _currencyWood.SetData(Wood);
        }

        public void AddDiamond(int value)
        {
            Diamond += value;
            _currentDiamond.SetData(Diamond);
        }

        public void AddCoin(int value)
        {
            Coin += value;
            _currencyCoin.SetData(Coin);
        }

        private void UpdateRewardsData()
        {
            _currencyWood.SetData(Wood);
            _currentDiamond.SetData(Diamond);
            _currencyCoin.SetData(Coin);
        }
    }
}