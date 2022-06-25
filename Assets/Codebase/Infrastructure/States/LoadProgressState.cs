using Codebase.Data;
using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using Codebase.Services;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;
using Codebase.StaticData;

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
    private readonly IStaticDataService _staticDataService;
    private readonly IGameFactory _gameFactory;

    public LoadProgressState(
      GameStateMachine stateMachine,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain,
      IProgressService progressService,
      ISavingService savingProgress,
      IStaticDataService staticDataService,
      IGameFactory gameFactory
    )
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _progressService = progressService;
      _savingProgress = savingProgress;
      _staticDataService = staticDataService;
      _gameFactory = gameFactory;
    }

    public void Enter()
    {
      _loadingCurtain.Show();

      _gameFactory.CleanUp();
      _gameFactory.WarmUp();

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

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress = new(GameScene);
      PlayerStaticData playerData = _staticDataService.Player;

      progress.PlayerState.MaxHealth = playerData.MaxHealth;
      progress.PlayerState.CurrentHealth = playerData.CurrentHealth;

      return progress;
    }
  }
}