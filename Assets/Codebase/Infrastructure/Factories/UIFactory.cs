using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.States;
using Codebase.Services.Audio;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;
using Codebase.Services.Window;
using Codebase.StaticData;
using Codebase.UI;
using Codebase.UI.Windows;
using System.Threading.Tasks;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataService _staticData;
    private readonly IInputService _inputService;
    private readonly IPauseService _pauseService;
    private readonly ISavingService _savingService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IAudioService _audioService;
    private readonly IWindowService _windowService;

    private Transform _rootCanvas;

    public UIFactory(
      IAssetProvider assetProvider,
      IStaticDataService staticData,
      IInputService inputService,
      IPauseService pauseService,
      ISavingService savingService,
      IGameStateMachine stateMachine,
      IAudioService audioService,
      IWindowService windowService
    )
    {
      _assetProvider = assetProvider;
      _staticData = staticData;
      _inputService = inputService;
      _pauseService = pauseService;
      _savingService = savingService;
      _stateMachine = stateMachine;
      _audioService = audioService;
      _windowService = windowService;
    }

    public void CleanUp()
    {
      _windowService.Clear();
      _assetProvider.CleanUp();
    }

    public async Task CreateRootCanvas()
    {
      GameObject rootCanvas = await _assetProvider.Instantiate(AssetsAddress.RootCanvas);
      rootCanvas.GetComponent<InputListener>().Construct(_windowService, _inputService);
      _rootCanvas = rootCanvas.transform;
    }

    public void CreatePauseWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Pause);
      PauseWindow window = Object.Instantiate(config.Template, _rootCanvas) as PauseWindow;

      window.Construct(_pauseService, _savingService, _stateMachine, _audioService, _windowService);
      RegisterWindow(WindowId.Pause, window);
    }

    public void CreateDeathWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Death);
      DeathWindow window = Object.Instantiate(config.Template, _rootCanvas) as DeathWindow;

      window.Construct(_stateMachine, _pauseService, _audioService);
      RegisterWindow(WindowId.Death, window);
    }

    public void CreateSettingsWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Settings);
      SettingsWindow window = Object.Instantiate(config.Template, _rootCanvas) as SettingsWindow;

      window.Construct(_audioService, _windowService);
      RegisterWindow(WindowId.Settings, window);
    }

    private void RegisterWindow(WindowId id, WindowTemplate window) =>
      _windowService.Register(id, window);
  }
}