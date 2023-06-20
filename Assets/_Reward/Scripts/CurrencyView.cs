using UnityEngine;

namespace Rewards
{
    internal class CurrencyView : MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        private const string CoinKey = nameof(CoinKey);

        // [filed: SerializeField] public CurrencySlotView _currencyWood;
        // [filed: SerializeField] public CurrencySlotView _currentDiamond;
        // [filed: SerializeField] public CurrencySlotView _currencyCoin;
        
        [filed: SerializeField] public CurrencySlotView Wood { get; private set; }
        [filed: SerializeField] public CurrencySlotView Diamond { get; private set; }
        [filed: SerializeField] public CurrencySlotView Coin { get; private set; }

        // private int Wood
        // {
        //     get => PlayerPrefs.GetInt(WoodKey);
        //     set => PlayerPrefs.SetInt(WoodKey, value);
        // }
        //
        // private int Diamond
        // {
        //     get => PlayerPrefs.GetInt(DiamondKey);
        //     set => PlayerPrefs.SetInt(DiamondKey, value);
        // }
        //
        // private int Coin
        // {
        //     get => PlayerPrefs.GetInt(CoinKey);
        //     set => PlayerPrefs.SetInt(CoinKey, value);
        // }
        
        

        private void Start()
        {
            //UpdateRewardsData();
        }

        public void AddReward(string key, int value, CurrencySlotView currency)
        {
            key += value;
            PlayerPrefs.SetInt(key, value);
            //UpdateRewardsData();
            currency.SetData(PlayerPrefs.GetInt(key));
        }

        // public void AddWood(int value)
        // {
        //     Wood += value;
        //     _currencyWood.SetData(Wood);
        // }
        //
        // public void AddDiamond(int value)
        // {
        //     Diamond += value;
        //     _currentDiamond.SetData(Diamond);
        // }
        //
        // public void AddCoin(int value)
        // {
        //     Coin += value;
        //     _currencyCoin.SetData(Coin);
        // }

        // private void UpdateRewardsData()
        // {
        //     Wood.SetData(Wood);
        //     _currentDiamond.SetData(Diamond);
        //     _currencyCoin.SetData(Coin);
        // }
    }
}