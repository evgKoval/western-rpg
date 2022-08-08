using Codebase.Infrastructure.Factories;
using Codebase.Services.Audio;
using System.Threading.Tasks;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
  public class MainMenuState : IState
  {
    private const string MainMenuScene = "Main Menu";
    private const string Menu = "Menu";

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
      _audioService.LoadAllMusic();

      _menuFactory.CleanUp();

      _sceneLoader.Load(MainMenuScene, OnLoaded);
    }

    public void Exit()
    {
    }

    private async void OnLoaded()
    {
      ShowDefaultCursor();
      await InitMenu();
      PlayMenuMusic();
    }

    private static void ShowDefaultCursor()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }

    private async Task InitMenu()
    {
      await _menuFactory.CreateMainAudioSource();
      await _menuFactory.CreateRootCanvas();
      await _menuFactory.CreateMainMenu();
    }

    private void PlayMenuMusic() =>
      _audioService.PlayMusic(Menu);
  }
}