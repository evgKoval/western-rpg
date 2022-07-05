using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData
{
  [CreateAssetMenu(fileName = "WindowData", menuName = "Static Data/Window")]
  public class WindowStaticData : ScriptableObject
  {
    public List<WindowConfig> Configs;
  }
}