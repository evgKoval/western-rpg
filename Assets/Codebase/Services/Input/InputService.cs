using UnityEngine;

namespace Codebase.Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    protected const string FiringButton = "Fire1";
    protected const string AimButton = "Fire2";
    protected const string EscapeButton = "Cancel";

    public abstract Vector2 Axis { get; }
    public abstract bool IsAimButton();
    public abstract bool IsFiringButtonDown();
    public abstract bool IsPauseButtonDown();

    protected static Vector2 SimpleInputAxis() =>
      new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
  }
}