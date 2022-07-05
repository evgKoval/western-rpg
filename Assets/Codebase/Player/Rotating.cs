using Codebase.Logic;
using Codebase.Services.Pause;
using UnityEngine;

namespace Codebase.Player
{
  public class Rotating : MonoBehaviour, IDeathable, IPauseable
  {
    [SerializeField] private float _turningSpeed;

    private Camera _camera;

    public bool IsPaused { get; private set; }

    private void Awake() =>
      _camera = Camera.main;

    private void Update()
    {
      if (IsPaused)
        return;

      Rotate();
    }

    private void Rotate()
    {
      float targetAngle = _camera.transform.rotation.eulerAngles.y;
      Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

      float interpolationRatio = _turningSpeed * Time.deltaTime;
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, interpolationRatio);
    }

    public void Pause() =>
      IsPaused = true;

    public void Resume() =>
      IsPaused = false;
  }
}