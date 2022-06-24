using Codebase.Data;
using Codebase.Logic;
using Codebase.Services;
using Codebase.Services.Saving;

namespace Codebase.Infrastructure.States
{
  public class LoadProgressState : IState
  {
    private const string InitialScene = "Initial";
    private const string GameScene = "Game";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IProgressService _progressService;
    private readonly ISavingService _savingProgress;

    public LoadProgressState(
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain,
      IProgressService progressService,
      ISavingService savingProgress
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _progressService = progressService;
      _savingProgress = savingProgress;
    }

    public void Enter()
    {
      _loadingCurtain.Show();

      _sceneLoader.Load(InitialScene, OnLoaded);
    }

    private void OnLoaded()
    {
      InitProgress();

      _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.LevelData.PositionOnLevel.Level);
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