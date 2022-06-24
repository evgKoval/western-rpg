using Codebase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Menus
{
  public class MainMenu : MonoBehaviour
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private IGameStateMachine _stateMachine;

    public void Construct(IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

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

    private void LoadGame() =>
      _stateMachine.Enter<LoadProgressState>();

    private static void CloseGame() =>
      Application.Quit();
  }
}