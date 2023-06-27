using Feature.Fight;
using Profile;
using Tool;
using UnityEngine;

namespace Game.InGameMenu
{
    internal class StartInGameMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/InGameMenu/StartInGameMenuView");

        private readonly StartInGameMenuView _view;
        private readonly ProfilePlayer _profilePlayer;
        private readonly Transform _placeForUi;
        private GameMenuController _gameMenuController;


        public StartInGameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _view = LoadView(placeForUi);
            Subscribe(_view);
        }

        private void OnDestroy()
        {
            Unsubscribe(_view);
        }


        private StartInGameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<StartInGameMenuView>();
        }
        private void Subscribe(StartInGameMenuView view)
        {
            view.InGameMenuButton.onClick.AddListener(OnInGameMenu);
            view.PauseButton.onClick.AddListener(OnPause);
        }

        private void Unsubscribe(StartInGameMenuView view)
        {
            view.InGameMenuButton.onClick.RemoveListener(OnInGameMenu);
            view.PauseButton.onClick.RemoveListener(OnPause);
        }

        private void OnPause() =>
            Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        

        private void OnInGameMenu()
        {
            _gameMenuController = new GameMenuController(_placeForUi, _profilePlayer);
            AddDisposable(_gameMenuController);
        }

    }
}
