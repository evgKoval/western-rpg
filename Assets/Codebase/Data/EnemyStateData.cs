using System;

namespace Codebase.Data
{
  [Serializable]
  public class EnemyStateData
  {
    public Vector3Data Position;
    public int CurrentHealth;
    public int MaxHealth;

    public EnemyStateData(Vector3Data position, int currentHealth, int maxHealth)
    {
      Position = position;
      CurrentHealth = currentHealth;
      MaxHealth = maxHealth;
    }
  }
}