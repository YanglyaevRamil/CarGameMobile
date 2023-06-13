using Tool;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;
using Services.Ads.UnityAds;
using Services.IAP;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly UnityAdsService _adsService;
        private readonly IAPService _IAPService;
        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService adsService, IAPService IAPService)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, OpenSettings, PlayRewarded, BuyGoldToken);
            _adsService = adsService;
            _IAPService = IAPService;
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void OpenSettings()
        {
            _profilePlayer.CurrentState.Value = GameState.Settings;
        }

        private void PlayRewarded()
        {
            _adsService.RewardedPlayer.Play();
        }

        private void BuyGoldToken()
        {
            _IAPService.Buy("product_2");
        }
    }
}