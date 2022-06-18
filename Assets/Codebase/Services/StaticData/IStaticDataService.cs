using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WeaponStaticData GetWeapon(WeaponId id);
  }
}