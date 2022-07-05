using System.Collections;
using Codebase.Logic;
using Codebase.Services.Window;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(Death))]
  public class CheckingDeath : MonoBehaviour
  {
    private IWindowService _windowService;
    private Death _death;

    public void Construct(IWindowService windowService) =>
      _windowService = windowService;

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
      _windowService.Open(WindowId.Death);
    }
  }
}