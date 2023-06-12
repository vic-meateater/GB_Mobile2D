namespace Feature.Garage.Upgrade
{
    internal class DefaultUpgradeHandler : IUpgradeHandler
    {
        public static readonly IUpgradeHandler Default = new DefaultUpgradeHandler();

        public void Upgrade(IUpgradable upgradable) { }
    }
}
