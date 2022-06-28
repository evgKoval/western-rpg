using UnityEngine;

namespace Codebase.UI.Windows
{
  public abstract class WindowTemplate : MonoBehaviour
  {
    [SerializeField] private AudioClip _appearSound;

    private AudioSource _audioSource;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();

      OnAwake();
    }

    private void Start()
    {
      Initialize();
      SubscribeUpdates();

      _audioSource.clip = _appearSound;
      _audioSource.Play();
    }

    private void OnDestroy() =>
      Cleanup();

    protected virtual void OnAwake()
    {
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void SubscribeUpdates()
    {
    }

    protected virtual void Cleanup()
    {
    }
  }
}