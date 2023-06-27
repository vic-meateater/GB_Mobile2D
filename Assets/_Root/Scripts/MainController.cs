using Ui;
using Game;
using Profile;
using UnityEngine;
using Tool.Analytics;
using Feature.Garage;
using Feature.Fight;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly AnalyticsManager _analyticsManager;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private GarageContext _garageContext;
    private SettingMenuController _settingGameController;
    private FightController _fightController;



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
        OnDisposeControllers();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        OnDisposeControllers();

        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingGameController = new SettingMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer);
                _analyticsManager.SendGameStartEvent();
                break;
            case GameState.Garage:
                _garageContext = new GarageContext(_placeForUi, _profilePlayer);
                break;
            case GameState.Fight:
                _fightController = new FightController(_placeForUi, _profilePlayer);
                break;
            default:
                OnDisposeControllers();
                break;
        }
    }

    private void OnDisposeControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingGameController?.Dispose();
        _garageContext?.Dispose();
        _fightController?.Dispose();
    }
}