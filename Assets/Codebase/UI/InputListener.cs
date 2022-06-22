using Codebase.Infrastructure.Factories;
using Codebase.Services.Input;
using UnityEngine;

namespace Codebase.UI
{
  public class InputListener : MonoBehaviour
  {
    private IInputService _inputService;
    private IUIFactory _uiFactory;

    public void Construct(IUIFactory uiFactory, IInputService inputService)
    {
      _uiFactory = uiFactory;
      _inputService = inputService;
    }

    private void Update()
    {
      if (_inputService.IsPauseButtonDown())
        _uiFactory.CreatePauseWindow();
    }
  }
}