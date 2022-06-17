using UnityEngine;

namespace Codebase.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string AimButton = "Fire2";

    public abstract Vector2 Axis { get; }
    public abstract bool IsAimButton();

    protected static Vector2 SimpleInputAxis() =>
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}