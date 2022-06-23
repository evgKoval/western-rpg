using Codebase.Services.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Windows
{
  public class PauseWindow : WindowTemplate
  {
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _exitButton;

    private IPauseService _pauseService;

    public void Construct(IPauseService pauseService) =>
      _pauseService = pauseService;

    protected override void Initialize() => 
      _pauseService.Pause();

    protected override void SubscribeUpdates() =>
      _resumeButton.onClick.AddListener(ClosePauseWindow);

    protected override void Cleanup()
    {
      base.Cleanup();
      _resumeButton.onClick.RemoveListener(ClosePauseWindow);
    }

    private void ClosePauseWindow()
    {
      Destroy(gameObject);
      _pauseService.Resume();
    }
  }
}