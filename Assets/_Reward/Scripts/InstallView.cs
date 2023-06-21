using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private CurrencyView _currencyView;
        [SerializeField] private GameConfig _gameConfig;

        private DailyRewardController _dailyRewardController;


        private void Awake() =>
            _dailyRewardController = new DailyRewardController(_dailyRewardView, _currencyView, _gameConfig);

        private void Start() =>
            _dailyRewardController.Init();

        private void OnDestroy() =>
            _dailyRewardController.Deinit();
    }
}
