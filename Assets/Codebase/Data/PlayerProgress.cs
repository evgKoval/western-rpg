using System;

namespace Codebase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public LevelData LevelData;
    public EnemyData EnemyData;

    public PlayerProgress(string initialScene)
    {
      LevelData = new LevelData(initialScene);
      EnemyData = new EnemyData();
    }
  }
}