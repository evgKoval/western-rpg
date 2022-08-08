using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.States;
using Codebase.Logic;
using Codebase.Menus;
using Codebase.Services.Audio;
using Codebase.UI;
using System.Threading.Tasks;
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

    public void CleanUp() =>
      _assetProvider.CleanUp();

    public async Task CreateRootCanvas()
    {
      GameObject rootCanvas = await _assetProvider.Instantiate(AssetsAddress.RootCanvas);
      rootCanvas.GetComponent<InputListener>().enabled = false;
      _rootCanvas = rootCanvas.transform;
    }

    public async Task CreateMainMenu()
    {
      GameObject mainMenu = await _assetProvider.Instantiate(AssetsAddress.MainMenu, _rootCanvas);
      mainMenu.GetComponent<MainMenu>().Construct(_stateMachine, _audioService);
    }

    public async Task CreateMainAudioSource()
    {
      GameObject mainAudioSource = await _assetProvider.Instantiate(AssetsAddress.MainAudioSource);
      _audioService.Register(mainAudioSource.GetComponent<MainAudioSource>());
    }
  }
}