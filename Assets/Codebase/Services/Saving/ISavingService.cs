using Codebase.Data;

namespace Codebase.Services.Saving
{
  public interface ISavingService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}