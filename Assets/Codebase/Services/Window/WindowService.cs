using Codebase.Infrastructure.Factories;
using Codebase.StaticData;

namespace Codebase.Services.Window
{
  public class WindowService : IWindowService
  {
    private readonly IUIFactory _uiFactory;

    public WindowService(IUIFactory uiFactory) =>
      _uiFactory = uiFactory;

    public void Open(WindowId id)
    {
    }
  }
}