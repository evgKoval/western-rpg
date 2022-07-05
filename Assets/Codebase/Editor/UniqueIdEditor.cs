using System.Linq;
using Codebase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(UniqueId))]
  public class UniqueIdEditor : UnityEditor.Editor
  {
    private void OnEnable()
    {
      UniqueId uniqueId = (UniqueId)target;

      if (IsPrefab(uniqueId))
        return;

      if (string.IsNullOrEmpty(uniqueId.Id) || IsNotUnique(uniqueId))
        Generate(uniqueId);
    }

    private bool IsPrefab(UniqueId uniqueId) =>
      uniqueId.gameObject.scene.rootCount == 0;

    private bool IsNotUnique(UniqueId uniqueId)
    {
      UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
      return uniqueIds.Any(x => x != uniqueId && x.Id == uniqueId.Id);
    }

    private void Generate(UniqueId uniqueId)
    {
      uniqueId.GenerateId();

      if (!Application.isPlaying)
      {
        EditorUtility.SetDirty(uniqueId);
        EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
      }
    }
  }
}