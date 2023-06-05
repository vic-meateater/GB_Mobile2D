using Profile;
using System;
using System.Collections.Generic;
using Tool.Ads.UnityAds;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;
    [SerializeField] private UnityAdsService _adsService;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager);

        Analytics.CustomEvent("MainMenuOpenedFromStart", new Dictionary<string, object>()
        {
            ["Car speed"] = SpeedCar,
        });
        _analyticsManager.SendMainMenuOpenedEvent();

        if (_adsService.IsInitialized)
        {
            OnAdsInitialized();
        }
        else
        {
            _adsService.Initialized.AddListener(OnAdsInitialized);
        }
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }
}
