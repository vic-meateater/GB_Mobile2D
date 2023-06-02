using Ui;
using Game;
using Profile;
using UnityEngine;
using Tool.Analytics;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly AnalyticsManager _analyticsManager;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingMenuController _settingGameController;



    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analyticsManager = analyticsManager;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingGameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingGameController?.Dispose();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingGameController = new SettingMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _analyticsManager.SendGameStartEvent();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _settingGameController?.Dispose();
                break;
        }
    }
}
