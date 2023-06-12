using Feature.Inventory;
using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly IInventoryModel Inventory;


        public ProfilePlayer(float speedCar, float jumpHeight, GameState initialState) : this(speedCar, jumpHeight)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeight);
            Inventory = new InventoryModel();
        }
    }
}
