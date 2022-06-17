using UnityEngine;

namespace Codebase.Services.Input
{
  class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();

    public override bool IsAimButton() =>
      SimpleInput.GetButton(AimButton);
  }
}