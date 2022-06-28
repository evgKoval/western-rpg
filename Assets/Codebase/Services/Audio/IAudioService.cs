using Codebase.Logic;

namespace Codebase.Services.Audio
{
  public interface IAudioService : IService
  {
    void Register(MainAudioSource mainAudioSource);
    void LoadAllSounds();
    void LoadAllMusic();
    void PlaySound(string clipName);
    void PlayMusic(string musicName);
  }
}