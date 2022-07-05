using Codebase.Infrastructure.Factories;
using Codebase.Services.Audio;
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

      _sceneLoader.Load(MainMenuScene, OnLoaded);
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
      ShowDefaultCursor();
      InitMenu();
      PlayMenuMusic();
    }

    private static void ShowDefaultCursor()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }

    private void InitMenu()
    {
      _menuFactory.CreateMainAudioSource();
      _menuFactory.CreateRootCanvas();
      _menuFactory.CreateMainMenu();
    }

    private void PlayMenuMusic() =>
      _audioService.PlayMusic(Menu);
  }
}