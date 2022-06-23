namespace Codebase.Services.Pause
{
  public interface IPauseable
  {
    bool IsPaused { get; }
    void Pause();
    void Resume();
  }
}