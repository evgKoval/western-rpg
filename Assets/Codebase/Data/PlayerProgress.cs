using System;

namespace Codebase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public LevelData LevelData;
    public EnemyDataDictionary EnemyData;
    public PlayerState PlayerState;

    public PlayerProgress(string initialScene)
    {
      LevelData = new LevelData(initialScene);
      EnemyData = new EnemyDataDictionary();
      PlayerState = new PlayerState();
    }
  }
}