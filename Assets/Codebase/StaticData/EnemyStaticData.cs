using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "Static Data/Enemy")]
  public class EnemyStaticData : ScriptableObject
  {
    public int MaxHealth;
    public int CurrentHealth;
  }
}