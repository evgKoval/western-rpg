using System.Collections.Generic;
using Codebase.StaticData;
using Codebase.UI.Windows;

namespace Codebase.Services.Window
{
  public class WindowService : IWindowService
  {
    private readonly Dictionary<WindowId, WindowTemplate> _windows = new();
    private WindowTemplate _activeWindow;

    public void Register(WindowId id, WindowTemplate window) =>
      _windows.Add(id, window);

    public void Clear() =>
      _windows.Clear();

    public void Open(WindowId id)
    {
      if (_activeWindow)
        Close(_activeWindow);

      if (_windows.TryGetValue(id, out WindowTemplate window))
      {
        window.gameObject.SetActive(true);
        _activeWindow = window;
      }
    }

    public void Close(WindowTemplate window)
    {
      window.gameObject.SetActive(false);
      _activeWindow = null;
    }
  }
}