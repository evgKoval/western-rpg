using System;

namespace Codebase.Data
{
  [Serializable]
  public class LevelData
  {
    public PositionOnLevel PositionOnLevel;

    public LevelData(string initialLevel) =>
      PositionOnLevel = new PositionOnLevel(initialLevel);
  }
}