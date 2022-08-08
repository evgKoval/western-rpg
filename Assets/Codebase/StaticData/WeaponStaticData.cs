using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "WeaponData", menuName = "Static Data/Weapon")]
  public class WeaponStaticData : ScriptableObject
  {
    public WeaponId WeaponId;
    public AssetReferenceGameObject PrefabReference;
  }
}