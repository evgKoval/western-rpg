using Codebase.StaticData;
using Codebase.UI.Windows;

namespace Codebase.Services.Window
{
  public interface IWindowService : IService
  {
    void Register(WindowId id, WindowTemplate window);
    void Clear();
    void Open(WindowId id);
    void Close(WindowTemplate window);
  }
}