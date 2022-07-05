using System.Collections.Generic;
using Codebase.Data;
using Codebase.Services.Progress;
using UnityEngine;

namespace Codebase.Services.Saving
{
  public class SavingService : ISavingService
  {
    private const string ProgressKey = "Progress";

    private readonly IProgressService _progressService;

    public List<ISaveable> Saveables { get; } = new();
    public List<ILoadable> Loadables { get; } = new();

    public SavingService(IProgressService progressService) =>
      _progressService = progressService;

    public void Register(ILoadable loadable)
    {
      if (loadable is ISaveable saveable)
        Saveables.Add(saveable);

      Loadables.Add(loadable);
    }

    public void Clear()
    {
      Saveables.Clear();
      Loadables.Clear();
    }

    public void SaveProgress()
    {
      foreach (ISaveable progressSaveable in Saveables)
        progressSaveable.SaveProgress(_progressService.Progress);

      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}