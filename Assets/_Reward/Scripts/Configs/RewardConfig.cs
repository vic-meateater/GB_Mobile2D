﻿using Rewards;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RewardConfig), menuName = "Configs/Rewards/" + nameof(RewardConfig))]
internal class RewardConfig: ScriptableObject
{
    [field: SerializeField] public RewardType RewardType { get; private set; }
    [field: SerializeField] public Sprite CurrencyIcon { get; private set; }
    [field: SerializeField] public int CountCurrency { get; private set; }
    [field: Header("Settings Time Get Reward")]
    [field: SerializeField] public float TimeCooldown { get; private set; } = 86400;
    [field: SerializeField] public float TimeDeadline { get; private set; } = 172800;
}