using UnityEngine;

namespace Codebase.Logic
{
  [RequireComponent(typeof(AudioSource))]
  public class Firearm : Weapon
  {
    private const string Enemy = "Enemy";

    [SerializeField] [Range(1, 100)] private int _damage;
    [SerializeField] [Range(1, 100)] private float _maxDistance;
    [SerializeField] [Range(0, 10)] private float _reloadingSpeed;
    [SerializeField] private AudioClip _reloadingSound;

    private Camera _camera;
    private ParticleSystem _gunSmokeFX;
    private int _enemyLayerMask;
    private AudioSource _firingAudio;

    public float ReloadingSpeed => _reloadingSpeed;
    public AudioClip ReloadingSound => _reloadingSound;

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
        raycastHit.collider.GetComponent<IHealth>().TakeDamage(_damage, raycastHit.point);
    }

    private bool TryHit(out RaycastHit raycastHit)
    {
      Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

      if (Physics.Raycast(ray, out raycastHit, _maxDistance, _enemyLayerMask))
        return raycastHit.collider;

      return false;
    }
  }
}