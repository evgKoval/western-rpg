using Codebase.Data;
using Codebase.Infrastructure.Factories;
using Codebase.Services.Progress;
using UnityEngine;

namespace Codebase.Services.Saving
{
  public class SavingService : ISavingService
  {
    private const string ProgressKey = "Progress";

    private readonly IGameFactory _gameFactory;
    private readonly IProgressService _progressService;

    public SavingService(IGameFactory gameFactory, IProgressService progressService)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
    }

    public void SaveProgress()
    {
      foreach (ISaveable progressSaveable in _gameFactory.ProgressSaveables)
        progressSaveable.SaveProgress(_progressService.Progress);

      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}