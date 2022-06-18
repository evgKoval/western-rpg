using Codebase.Services.Input;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Codebase.Player
{
  public class Aiming : MonoBehaviour
  {
    [SerializeField] private Rig _aimingLayer;
    [SerializeField] private float _aimDuration;

    public bool IsReady => Mathf.Approximately(_aimingLayer.weight, 1f);

    private IInputService _inputService;

    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    private void Update()
    {
      if (_inputService.IsAimButton())
        _aimingLayer.weight += Time.deltaTime / _aimDuration;
      else
        _aimingLayer.weight -= Time.deltaTime / _aimDuration;
    }
  }
}