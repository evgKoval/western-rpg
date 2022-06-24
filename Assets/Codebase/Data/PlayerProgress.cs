using System;

namespace Codebase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public LevelData LevelData;
    public EnemyData EnemyData;
    public PlayerState PlayerState;

    public PlayerProgress(string initialScene)
    {
      LevelData = new LevelData(initialScene);
      EnemyData = new EnemyData();
      PlayerState = new PlayerState();
    }
  }
}