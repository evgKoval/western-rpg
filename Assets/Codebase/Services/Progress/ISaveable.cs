using Codebase.Data;

namespace Codebase.Services.Progress
{
  public interface ISaveable : ILoadable
  {
    void SaveProgress(PlayerProgress progress);
  }
}