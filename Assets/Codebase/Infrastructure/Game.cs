using Codebase.Infrastructure.States;
using Codebase.Logic;
using Codebase.Services;

namespace Codebase.Infrastructure
{
  public class Game
  {
    public IGameStateMachine StateMachine { get; }

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain) =>
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, ServiceLocator.Container);
  }
}