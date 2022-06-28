using Codebase.Infrastructure.Factories;
using Codebase.Services.Audio;

namespace Codebase.Infrastructure.States
{
  public class MainMenuState : IState
  {
    private const string MainMenuScene = "Main Menu";

    private readonly SceneLoader _sceneLoader;
    private readonly IMenuFactory _menuFactory;
    private readonly IAudioService _audioService;

    public MainMenuState(SceneLoader sceneLoader, IMenuFactory menuFactory, IAudioService audioService)
    {
      _sceneLoader = sceneLoader;
      _menuFactory = menuFactory;
      _audioService = audioService;
    }

    public void Enter()
    {
      _audioService.LoadAllSounds();

      _sceneLoader.Load(MainMenuScene, OnLoaded);
    }

    public void Exit()
    {
    }

    private void OnLoaded() =>
      InitMenu();

    private void InitMenu()
    {
      _menuFactory.CreateMainAudioSource();
      _menuFactory.CreateRootCanvas();
      _menuFactory.CreateMainMenu();
    }
  }
}