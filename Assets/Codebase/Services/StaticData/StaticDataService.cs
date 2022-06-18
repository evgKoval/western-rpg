using System.Collections.Generic;
using System.Linq;
using Codebase.StaticData;
using UnityEngine;

namespace Codebase.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string WeaponsDataPath = "Static Data/Weapons";

    private Dictionary<WeaponId, WeaponStaticData> _weapons;

    public void Load() =>
      LoadWeapons();

    public WeaponStaticData GetWeapon(WeaponId id) =>
      _weapons.TryGetValue(id, out WeaponStaticData staticData)
        ? staticData
        : throw new KeyNotFoundException();

    private void LoadWeapons() =>
      _weapons = Resources
        .LoadAll<WeaponStaticData>(WeaponsDataPath)
        .ToDictionary(data => data.WeaponId, data => data);
  }
}