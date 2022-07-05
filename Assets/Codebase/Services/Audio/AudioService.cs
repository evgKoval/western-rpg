using System.Collections.Generic;
using System.Linq;
using Codebase.Logic;
using UnityEngine;

namespace Codebase.Services.Audio
{
  public class AudioService : IAudioService
  {
    private const string SoundsPath = "Audio/Sounds";
    private const string MusicPath = "Audio/Music";

    private MainAudioSource _mainAudioSource;
    private Dictionary<string, AudioClip> _sounds;
    private Dictionary<string, AudioClip> _music;

    public void Register(MainAudioSource mainAudioSource) =>
      _mainAudioSource = mainAudioSource;

    public void LoadAllSounds() =>
      _sounds = Resources
        .LoadAll<AudioClip>(SoundsPath)
        .ToDictionary(clip => clip.name, clip => clip);

    public void LoadAllMusic() =>
      _music = Resources
        .LoadAll<AudioClip>(MusicPath)
        .ToDictionary(clip => clip.name, clip => clip);

    public void PlaySound(string clipName)
    {
      if (_sounds.TryGetValue(clipName, out AudioClip clip))
      {
        _mainAudioSource.Sounds.clip = clip;
        _mainAudioSource.Sounds.Play();
      }
    }

    public void PlayMusic(string musicName)
    {
      if (_music.TryGetValue(musicName, out AudioClip music))
      {
        _mainAudioSource.Music.clip = music;
        _mainAudioSource.Music.Play();
      }
    }

    public void ChangeGroupVolume(string groupName, float volume) =>
      _mainAudioSource.Mixer.SetFloat(groupName, Mathf.Lerp(-80, 0, volume));

    public float GetGroupVolumeValue(string groupName)
    {
      _mainAudioSource.Mixer.GetFloat(groupName, out float value);
      return FromMixerToNormalValue(value);
    }

    private float FromMixerToNormalValue(float value) =>
      (value + 80) / 80;
  }
}