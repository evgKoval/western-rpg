﻿using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup _curtain;
    [SerializeField] private float _fadeInSpeed;
    [SerializeField] private float _delayInSeconds;

    private void Awake() =>
      DontDestroyOnLoad(this);

    public void Show()
    {
      gameObject.SetActive(true);
      _curtain.alpha = 1;
    }

    public void Hide() =>
      StartCoroutine(DoFadeIn());

    private IEnumerator DoFadeIn()
    {
      yield return new WaitForSeconds(_delayInSeconds);

      while (_curtain.alpha > 0)
      {
        _curtain.alpha -= _fadeInSpeed;
        yield return new WaitForSeconds(_fadeInSpeed);
      }

      gameObject.SetActive(false);
    }
  }
}