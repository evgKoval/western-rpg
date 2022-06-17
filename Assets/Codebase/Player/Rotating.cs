using UnityEngine;

namespace Codebase.Player
{
  public class Rotating : MonoBehaviour
  {
    [SerializeField] private float _turningSpeed;

    private Camera _camera;

    private void Awake() =>
      _camera = Camera.main;

    private void Update() =>
      Rotate();

    private void Rotate()
    {
      float targetAngle = _camera.transform.rotation.eulerAngles.y;
      Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

      float interpolationRatio = _turningSpeed * Time.deltaTime;
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, interpolationRatio);
    }
  }
}