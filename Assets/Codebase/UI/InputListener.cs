using Codebase.Services.Input;
using Codebase.Services.Window;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.UI
{
  public class InputListener : MonoBehaviour
  {
    private IInputService _inputService;
    private IWindowService _windowService;

    public void Construct(IWindowService windowService, IInputService inputService)
    {
      _windowService = windowService;
      _inputService = inputService;
    }

    private void Update()
    {
      if (_inputService.IsPauseButtonDown())
        _windowService.Open(WindowId.Pause);
    }
  }
}