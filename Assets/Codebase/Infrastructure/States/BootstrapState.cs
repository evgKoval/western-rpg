using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.Factories;
using Codebase.Services;
using Codebase.Services.Input;

namespace Codebase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string InitialScene = "Initial";
    private const string GameScene = "Game";

    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly ServiceLocator _services;

    public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      RegisterInputService();
      _services.RegisterSingle<IAssetProvider, AssetProvider>();
      _services.RegisterSingle<IGameFactory, GameFactory>();
    }

    private void RegisterInputService()
    {
#if UNITY_STANDALONE
      _services.RegisterSingle<IInputService, StandaloneInputService>();
#else
      _services.RegisterSingle<IInputService, MobileInputService>();
#endif
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>(GameScene);
  }
}