using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    PlayerStaticData Player { get; }
    EnemyStaticData Enemy { get; }
    void Load();
    WeaponStaticData GetWeapon(WeaponId id);
    LevelStaticData GetLevel(string sceneName);
    WindowConfig GetWindow(WindowId id);
  }
}