using Codebase.Services;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
  public class GameLoopState : IState
  {
    private readonly ServiceLocator _services;

    public GameLoopState(ServiceLocator services) =>
      _services = services;

    public void Exit() =>
      _services.DisposeAll();

    public void Enter() =>
      HideDefaultCursor();

    private static void HideDefaultCursor()
    {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
}