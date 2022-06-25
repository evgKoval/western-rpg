using Codebase.Logic;
using Codebase.Services.Input;
using Codebase.Services.Pause;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Player
{
  public class Aiming : MonoBehaviour, IDeathable, IPauseable
  {
    [SerializeField] private Rig _aimingLayer;
    [SerializeField] private float _aimDuration;

    private IInputService _inputService;

    public bool IsReady => Mathf.Approximately(_aimingLayer.weight, 1f);
    public float ReadyPercentage => _aimingLayer.weight;
    public bool IsPaused { get; private set; }


    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Update()
    {
      if (IsPaused)
        return;

      if (_inputService.IsAimButton())
        _aimingLayer.weight += Time.deltaTime / _aimDuration;
      else
        _aimingLayer.weight -= Time.deltaTime / _aimDuration;
    }

    public void Pause() =>
      IsPaused = true;

    public void Resume() =>
      IsPaused = false;
  }
}