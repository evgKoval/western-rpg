using Codebase.Services.Window;
using Codebase.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
  public class PauseWindowOpener : MonoBehaviour
  {
    private Button _button;
    private IWindowService _windowService;

    public void Construct(IWindowService windowService) =>
      _windowService = windowService;

    private void Awake() =>
      _button = GetComponent<Button>();

    private void Start() =>
      _button.onClick.AddListener(OpenPauseWindow);

    private void OnDestroy() =>
      _button.onClick.RemoveListener(OpenPauseWindow);

    private void OpenPauseWindow() =>
      _windowService.Open(WindowId.Pause);
  }
}