using Codebase.Logic;
using UnityEditor;
using UnityEngine;

namespace Codebase.Editor
{
  [CustomEditor(typeof(SpawnMarker))]
  public class SpawnerMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(spawner.transform.position, 0.5f);
    }
  }
}