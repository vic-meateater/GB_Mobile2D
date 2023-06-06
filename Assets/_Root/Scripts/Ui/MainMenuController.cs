using Profile;
using Tool;
using Tool.Ads.UnityAds;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly UnityAdsService _adsService;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService unityAdsService)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _adsService = unityAdsService;
            _view.Init(StartGame, SettingsGame, GetReward);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;
        private void SettingsGame() => 
            _profilePlayer.CurrentState.Value = GameState.Settings;  
        private void GetReward() => _adsService.RewardedPlayer.Play();
    }
}
