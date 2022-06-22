using Codebase.Infrastructure.AssetManagement;
using Codebase.Services.StaticData;
using UnityEngine;

namespace Codebase.Infrastructure.Factories
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataService _staticData;

    private Transform _rootCanvas;

    public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
    {
      _assetProvider = assetProvider;
      _staticData = staticData;
    }

    public void CreateRootCanvas()
    {
      GameObject rootCanvas = _assetProvider.Instantiate(AssetPath.RootCanvas);
      _rootCanvas = rootCanvas.transform;
    }
  }
}