using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] private Image _currentValue;

    public void SetValue(float current, float max) =>
      _currentValue.fillAmount = current / max;
  }
}