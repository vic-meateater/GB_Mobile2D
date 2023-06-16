using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KPower = 1.5f;
        private const float KMaxHealthPlayer = 20;
        private const int KMaxCrime = 5;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _crimePlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Force:
                    _powerPlayer = playerData.Value;
                    break;

                case DataType.Crime:
                    _crimePlayer = playerData.Value;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _moneyPlayer / KMoney;
            float powerRatio = _powerPlayer / KPower;


            return (int) (moneyRatio + kHealth + powerRatio + Random.Range(0, _crimePlayer));
        }

        public int CalcCrime(ref int crimePlayerResult)
        {
            if (_crimePlayer > KMaxCrime)
                _crimePlayer = KMaxCrime;
            crimePlayerResult = _crimePlayer;
            return crimePlayerResult;
        }

    private int CalcKHealth() =>
            _healthPlayer > KMaxHealthPlayer ? 100 : 5;

        
    }
}