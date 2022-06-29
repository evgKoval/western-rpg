using UnityEngine;

namespace Codebase.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }
    Vector2 MouseAxis { get; }
    bool IsAimButton();
    bool IsFiringButtonDown();
    bool IsPauseButtonDown();
  }
}