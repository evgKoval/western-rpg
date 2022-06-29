using UnityEngine;

namespace Codebase.Services.Input
{
  class MobileInputService : InputService
  {
    private const string SimpleMouseX = "Simple Mouse X";
    private const string SimpleMouseY = "Simple Mouse Y";

    public override Vector2 Axis => SimpleInputAxis();

    public override Vector2 MouseAxis => new(
      SimpleInput.GetAxis(SimpleMouseX),
      SimpleInput.GetAxis(SimpleMouseY)
    );

    public override bool IsAimButton() =>
      SimpleInput.GetButton(AimButton);

    public override bool IsFiringButtonDown() =>
      SimpleInput.GetButtonDown(FiringButton);

    public override bool IsPauseButtonDown() =>
      SimpleInput.GetButtonDown(EscapeButton);
  }
}