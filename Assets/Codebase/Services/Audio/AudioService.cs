using System.Collections.Generic;
using System.Linq;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Services.Audio
{
  public class AudioService : IAudioService
  {
    private const string SoundsPath = "Audio/Sounds";

    private MainAudioSource _mainAudioSource;
    private Dictionary<string, AudioClip> _sounds;

    public void Register(MainAudioSource mainAudioSource) =>
      _mainAudioSource = mainAudioSource;

    public void LoadAllSounds() =>
      _sounds = Resources
        .LoadAll<AudioClip>(SoundsPath)
        .ToDictionary(clip => clip.name, clip => clip);

    public void PlaySound(string clipName)
    {
      if (_sounds.TryGetValue(clipName, out AudioClip clip))
      {
        _mainAudioSource.Sounds.clip = clip;
        _mainAudioSource.Sounds.Play();
      }
    }
  }
}