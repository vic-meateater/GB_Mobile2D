using Tool;
using Profile;
using UnityEngine;

namespace Game.InGameMenu
{
    internal class GameMenuController: BaseController
    {
        private const float START_TIME = 1f;
        private const float STOP_TIME = 0f;
        
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
            Time.timeScale = STOP_TIME;

            return objectView.GetComponent<GameMenuView>();
        }

        private void Subscribe(GameMenuView view)
        {
            view.BackToMainButton.onClick.AddListener(BackToMainMenu);
            view.CloseButton.onClick.AddListener(Close);
        }
        private void Unsubscribe(GameMenuView view)
        {
            view.BackToMainButton.onClick.RemoveListener(BackToMainMenu);
            view.CloseButton.onClick.RemoveListener(Close);
        }


        private void BackToMainMenu()
        {
            CheckTimescale();
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private void Close()
        {
            CheckTimescale();
            Dispose();
        }

        private void CheckTimescale()
        {
            Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        }
    }
}
