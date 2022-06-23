using Codebase.Services.Pause;
using UnityEngine;

namespace Codebase.Logic
{
  public class CameraRotating : MonoBehaviour, IPauseable
  {
    public bool IsPaused { get; }

    public void Pause() =>
      gameObject.SetActive(false);

    public void Resume() =>
      gameObject.SetActive(true);
  }
}