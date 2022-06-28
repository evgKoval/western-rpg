using UnityEngine;

namespace Codebase.Logic
{
  public class MainAudioSource : MonoBehaviour
  {
    [SerializeField] private AudioSource _sounds;

    public AudioSource Sounds => _sounds;

    private void Awake() =>
      DontDestroyOnLoad(this);
  }
}