using Tool.Ads.UnityAds.Settings;
using UnityEngine;

namespace Tool.Ads.UnityAds
{
    [CreateAssetMenu(fileName = nameof(UnityAdsSettings), menuName = "Settings/Ads/" + nameof(UnityAdsSettings))]
    internal class UnityAdsSettings : ScriptableObject
    {
        [Header("Game ID")]
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _iosGameId;

        [field: Header("Players")]
        [field: SerializeField] public AdsPlayerSettings Interstitial { get; private set; }
        [field: SerializeField] public AdsPlayerSettings Rewarded { get; private set; }
        [field: SerializeField] public AdsPlayerSettings Banner { get; private set; }

        [field: Header("Settings")]
        [field: SerializeField] public bool TestMode { get; private set; } = true;


        public string GameId =>
#if UNITY_EDITOR
            _androidGameId;
#else
            Application.platform switch
            {
                RuntimePlatform.Android => _androidGameId,
                RuntimePlatform.IPhonePlayer => _iosGameId,
                _ => ""
            };
#endif
    }
}