using Codebase.Logic;
using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "WeaponData", menuName = "Static Data/Weapon")]
  public class WeaponStaticData : ScriptableObject
  {
    public WeaponId WeaponId;
    public Weapon Prefab;
  }
}