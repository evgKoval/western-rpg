﻿using UnityEngine;

namespace Codebase.Logic
{
  public class MainAudioSource : MonoBehaviour
  {
    [SerializeField] private AudioSource _sounds;
    [SerializeField] private AudioSource _music;

    public AudioSource Sounds => _sounds;
    public AudioSource Music => _music;

    private void Awake()
    {
      if (IsThereAnotherMainSource(out GameObject duplicate))
        Destroy(duplicate);

      DontDestroyOnLoad(this);
    }

    private static bool IsThereAnotherMainSource(out GameObject duplicate)
    {
      MainAudioSource[] mainAudioSources = FindObjectsOfType<MainAudioSource>();

      if (mainAudioSources.Length > 1)
      {
        duplicate = mainAudioSources[1].gameObject;
        return true;
      }

      duplicate = null;
      return false;
    }
  }
}