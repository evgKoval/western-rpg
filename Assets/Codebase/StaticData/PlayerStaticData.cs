using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "PlayerData", menuName = "Static Data/Player")]
  public class PlayerStaticData : ScriptableObject
  {
    public int MaxHealth;
    public int CurrentHealth;
  }
}