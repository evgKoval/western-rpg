using Codebase.Data;

namespace Codebase.Services.Progress
{
  public interface ILoadable
  {
    void LoadProgress(PlayerProgress progress);
  }
}