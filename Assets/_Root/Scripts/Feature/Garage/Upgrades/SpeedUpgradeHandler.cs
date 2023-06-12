namespace Feature.Garage.Upgrade
{
    internal class SpeedUpgradeHandler : IUpgradeHandler
    {
        private readonly float _speed;

        public SpeedUpgradeHandler(float speed) =>
            _speed = speed;

        public void Upgrade(IUpgradable upgradable) =>
            upgradable.Speed += _speed;
    }
}