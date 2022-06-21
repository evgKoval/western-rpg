using UnityEngine;

namespace Codebase.Data
{
  public static class Extensions
  {
    public static Vector3Data AsVectorData(this Vector3 vector) =>
      new(vector.x, vector.y, vector.z);

    public static Vector3 AsUnityVector(this Vector3Data vector3Data) =>
      new(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static string ToJson(this object obj) =>
      JsonUtility.ToJson(obj);

    public static T ToDeserialized<T>(this string json) =>
      JsonUtility.FromJson<T>(json);
  }
}