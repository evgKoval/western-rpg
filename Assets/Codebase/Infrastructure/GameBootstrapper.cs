using Codebase.Infrastructure.States;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain _curtainPrefab;

    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(_curtainPrefab));
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}