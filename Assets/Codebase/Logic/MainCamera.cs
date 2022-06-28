using UnityEngine;

namespace Codebase.Logic
{
  public class MainCamera : MonoBehaviour
  {
    private void Awake() =>
      DontDestroyOnLoad(this);
  }
}