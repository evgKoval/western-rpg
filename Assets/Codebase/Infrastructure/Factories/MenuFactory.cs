using Codebase.Infrastructure.AssetManagement;
using Codebase.Infrastructure.States;
using Codebase.Menus;
using Codebase.UI;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class MenuFactory : IMenuFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IGameStateMachine _stateMachine;

    private Transform _rootCanvas;

    public MenuFactory(IAssetProvider assetProvider, IGameStateMachine stateMachine)
    {
      _assetProvider = assetProvider;
      _stateMachine = stateMachine;
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
      mainMenu.GetComponent<MainMenu>().Construct(_stateMachine);
    }
  }
}