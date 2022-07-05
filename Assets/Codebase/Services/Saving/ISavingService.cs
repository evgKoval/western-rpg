using System.Collections.Generic;
using Codebase.Data;
using Codebase.Services.Progress;

namespace Codebase.Services.Saving
{
  public interface ISavingService : IService
  {
    List<ISaveable> Saveables { get; }
    List<ILoadable> Loadables { get; }
    void Register(ILoadable loadable);
    void Clear();
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}