using Codebase.Services;

namespace Codebase.Infrastructure.Factories
{
  public interface IMenuFactory : IService
  {
    void CreateRootCanvas();
    void CreateMainMenu();
    void CreateMainAudioSource();
  }
}