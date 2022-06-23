using Codebase.Services;

namespace Codebase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly ServiceLocator _services;

    public GameLoopState(ServiceLocator services) =>
      _services = services;

    public void Exit() =>
      _services.DisposeAll();

    public void Enter()
    {
    }
  }
}