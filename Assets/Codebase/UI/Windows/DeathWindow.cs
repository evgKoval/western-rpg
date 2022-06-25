using Codebase.Infrastructure.States;
using Codebase.Services.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Windows
{
  public class DeathWindow : WindowTemplate
  {
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _exitButton;

    private IGameStateMachine _stateMachine;
    private IPauseService _pauseService;

    public void Construct(IGameStateMachine stateMachine, IPauseService pauseService)
    {
      _stateMachine = stateMachine;
      _pauseService = pauseService;
    }

    protected override void Initialize() =>
      _pauseService.Pause();

    protected override void SubscribeUpdates()
    {
      _loadButton.onClick.AddListener(LoadProgress);
      _exitButton.onClick.AddListener(Exit);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      _loadButton.onClick.RemoveListener(LoadProgress);
      _exitButton.onClick.RemoveListener(Exit);
    }

    private void LoadProgress() =>
      _stateMachine.Enter<LoadProgressState>();

    private void Exit() =>
      _stateMachine.Enter<MainMenuState>();
  }
}