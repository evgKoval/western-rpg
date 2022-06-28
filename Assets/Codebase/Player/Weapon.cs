using Codebase.Logic;
using UnityEngine;

namespace Codebase.Player
{
  [RequireComponent(typeof(AudioSource))]
  public class Weapon : MonoBehaviour
  {
    private const string Enemy = "Enemy";

    private Camera _camera;
    private ParticleSystem _gunSmokeFX;
    private int _enemyLayerMask;
    private AudioSource _firingAudio;

    private void Awake()
    {
      _camera = Camera.main;
      _gunSmokeFX = GetComponentInChildren<ParticleSystem>();
      _firingAudio = GetComponent<AudioSource>();
    }

    private void Start() =>
      _enemyLayerMask = 1 << LayerMask.NameToLayer(Enemy);

    public void Shoot()
    {
      _gunSmokeFX.Play();
      _firingAudio.Play();

      if (TryHit(out RaycastHit raycastHit))
        raycastHit.collider.GetComponent<IHealth>().TakeDamage(20, raycastHit.point);
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