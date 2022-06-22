using System.Linq;
using Codebase.Logic;
using Codebase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPosition = "InitialPosition";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.transform.position))
          .ToList();

        levelData.LevelName = SceneManager.GetActiveScene().name;

        levelData.InitialPosition = GameObject.FindWithTag(InitialPosition).transform.position;
      }

      EditorUtility.SetDirty(target);
    }
  }
}