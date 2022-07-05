using Codebase.Infrastructure.States;
using Codebase.Services.Audio;
using Codebase.Services.Pause;
using Codebase.Services.Saving;
using Codebase.Services.Window;
using Codebase.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Windows
{
  public class PauseWindow : WindowTemplate
  {
    private const string ButtonClick = "Button Click";

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private IPauseService _pauseService;
    private ISavingService _savingService;
    private IGameStateMachine _stateMachine;
    private IAudioService _audioService;
    private IWindowService _windowService;

    public void Construct(
      IPauseService pauseService,
      ISavingService savingService,
      IGameStateMachine stateMachine,
      IAudioService audioService,
      IWindowService windowService
    )
    {
      _pauseService = pauseService;
      _savingService = savingService;
      _stateMachine = stateMachine;
      _audioService = audioService;
      _windowService = windowService;
    }

    protected override void Initialize() =>
      _pauseService.Pause();

    protected override void SubscribeUpdates()
    {
      _resumeButton.onClick.AddListener(ClosePauseWindow);
      _saveButton.onClick.AddListener(SaveProgress);
      _loadButton.onClick.AddListener(LoadProgress);
      _settingsButton.onClick.AddListener(OpenSettingsWindow);
      _exitButton.onClick.AddListener(Exit);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      _resumeButton.onClick.RemoveListener(ClosePauseWindow);
      _saveButton.onClick.RemoveListener(SaveProgress);
      _loadButton.onClick.RemoveListener(LoadProgress);
      _settingsButton.onClick.RemoveListener(OpenSettingsWindow);
      _exitButton.onClick.RemoveListener(Exit);
    }

    private void ClosePauseWindow()
    {
      _audioService.PlaySound(ButtonClick);
      _windowService.Close(this);
      _pauseService.Resume();
    }

    private void SaveProgress()
    {
      _audioService.PlaySound(ButtonClick);
      _savingService.SaveProgress();
      ClosePauseWindow();
    }

    private void LoadProgress()
    {
      _audioService.PlaySound(ButtonClick);
      _stateMachine.Enter<LoadProgressState>();
    }

    private void OpenSettingsWindow()
    {
      _audioService.PlaySound(ButtonClick);
      _windowService.Open(WindowId.Settings);
    }

    private void Exit()
    {
      _audioService.PlaySound(ButtonClick);
      _stateMachine.Enter<MainMenuState>();
    }
  }
}