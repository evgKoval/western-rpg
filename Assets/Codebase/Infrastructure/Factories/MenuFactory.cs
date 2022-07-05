using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.States;
using Codebase.Logic;
using Codebase.Menus;
using Codebase.Services.Audio;
using Codebase.UI;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class MenuFactory : IMenuFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IGameStateMachine _stateMachine;
    private readonly IAudioService _audioService;

    private Transform _rootCanvas;

    public MenuFactory(IAssetProvider assetProvider, IGameStateMachine stateMachine, IAudioService audioService)
    {
      _assetProvider = assetProvider;
      _stateMachine = stateMachine;
      _audioService = audioService;
    }

    public void CreateRootCanvas()
    {
      GameObject rootCanvas = _assetProvider.Instantiate(AssetPath.RootCanvas);
      rootCanvas.GetComponent<InputListener>().enabled = false;
      _rootCanvas = rootCanvas.transform;
    }

    public void CreateMainMenu()
    {
      GameObject mainMenu = _assetProvider.Instantiate(AssetPath.MainMenu, _rootCanvas);
      mainMenu.GetComponent<MainMenu>().Construct(_stateMachine, _audioService);
    }

    public void CreateMainAudioSource()
    {
      GameObject mainAudioSource = _assetProvider.Instantiate(AssetPath.MainAudioSource);
      _audioService.Register(mainAudioSource.GetComponent<MainAudioSource>());
    }
  }
}