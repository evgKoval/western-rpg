using Codebase.Logic;
using UnityEngine;

namespace Codebase.UI
{
  public class HUDBinding : MonoBehaviour
  {
    [SerializeField] private HealthBar _healthBar;

    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      _health.Changed += UpdateHealthBar;
    }

    private void OnDestroy()
    {
      if (_health != null)
        _health.Changed -= UpdateHealthBar;
    }

    private void UpdateHealthBar() =>
      _healthBar.SetValue(_health.Current, _health.Max);
  }
}