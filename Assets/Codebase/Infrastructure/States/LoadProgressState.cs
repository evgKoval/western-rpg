using Codebase.Data;
using Codebase.Services;
using Codebase.Services.Saving;

namespace Codebase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private const string GameScene = "Game";

    private readonly GameStateMachine _gameStateMachine;
    private readonly IProgressService _progressService;
    private readonly ISavingService _savingProgress;

    public LoadProgressState(
      GameStateMachine gameStateMachine,
      IProgressService progressService,
      ISavingService savingProgress
    )
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _savingProgress = savingProgress;
    }

    public void Enter()
    {
      InitProgress();

      _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.LevelData.PositionOnLevel.Level);
    }

    public void Exit()
    {
    }

    private void InitProgress()
    {
      _progressService.Progress =
        _savingProgress.LoadProgress()
        ?? NewProgress();
    }

    private static PlayerProgress NewProgress() =>
      new(GameScene);
  }
}