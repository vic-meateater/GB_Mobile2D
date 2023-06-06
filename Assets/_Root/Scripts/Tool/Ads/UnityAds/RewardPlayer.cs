using Tool.Ads.UnityAds;
using UnityEngine.Advertisements;

internal sealed class RewardPlayer : UnityAdsPlayer
{
    public RewardPlayer(string id) : base(id) { }
    protected override void OnPlaying() => Advertisement.Show(Id, this);
    protected override void Load() => Advertisement.Load(Id, this);
}