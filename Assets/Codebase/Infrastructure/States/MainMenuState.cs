using Codebase.Infrastructure.Factories;

namespace Codebase.Infrastructure.States
{
  public class MainMenuState : IState
  {
    private const string MainMenuScene = "Main Menu";

    private readonly SceneLoader _sceneLoader;
    private readonly IMenuFactory _menuFactory;

    public MainMenuState(SceneLoader sceneLoader, IMenuFactory menuFactory)
    {
      _sceneLoader = sceneLoader;
      _menuFactory = menuFactory;
    }

    public void Enter() =>
      _sceneLoader.Load(MainMenuScene, OnLoaded);

    public void Exit()
    {
    }

    private void OnLoaded() =>
      InitMenu();

    private void InitMenu()
    {
      _menuFactory.CreateRootCanvas();
      _menuFactory.CreateMainMenu();
    }
  }
}