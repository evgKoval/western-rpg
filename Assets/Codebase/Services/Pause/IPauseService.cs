namespace Codebase.Services.Pause
{
  public interface IPauseService : IService
  {
    void Pause();
    void Resume();
  }
}