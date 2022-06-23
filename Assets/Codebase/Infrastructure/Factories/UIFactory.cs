using Codebase.Infrastructure.AssetManagement;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.UI;
using Codebase.UI.Windows;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _inputService;
    private readonly IPauseService _pauseService;

    private Transform _rootCanvas;

    public UIFactory(
      IAssetProvider assetProvider,
      IStaticDataService staticData,
      IInputService inputService,
      IPauseService pauseService
    )
    {
      _assetProvider = assetProvider;
      _staticData = staticData;
      _inputService = inputService;
      _pauseService = pauseService;
    }

    public void CreateRootCanvas()
    {
      GameObject rootCanvas = _assetProvider.Instantiate(AssetPath.RootCanvas);
      rootCanvas.GetComponent<InputListener>().Construct(this, _inputService);
      _rootCanvas = rootCanvas.transform;
    }

    public void CreatePauseWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Pause);
      PauseWindow window = Object.Instantiate(config.Template, _rootCanvas) as PauseWindow;
      window.Construct(_pauseService);
    }
  }
}