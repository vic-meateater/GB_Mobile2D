using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Fight
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _removeMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _removeHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addForceButton;
        [SerializeField] private Button _removeForceButton;

        [Header("Crime Buttons")]
        [SerializeField] private Button _addCrimeButton;
        [SerializeField] private Button _removeCrimeButton;

        
        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _skipButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _force;
        private PlayerData _crime;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _force = CreatePlayerData(DataType.Force);
            _crime = CreatePlayerData(DataType.Crime);
            
            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _force);
            DisposePlayerData(ref _crime);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _removeMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _removeHealthButton.onClick.AddListener(DecreaseHealth);

            _addForceButton.onClick.AddListener(IncreasePower);
            _removeForceButton.onClick.AddListener(DecreasePower);

            _addCrimeButton.onClick.AddListener(IncreaseCrime);
            _removeCrimeButton.onClick.AddListener(DecreaseCrime);
            
            _fightButton.onClick.AddListener(Fight);
            _skipButton.onClick.AddListener(Skip);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _removeMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _removeHealthButton.onClick.RemoveAllListeners();

            _addForceButton.onClick.RemoveAllListeners();
            _removeForceButton.onClick.RemoveAllListeners();
            
            _addCrimeButton.onClick.RemoveAllListeners();
            _removeCrimeButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
            _skipButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_force);
        private void DecreasePower() => DecreaseValue(_force);        
        
        private void IncreaseCrime() => IncreaseValue(_crime);
        private void DecreaseCrime() => DecreaseValue(_crime);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }


        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            if (dataType == DataType.Crime)
            {
                playerData.Value = _enemy.CalcCrime(ref value);
                VisibleSkipButton(playerData.Value);
            }
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";


            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Force => _countPowerText,
                DataType.Crime => _countCrimeText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void VisibleSkipButton(int crimeCount) =>
            _skipButton.gameObject.SetActive(crimeCount <= 2);
        

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _force.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
        
        private void Skip()
        {
            Debug.Log($"Player skipped from fight. lol");
        }
    }
}