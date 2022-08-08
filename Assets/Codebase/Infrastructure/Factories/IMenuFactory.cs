using Codebase.Services;
using System.Threading.Tasks;

namespace Codebase.Infrastructure.Factories
{
  public interface IMenuFactory : IService
  {
    void CleanUp();
    Task CreateRootCanvas();
    Task CreateMainMenu();
    Task CreateMainAudioSource();
  }
}