using System.Collections.Generic;
using System.Linq;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string WeaponsDataPath = "Static Data/Weapons";
    private const string LevelsDataPath = "Static Data/Levels";

    private Dictionary<WeaponId, WeaponStaticData> _weapons;
    private Dictionary<string, LevelStaticData> _levels;


    public void Load()
    {
      LoadWeapons();
      LoadLevels();
    }

    public WeaponStaticData GetWeapon(WeaponId id) =>
      _weapons.TryGetValue(id, out WeaponStaticData staticData)
        ? staticData
        : throw new KeyNotFoundException();

    public LevelStaticData GetLevel(string sceneName) =>
      _levels.TryGetValue(sceneName, out LevelStaticData staticData)
        ? staticData
        : throw new KeyNotFoundException();

    private void LoadWeapons() =>
      _weapons = Resources
        .LoadAll<WeaponStaticData>(WeaponsDataPath)
        .ToDictionary(data => data.WeaponId, data => data);

    private void LoadLevels()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelName, x => x);
    }
  }
}