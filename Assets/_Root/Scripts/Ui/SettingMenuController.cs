using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

internal class SettingMenuController: BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/SettingMenu");
    private readonly ProfilePlayer _profilePlayer;
    private readonly SettingMenuView _view;

    public SettingMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(BackToMain);
    }

    private SettingMenuView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<SettingMenuView>();
    }
    private void BackToMain() =>
        _profilePlayer.CurrentState.Value = GameState.Start;
}