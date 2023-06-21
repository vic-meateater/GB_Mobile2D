using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    internal class DailyRewardController
    {
        private readonly DailyRewardView _dailyRewardView;
        private readonly CurrencyView _currencyView;
        private readonly RewardsConfig _rewardsList;

        private List<ContainerSlotRewardView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;
        private bool _isInitialized;


        public DailyRewardController(DailyRewardView dailyRewardView, CurrencyView currencyView, GameConfig gameConfig)
        {
            _dailyRewardView = dailyRewardView;
            _currencyView = currencyView;
            _rewardsList = gameConfig.RewardsConfig;
        }


        public void Init()
        {
            if (_isInitialized)
                return;

            InitSlots();
            RefreshUi();
            StartRewardsUpdating();
            SubscribeButtons();

            _isInitialized = true;
        }

        public void Deinit()
        {
            if (!_isInitialized)
                return;

            DeinitSlots();
            StopRewardsUpdating();
            UnsubscribeButtons();

            _isInitialized = false;
        }


        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (int i = 0; i < _rewardsList.Rewards.Count; i++)
            {
                ContainerSlotRewardView instanceSlot = CreateSlotRewardView();
                _slots.Add(instanceSlot);
            }
        }

        private ContainerSlotRewardView CreateSlotRewardView() =>
            Object.Instantiate
            (
                _dailyRewardView.ContainerSlotRewardPrefab,
                _dailyRewardView.MountRootSlotsReward,
                false
            );

        private void DeinitSlots()
        {
            foreach (ContainerSlotRewardView slot in _slots)
                Object.Destroy(slot.gameObject);

            _slots.Clear();
        }


        private void StartRewardsUpdating() =>
            _coroutine = _dailyRewardView.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _dailyRewardView.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }


        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetRewardsState);
        }

        private void UnsubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.RemoveListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.RemoveListener(ResetRewardsState);
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            RewardConfig reward = _rewardsList.Rewards[_dailyRewardView.CurrentSlotInActive];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    _currencyView.AddWood(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    _currencyView.AddDiamond(reward.CountCurrency);
                    break;
                case RewardType.Coin:
                    _currencyView.AddCoin(reward.CountCurrency);
                    break;
            }

            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.CurrentSlotInActive++;

            RefreshRewardsState();
        }


        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _dailyRewardView.TimeGetReward.HasValue;
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            foreach (var reward in _rewardsList.Rewards)
            {
                TimeSpan timeFromLastRewardGetting =
                    DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

                bool isDeadlineElapsed =
                    timeFromLastRewardGetting.Seconds >= reward.TimeDeadline;

                bool isTimeToGetNewReward =
                    timeFromLastRewardGetting.Seconds >= reward.TimeCooldown;

                if (isDeadlineElapsed)
                    ResetRewardsState();

                _isGetReward = isTimeToGetNewReward;
            }
        }

        private void ResetRewardsState()
        {
            _dailyRewardView.TimeGetReward = null;
            _dailyRewardView.CurrentSlotInActive = 0;
        }


        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;
            _dailyRewardView.TimerNewReward.text = GetTimerNewRewardText();
            RefreshSlots();
        }

        private string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_dailyRewardView.TimeGetReward.HasValue)
            {
                foreach (RewardConfig reward in _rewardsList.Rewards)
                {
                    DateTime nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(reward.TimeCooldown);
                    TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    string timeGetReward =
                        $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                        $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                    return $"Time to get the next reward: {timeGetReward}";
                    
                }


            }

            return string.Empty;
        }

        private void RefreshSlots()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                RewardConfig reward = _rewardsList.Rewards[i];
                int countDay = i + 1;
                bool isSelected = i == _dailyRewardView.CurrentSlotInActive;

                _slots[i].SetData(reward, countDay, isSelected);
            }
        }
    }
}