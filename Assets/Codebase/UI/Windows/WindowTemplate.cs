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

    private void OnEnable()
    {
      ShowDefaultCursor();
      Initialize();
      SubscribeUpdates();

      _audioSource.clip = _appearSound;
      _audioSource.Play();
    }

    private void OnDisable()
    {
      HideDefaultCursor();
      Cleanup();
    }

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

    private static void ShowDefaultCursor()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }

    private static void HideDefaultCursor()
    {
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
}