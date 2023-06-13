using Profile;
using System;
using System.Collections.Generic;
using Tool.Ads.UnityAds;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const GameState InitialState = GameState.Start;

    [SerializeField] private float _speedCar = 15f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(_speedCar, _jumpHeight, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer, _analyticsManager);

        Analytics.CustomEvent("MainMenuOpenedFromStart", new Dictionary<string, object>()
        {
            ["Car speed"] = _speedCar,
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
        UnityAdsService.Instance.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }
}
