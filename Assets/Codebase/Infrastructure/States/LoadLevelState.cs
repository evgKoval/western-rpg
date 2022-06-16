using Codebase.Logic;

namespace Codebase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();

      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded() =>
      _stateMachine.Enter<GameLoopState>();
  }
}