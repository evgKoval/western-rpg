using Codebase.Logic;

namespace Codebase.Services.Audio
{
  public interface IAudioService : IService
  {
    void Register(MainAudioSource mainAudioSource);
    void LoadAllSounds();
    void PlaySound(string clipName);
  }
}