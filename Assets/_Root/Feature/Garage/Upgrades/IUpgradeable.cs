namespace Feature.Garage.Upgrade
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        void Restore();
    }
}
