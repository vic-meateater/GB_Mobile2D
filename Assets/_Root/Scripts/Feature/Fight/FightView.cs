using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Fight
{
    internal class FightView : MonoBehaviour
    {
        [field: Header("Player Stats")]
        [field: SerializeField] public TMP_Text CountMoneyText{ get; private set; }
        [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
        [field: SerializeField] public TMP_Text CountForceText { get; private set; }
        [field: SerializeField] public TMP_Text CountCrimeText { get; private set; }

        [field: Header("Enemy Stats")]
        [field: SerializeField] public TMP_Text CountForceEnemyText { get; private set; }

        [field: Header("Money Buttons")]
        [field: SerializeField] public Button AddMoneyButton { get; private set; }
        [field: SerializeField] public Button RemoveMoneyButton { get; private set; }

        [field: Header("Health Buttons")]
        [field: SerializeField] public Button AddHealthButton { get; private set; }
        [field: SerializeField] public Button RemoveHealthButton { get; private set; }

        [field: Header("Power Buttons")]
        [field: SerializeField] public Button AddForceButton { get; private set; }
        [field: SerializeField] public Button RemoveForceButton { get; private set; }

        [field: Header("Crime Buttons")]
        [field: SerializeField] public Button AddCrimeButton { get; private set; }
        [field: SerializeField] public Button RemoveCrimeButton { get; private set; }

        [field: Header("Other Buttons")]
        [field: SerializeField] public Button FightButton { get; private set; }
        [field: SerializeField] public Button SkipButton { get; private set; }
    }
}
