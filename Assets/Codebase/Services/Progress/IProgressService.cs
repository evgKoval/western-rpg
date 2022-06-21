using Codebase.Data;

namespace Codebase.Services
{
  public interface IProgressService : IService
  {
    PlayerProgress Progress { get; set; }
  }
}