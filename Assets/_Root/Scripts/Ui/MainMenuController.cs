using Profile;
using Tool;
using Tool.Ads.UnityAds;
using Tool.IAP;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, SettingsGame, GetReward, BuyItem, EnterGarage);
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
        private void GetReward() => UnityAdsService.Instance.RewardedPlayer.Play();
        private void BuyItem() => IAPService.Instance.Buy("Golds");
        private void EnterGarage() =>_profilePlayer.CurrentState.Value = GameState.Garage;
    }
}
