using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using Codebase.Services;
using Codebase.Services.Audio;
using Codebase.Services.Saving;
using Codebase.Services.StaticData;

namespace Codebase.Infrastructure.States
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(MainMenuState)] = new MainMenuState(
          sceneLoader,
          services.Single<IMenuFactory>(),
          services.Single<IAudioService>()
        ),
        [typeof(LoadProgressState)] = new LoadProgressState(
          this,
          sceneLoader,
          loadingCurtain,
          services.Single<IProgressService>(),
          services.Single<ISavingService>(),
          services.Single<IStaticDataService>(),
          services.Single<IGameFactory>(),
          services.Single<IUIFactory>()
        ),
        [typeof(LoadLevelState)] = new LoadLevelState(
          this,
          sceneLoader,
          loadingCurtain,
          services.Single<IGameFactory>(),
          services.Single<IProgressService>(),
          services.Single<IStaticDataService>(),
          services.Single<IUIFactory>(),
          services.Single<ISavingService>(),
          services.Single<IAudioService>()
        ),
        [typeof(GameLoopState)] = new GameLoopState(services),
      };
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}