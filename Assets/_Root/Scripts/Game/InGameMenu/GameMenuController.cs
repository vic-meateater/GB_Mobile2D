using Tool;
using Profile;
using UnityEngine;

namespace Game.InGameMenu
{
    internal class GameMenuController: BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/InGameMenu/InGameMenuView");

        private readonly GameMenuView _view;
        private readonly ProfilePlayer _profilePlayer;


        public GameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            Subscribe(_view);
        }

        private void OnDestroy()
        {
            Unsubscribe(_view);
        }

        private GameMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameMenuView>();
        }

        private void Subscribe(GameMenuView view)
        {
            view.BackToMainButton.onClick.AddListener(BackToMainMenu);
        }
        private void Unsubscribe(GameMenuView view)
        {
            view.BackToMainButton.onClick.RemoveListener(BackToMainMenu);
        }


        private void BackToMainMenu() => 
            _profilePlayer.CurrentState.Value = GameState.Start;

    }
}
