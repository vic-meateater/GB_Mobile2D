using UnityEngine;
using UnityEngine.UI;

internal class GameMenuView : MonoBehaviour
{
    [field: SerializeField] public Button BackToMainButton { get; private set; }
    [field: SerializeField] public Button CloseButton { get; private set; }
}