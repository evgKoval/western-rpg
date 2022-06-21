using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  public class Weapon : MonoBehaviour
  {
    private const string Enemy = "Enemy";

    private Camera _camera;
    private ParticleSystem _gunSmokeFX;
    private int _enemyLayerMask;

    private void Awake()
    {
      _camera = Camera.main;
      _gunSmokeFX = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() =>
      _enemyLayerMask = 1 << LayerMask.NameToLayer(Enemy);

    public void Shoot()
    {
      _gunSmokeFX.Play();

      if (TryHit(out RaycastHit raycastHit))
        raycastHit.collider.GetComponent<IHealth>().TakeDamage(20);
    }

    private bool TryHit(out RaycastHit raycastHit)
    {
      Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

      if (Physics.Raycast(ray, out raycastHit, 20, _enemyLayerMask))
        return raycastHit.collider;

      return false;
    }
  }
}