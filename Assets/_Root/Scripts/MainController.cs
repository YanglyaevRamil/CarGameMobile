using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private SettingsController _settingsController;
    private AnalyticsManager _analyticsManager;
    private UnityAdsService _adsService;
    private IAPService _IAPService;
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analyticsManager, UnityAdsService adsService, IAPService IAPService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analyticsManager = analyticsManager;
        _adsService = adsService;
        _IAPService = IAPService;
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _IAPService);
                _gameController?.Dispose();
                _settingsController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer);
                _analyticsManager.SendStartGame();
                _mainMenuController?.Dispose();
                break;
            case GameState.Settings:
                _settingsController = new SettingsController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}