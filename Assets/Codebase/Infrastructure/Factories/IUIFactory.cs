using Codebase.Services;

namespace Codebase.Infrastructure.Factories
{
  public interface IUIFactory : IService
  {
    void CreateRootCanvas();
  }
}