using System.Collections.Generic;

namespace Codebase.Services.Pause
{
  public class PauseService : IPauseService
  {
    private List<IPauseable> _pauseables = new();

    public void Register(IPauseable pauseable) =>
      _pauseables.Add(pauseable);

    public void Clear() =>
      _pauseables.Clear();

    public void Pause()
    {
      foreach (IPauseable pauseable in _pauseables)
        pauseable.Pause();
    }

    public void Resume()
    {
      foreach (IPauseable pauseable in _pauseables)
        pauseable.Resume();
    }
  }
}