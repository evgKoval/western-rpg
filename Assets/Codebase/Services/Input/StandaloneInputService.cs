using UnityEngine;

namespace Codebase.Services.Input
{
  class StandaloneInputService : InputService
  {
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public override Vector2 Axis
    {
      get
      {
        Vector2 axis = SimpleInputAxis();

        if (axis == Vector2.zero)
          axis = UnityAxis();

        return axis;
      }
    }

    public override Vector2 MouseAxis => new(
      SimpleInput.GetAxis(MouseX),
      SimpleInput.GetAxis(MouseY)
    );

    public override bool IsAimButton() =>
      UnityEngine.Input.GetButton(AimButton);

    public override bool IsFiringButtonDown() =>
      UnityEngine.Input.GetButtonDown(FiringButton);

    public override bool IsPauseButtonDown() =>
      UnityEngine.Input.GetButtonDown(EscapeButton);

    private static Vector2 UnityAxis() =>
      new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
  }
}