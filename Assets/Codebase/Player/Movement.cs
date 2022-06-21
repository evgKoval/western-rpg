using Codebase.Logic;
using Codebase.Services.Input;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Animator))]
  public class Movement : MonoBehaviour, IDeathable
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

    private void Move()
    {
      _animator.SetFloat(_velocityXHash, _inputService.Axis.x, 0.1f, Time.deltaTime);
      _animator.SetFloat(_velocityZHash, _inputService.Axis.y, 0.1f, Time.deltaTime);
    }
  }
}