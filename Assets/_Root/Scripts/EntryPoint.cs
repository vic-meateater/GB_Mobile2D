using Profile;
using System.Collections.Generic;
using Tool.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private AnalyticsManager _analyticsManager;

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
    }

    

    private void OnDestroy()
    {
        _mainController.Dispose();
    }
}
