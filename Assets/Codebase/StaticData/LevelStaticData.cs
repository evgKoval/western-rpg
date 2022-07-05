using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelName;
    public Vector3 InitialPosition;
    public List<EnemySpawnerStaticData> EnemySpawners;
  }
}