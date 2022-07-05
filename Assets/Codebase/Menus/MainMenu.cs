using Codebase.Infrastructure.States;
using Codebase.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Menus
{
  public class MainMenu : MonoBehaviour
  {
    private const string ButtonClick = "Button Click";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private IGameStateMachine _stateMachine;
    private IAudioService _audioService;

    public void Construct(IGameStateMachine stateMachine, IAudioService audioService)
    {
      _stateMachine = stateMachine;
      _audioService = audioService;
    }

    private void Start()
    {
      _playButton.onClick.AddListener(LoadGame);
      _exitButton.onClick.AddListener(CloseGame);
    }

    private void OnDestroy()
    {
      _playButton.onClick.RemoveListener(LoadGame);
      _exitButton.onClick.RemoveListener(CloseGame);
    }

    private void LoadGame()
    {
      _audioService.PlaySound(ButtonClick);
      _stateMachine.Enter<LoadProgressState>();
    }

    private void CloseGame()
    {
      _audioService.PlaySound(ButtonClick);
      Application.Quit();
    }
  }
}