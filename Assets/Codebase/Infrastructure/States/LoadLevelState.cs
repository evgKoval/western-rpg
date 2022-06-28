using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using Codebase.Services;
using Codebase.Services.Audio;
using Codebase.Services.Progress;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string Game = "Game";

    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IUIFactory _uiFactory;
    private readonly ISavingService _savingService;
    private readonly IAudioService _audioService;

    public LoadLevelState(
      IGameStateMachine gameStateMachine,
      SceneLoader sceneLoader,
      LoadingCurtain loadingCurtain,
      IGameFactory gameFactory,
      IProgressService progressService,
      IStaticDataService staticDataService,
      IUIFactory uiFactory,
      ISavingService savingService,
      IAudioService audioService
    )
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticDataService = staticDataService;
      _uiFactory = uiFactory;
      _savingService = savingService;
      _audioService = audioService;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName, OnLoaded);

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      PlayGameMusic();
      InitRootCanvas();
      InitGameWorld();
      InformProgressLoadables();

      _stateMachine.Enter<GameLoopState>();
    }

    private void PlayGameMusic() =>
      _audioService.PlayMusic(Game);

    private void InitRootCanvas() =>
      _uiFactory.CreateRootCanvas();

    private void InitGameWorld()
    {
      LevelStaticData levelData = GetLevelData();

      InitPlayer(levelData);
      InitPlayerCamera();
      InitHUD();
      InitSpawners(levelData);
    }

    private LevelStaticData GetLevelData() =>
      _staticDataService.GetLevel(SceneManager.GetActiveScene().name);

    private void InitPlayer(LevelStaticData levelData)
    {
      GameObject player = _gameFactory.CreatePlayer(at: levelData.InitialPosition);
      _gameFactory.CreateWeapon(WeaponId.Shotgun, player.transform);
    }

    private void InitPlayerCamera() =>
      _gameFactory.CreatePlayerCamera();

    private void InitHUD() =>
      _gameFactory.CreateHUD();

    private void InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
        _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position);
    }

    private void InformProgressLoadables()
    {
      foreach (ILoadable loadable in _savingService.Loadables)
        loadable.LoadProgress(_progressService.Progress);
    }
  }
}