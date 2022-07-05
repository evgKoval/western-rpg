using Codebase.Services;

namespace Codebase.Infrastructure.Factories
{
  public interface IUIFactory : IService
  {
    void CleanUp();
    void CreateRootCanvas();
    void CreatePauseWindow();
    void CreateDeathWindow();
    void CreateSettingsWindow();
  }
}