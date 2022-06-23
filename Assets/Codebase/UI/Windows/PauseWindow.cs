using Codebase.Infrastructure.States;
using Codebase.Services.Pause;
using Codebase.Services.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Windows
{
  public class PauseWindow : WindowTemplate
  {
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _exitButton;

    private IPauseService _pauseService;
    private ISavingService _savingService;
    private IGameStateMachine _stateMachine;

    public void Construct(IPauseService pauseService, ISavingService savingService, IGameStateMachine stateMachine)
    {
      _pauseService = pauseService;
      _savingService = savingService;
      _stateMachine = stateMachine;
    }

    protected override void Initialize() =>
      _pauseService.Pause();

    protected override void SubscribeUpdates()
    {
      _resumeButton.onClick.AddListener(ClosePauseWindow);
      _saveButton.onClick.AddListener(SaveProgress);
      _loadButton.onClick.AddListener(LoadProgress);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      _resumeButton.onClick.RemoveListener(ClosePauseWindow);
      _resumeButton.onClick.RemoveListener(SaveProgress);
      _resumeButton.onClick.RemoveListener(LoadProgress);
    }

    private void ClosePauseWindow()
    {
      Destroy(gameObject);
      _pauseService.Resume();
    }

    private void SaveProgress()
    {
      _savingService.SaveProgress();
      ClosePauseWindow();
    }

    private void LoadProgress() =>
      _stateMachine.Enter<BootstrapState>();
  }
}