using Ui;
using Tool;
using Profile;
using Services;
using UnityEngine;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly AbilitiesContext _abilitiesContext;
        private readonly GameMenuController _gameMenuController;


        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController(profilePlayer.CurrentCar);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);
            _abilitiesContext = CreateAbilitiesContext(placeForUi, _carController);
            _gameMenuController = CreateGameMenuController(placeForUi, profilePlayer);

            ServiceRoster.Analytics.SendGameStarted();
        }


        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            TapeBackgroundController tapeBackgroundController = new(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            InputGameController inputGameController = new(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private CarController CreateCarController(CarModel carModel)
        {
            CarController carController = new(carModel);
            AddController(carController);

            return carController;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            AbilitiesContext context = new(placeForUi, abilityActivator);
            AddContext(context);

            return context;
        }

        private GameMenuController CreateGameMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var gameMenuController = new GameMenuController(placeForUi, profilePlayer);
            AddController(gameMenuController);

            return gameMenuController;
        }
    }
}