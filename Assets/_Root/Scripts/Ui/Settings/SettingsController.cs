using Tool;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new("Prefabs/SettingsMenu");
        private readonly SettingsView _view;
        private readonly ProfilePlayer _profilePlayer;

        public SettingsController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _view = LoadView(placeForUi);
            _view.Init(OpenMainMenu);
            _profilePlayer = profilePlayer;
        }

        private SettingsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsView>();
        }

        private void OpenMainMenu()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
    }
}