﻿using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.States;
using Codebase.Services.Audio;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using Codebase.Services.Saving;
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
    private readonly ISavingService _savingService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IAudioService _audioService;

    private Transform _rootCanvas;

    public UIFactory(
      IAssetProvider assetProvider,
      IStaticDataService staticData,
      IInputService inputService,
      IPauseService pauseService,
      ISavingService savingService,
      IGameStateMachine stateMachine,
      IAudioService audioService
    )
    {
      _assetProvider = assetProvider;
      _staticData = staticData;
      _inputService = inputService;
      _pauseService = pauseService;
      _savingService = savingService;
      _stateMachine = stateMachine;
      _audioService = audioService;
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
      window.Construct(_pauseService, _savingService, _stateMachine, _audioService, this);
    }

    public void CreateDeathWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Death);
      DeathWindow window = Object.Instantiate(config.Template, _rootCanvas) as DeathWindow;
      window.Construct(_stateMachine, _pauseService, _audioService);
    }

    public void CreateSettingsWindow()
    {
      WindowConfig config = _staticData.GetWindow(WindowId.Settings);
      SettingsWindow window = Object.Instantiate(config.Template, _rootCanvas) as SettingsWindow;
      window.Construct(_audioService);
    }
  }
}