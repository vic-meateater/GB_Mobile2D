using UnityEngine;
using UnityEngine.UI;

internal class StartInGameMenuView : MonoBehaviour
{
    [field: SerializeField] public Button InGameMenuButton { get; private set; }
    [field: SerializeField] public Button PauseButton { get; private set; }
}