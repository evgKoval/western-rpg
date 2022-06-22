using UnityEngine;

namespace Codebase.UI.Windows
{
  public abstract class WindowTemplate : MonoBehaviour
  {
    private void Awake() =>
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
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