namespace Codebase.Infrastructure.States
{
  public interface IState : IExitableState
  {
    void Enter();
  }
}