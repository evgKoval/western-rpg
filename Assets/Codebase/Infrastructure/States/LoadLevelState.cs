using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using Codebase.Services;
using Codebase.Services.Audio;
using Codebase.Services.Progress;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using System.Threading.Tasks;
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

    private async void OnLoaded()
    {
      PlayGameMusic();
      await InitWindows();
      await InitGameWorld();
      InformProgressLoadables();

      _stateMachine.Enter<GameLoopState>();
    }

    private void PlayGameMusic() =>
      _audioService.PlayMusic(Game);

    private async Task InitWindows()
    {
      await _uiFactory.CreateRootCanvas();
      _uiFactory.CreatePauseWindow();
      _uiFactory.CreateSettingsWindow();
      _uiFactory.CreateDeathWindow();
    }

    private async Task InitGameWorld()
    {
      LevelStaticData levelData = GetLevelData();

      await InitPlayer(levelData);
      await InitPlayerCamera();
      await InitHUD();
      await InitSpawners(levelData);
    }

    private LevelStaticData GetLevelData() =>
      _staticDataService.GetLevel(SceneManager.GetActiveScene().name);

    private async Task InitPlayer(LevelStaticData levelData)
    {
      GameObject player = await _gameFactory.CreatePlayer(at: levelData.InitialPosition);
      await _gameFactory.CreateWeapon(WeaponId.Shotgun, player.transform);
    }

    private async Task InitPlayerCamera() =>
      await _gameFactory.CreatePlayerCamera();

    private async Task InitHUD() =>
      await _gameFactory.CreateHUD();

    private async Task InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
        await _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position);
    }

    private void InformProgressLoadables()
    {
      foreach (ILoadable loadable in _savingService.Loadables)
        loadable.LoadProgress(_progressService.Progress);
    }
  }
}