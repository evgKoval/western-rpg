using System;

namespace Codebase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public LevelData LevelData;

    public PlayerProgress(string initialScene) =>
      LevelData = new LevelData(initialScene);
  }
}