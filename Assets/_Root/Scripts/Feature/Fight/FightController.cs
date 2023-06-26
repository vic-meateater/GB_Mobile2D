using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;
        
        
        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _force;
        private PlayerData _crime;

        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _enemy = new Enemy("Enemy Flappy");

            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _force = CreatePlayerData(DataType.Force);
            _crime = CreatePlayerData(DataType.Crime);
            
            Subscribe(_view);
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _force);
            DisposePlayerData(ref _crime);

            Unsubscribe(_view);
        }

        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
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


        private void Subscribe(FightView view)
        {
            view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            view.RemoveMoneyButton.onClick.AddListener(DecreaseMoney);

            view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            view.RemoveHealthButton.onClick.AddListener(DecreaseHealth);

            view.AddForceButton.onClick.AddListener(IncreasePower);
            view.RemoveForceButton.onClick.AddListener(DecreasePower);

            view.AddCrimeButton.onClick.AddListener(IncreaseCrime);
            view.RemoveCrimeButton.onClick.AddListener(DecreaseCrime);
            
            view.FightButton.onClick.AddListener(Fight);
            view.SkipButton.onClick.AddListener(Skip);
        }

        private void Unsubscribe(FightView view)
        {
            view.AddMoneyButton.onClick.RemoveAllListeners();
            view.RemoveMoneyButton.onClick.RemoveAllListeners();

            view.AddHealthButton.onClick.RemoveAllListeners();
            view.RemoveHealthButton.onClick.RemoveAllListeners();

            view.AddForceButton.onClick.RemoveAllListeners();
            view.RemoveForceButton.onClick.RemoveAllListeners();
            
            view.AddCrimeButton.onClick.RemoveAllListeners();
            view.RemoveCrimeButton.onClick.RemoveAllListeners();

            view.FightButton.onClick.RemoveAllListeners();
            view.SkipButton.onClick.RemoveAllListeners();
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
            _view.CountForceText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Force => _view.CountForceText,
                DataType.Crime => _view.CountCrimeText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void VisibleSkipButton(int crimeCount) =>
            _view.SkipButton.gameObject.SetActive(crimeCount <= 2);
        

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
            Close();
        }
        
        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}