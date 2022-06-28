using Codebase.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI.Windows
{
  public class SettingsWindow : WindowTemplate
  {
    private const string ButtonClick = "Button Click";
    private const string Music = "Music";
    private const string Sounds = "Sounds";
    private const string UI = "UI";

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private Slider _UISlider;
    [SerializeField] private Button _returnButton;

    private IAudioService _audioService;

    public void Construct(IAudioService audioService) =>
      _audioService = audioService;

    protected override void Initialize()
    {
      _musicSlider.value = _audioService.GetGroupVolumeValue(Music);
      _soundsSlider.value = _audioService.GetGroupVolumeValue(Sounds);
      _UISlider.value = _audioService.GetGroupVolumeValue(UI);
    }

    protected override void SubscribeUpdates()
    {
      _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
      _soundsSlider.onValueChanged.AddListener(ChangeSoundsVolume);
      _UISlider.onValueChanged.AddListener(ChangeUIVolume);
      _returnButton.onClick.AddListener(CloseWindow);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
      _soundsSlider.onValueChanged.RemoveListener(ChangeSoundsVolume);
      _UISlider.onValueChanged.RemoveListener(ChangeUIVolume);
    }

    private void ChangeMusicVolume(float volume) =>
      _audioService.ChangeGroupVolume(Music, volume);

    private void ChangeSoundsVolume(float volume) =>
      _audioService.ChangeGroupVolume(Sounds, volume);

    private void ChangeUIVolume(float volume) =>
      _audioService.ChangeGroupVolume(UI, volume);

    private void CloseWindow()
    {
      _audioService.PlaySound(ButtonClick);
      Destroy(gameObject);
    }
  }
}