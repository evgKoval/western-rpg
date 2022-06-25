namespace Codebase.Services.Pause
{
  public interface IPauseService : IService
  {
    void Register(IPauseable pauseable);
    void Clear();
    void Pause();
    void Resume();
  }
}