namespace Feature.Garage.Upgrade
{
    internal class GarageUpgradeHandler : IUpgradeHandler
    {
        public static readonly IUpgradeHandler Default = new GarageUpgradeHandler();

        public void Upgrade(IUpgradable upgradable) { }
    }
}
