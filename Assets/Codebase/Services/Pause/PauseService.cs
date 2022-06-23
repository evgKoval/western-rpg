using Codebase.Infrastructure.Factories;

namespace Codebase.Services.Pause
{
  public class PauseService : IPauseService
  {
    private readonly IGameFactory _gameFactory;

    public PauseService(IGameFactory gameFactory) => 
      _gameFactory = gameFactory;

    public void Pause()
    {
      foreach (IPauseable pauseable in _gameFactory.Pauseables)
        pauseable.Pause();
    }

    public void Resume()
    {
      foreach (IPauseable pauseable in _gameFactory.Pauseables)
        pauseable.Resume();
    }
  }
}