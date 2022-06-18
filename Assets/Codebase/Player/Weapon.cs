using UnityEngine;

namespace Codebase.Player
{
  public class Weapon : MonoBehaviour
  {
    private Camera _camera;
    private ParticleSystem _gunSmokeFX;

    private void Awake()
    {
      _camera = Camera.main;
      _gunSmokeFX = GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
      _gunSmokeFX.Play();

      if (TryHit(out RaycastHit raycastHit))
        Debug.Log($"Hit something: {raycastHit.collider.name}");
    }

    private bool TryHit(out RaycastHit raycastHit)
    {
      Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

      if (Physics.Raycast(ray, out raycastHit))
        return raycastHit.collider;

      return false;
    }
  }
}