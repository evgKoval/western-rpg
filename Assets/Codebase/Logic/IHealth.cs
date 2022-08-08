using System;
using UnityEngine;

namespace Codebase.Logic
{
  public interface IHealth
  {
    event Action Changed;
    event Action TakenDamage;
    int Current { get; }
    int Max { get; }
    void TakeDamage(int damage, Vector3 hitPoint);
  }
}