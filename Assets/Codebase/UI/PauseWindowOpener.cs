using Codebase.Infrastructure.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
  public class PauseWindowOpener : MonoBehaviour
  {
    private Button _button;
    private IUIFactory _uiFactory;

    public void Construct(IUIFactory uiFactory) =>
      _uiFactory = uiFactory;

    private void Awake() =>
      _button = GetComponent<Button>();

    private void Start() =>
      _button.onClick.AddListener(OpenPauseWindow);

    private void OnDestroy() =>
      _button.onClick.RemoveListener(OpenPauseWindow);

    private void OpenPauseWindow() =>
      _uiFactory.CreatePauseWindow();
  }
}