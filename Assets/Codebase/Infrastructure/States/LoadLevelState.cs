using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string Initialposition = "InitialPosition";

    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();

      _gameFactory.WarmUp();

      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      InitGameWorld();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      InitPlayer();
      InitPlayerCamera();
      InitHUD();
      InitEnemies();
    }

    private void InitPlayer()
    {
      Vector3 initialPosition = GameObject.FindWithTag(Initialposition).transform.position;
      GameObject player = _gameFactory.CreatePlayer(initialPosition);
      _gameFactory.CreateWeapon(WeaponId.Shotgun, player.transform);
    }

    private void InitPlayerCamera() =>
      _gameFactory.CreatePlayerCamera();

    private void InitHUD() =>
      _gameFactory.CreateHUD();

    private void InitEnemies()
    {
      GameObject enemy = _gameFactory.CreateEnemy();
      _gameFactory.CreateWeapon(WeaponId.Axe, enemy.transform);
    }
  }
}