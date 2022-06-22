using System;
using UnityEngine;

namespace Codebase.Logic
{
  public class UniqueId : MonoBehaviour
  {
    [SerializeField] private string _id;

    public string Id => _id;

    public void GenerateId() =>
      _id = $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}";
  }
}