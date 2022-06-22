using System;
using UnityEngine;

namespace Codebase.StaticData
{
  [Serializable]
  public class EnemySpawnerStaticData
  {
    public string Id;
    public Vector3 Position;

    public EnemySpawnerStaticData(string id, Vector3 position)
    {
      Id = id;
      Position = position;
    }
  }
}