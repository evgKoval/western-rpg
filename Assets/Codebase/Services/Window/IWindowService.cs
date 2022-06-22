using Codebase.StaticData;

namespace Codebase.Services.Window
{
  public interface IWindowService : IService
  {
    void Open(WindowId id);
  }
}