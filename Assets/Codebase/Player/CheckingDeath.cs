using System.Collections;
using Codebase.Infrastructure.Factories;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Death))]
  public class CheckingDeath : MonoBehaviour
  {
    private IUIFactory _uiFactory;
    private Death _death;

    public void Construct(IUIFactory uiFactory) =>
      _uiFactory = uiFactory;

    private void Awake() =>
      _death = GetComponent<Death>();

    private void Start() =>
      _death.Happened += ShowDeathWindowAfterSeconds;

    private void OnDestroy() =>
      _death.Happened -= ShowDeathWindowAfterSeconds;

    private void ShowDeathWindowAfterSeconds() =>
      StartCoroutine(ShowDeathWindow());

    private IEnumerator ShowDeathWindow()
    {
      yield return new WaitForSeconds(3);
      _uiFactory.CreateDeathWindow();
    }
  }
}