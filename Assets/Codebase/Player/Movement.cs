using Codebase.Data;
using Codebase.Logic;
using Codebase.Services.Input;
using Codebase.Services.Progress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Player
{
  [RequireComponent(typeof(Animator))]
  public class Movement : MonoBehaviour, IDeathable, ISaveable
  {
    private readonly int _velocityXHash = Animator.StringToHash("VelocityX");
    private readonly int _velocityZHash = Animator.StringToHash("VelocityZ");

    private Animator _animator;
    private IInputService _inputService;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Awake() =>
      _animator = GetComponent<Animator>();

    private void Update() =>
      Move();

    public void SaveProgress(PlayerProgress progress)
    {
      progress.LevelData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (CurrentLevel() != progress.LevelData.PositionOnLevel.Level)
        return;

      Vector3Data savedPosition = progress.LevelData.PositionOnLevel.Position;

      if (savedPosition != null)
        transform.position = savedPosition.AsUnityVector();
    }

    private void Move()
    {
      _animator.SetFloat(_velocityXHash, _inputService.Axis.x, 0.1f, Time.deltaTime);
      _animator.SetFloat(_velocityZHash, _inputService.Axis.y, 0.1f, Time.deltaTime);
    }

    private static string CurrentLevel()
    {
      return SceneManager.GetActiveScene().name;
    }
  }
}