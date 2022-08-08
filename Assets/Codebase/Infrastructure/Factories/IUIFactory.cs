using Codebase.Services;
using System.Threading.Tasks;

namespace Codebase.Infrastructure.Factories
{
  public interface IUIFactory : IService
  {
    void CleanUp();
    Task CreateRootCanvas();
    void CreatePauseWindow();
    void CreateDeathWindow();
    void CreateSettingsWindow();
  }
}