using UnityEngine;

namespace Codebase.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }
    bool IsAimButton();
    bool IsFiringButtonDown();
  }
}