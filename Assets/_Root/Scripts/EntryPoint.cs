using Profile;
using System;
using System.Collections.Generic;
using Tool.Ads.UnityAds;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 3f;
    private const float JumpHeight = 2f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpHeight, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager);

        Analytics.CustomEvent("MainMenuOpenedFromStart", new Dictionary<string, object>()
        {
            ["Car speed"] = SpeedCar,
        });
        _analyticsManager.SendMainMenuOpenedEvent();

        if (UnityAdsService.Instance.IsInitialized)
        {
            OnAdsInitialized();
        }
        else
        {
            UnityAdsService.Instance.Initialized.AddListener(OnAdsInitialized);
        }
    }

    private void OnAdsInitialized() => UnityAdsService.Instance.InterstitialPlayer.Play();

    private void OnDestroy()
    {
      //  UnityAdsService.Instance.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }
}
