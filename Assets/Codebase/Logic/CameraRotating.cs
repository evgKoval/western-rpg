using Cinemachine;
using Codebase.Player;
using Codebase.Services.Pause;
using UnityEngine;

namespace Codebase.Logic
{
  [RequireComponent(typeof(CinemachineFreeLook))]
  public class CameraRotating : MonoBehaviour, IPauseable
  {
    [SerializeField] private float _aimingFOV;
    [SerializeField] private float _aimingXAxisSpeed;
    [SerializeField] private float _aimingYAxisSpeed;

    private CinemachineFreeLook _camera;
    private Aiming _aiming;
    private float _defaultFOV;
    private float _defaultXAxisSpeed;
    private float _defaultYAxisSpeed;

    public bool IsPaused { get; }

    public void Construct(Aiming aiming) =>
      _aiming = aiming;

    private void Awake() =>
      _camera = GetComponent<CinemachineFreeLook>();

    private void Start()
    {
      _defaultFOV = _camera.m_Lens.FieldOfView;
      _defaultXAxisSpeed = _camera.m_XAxis.m_MaxSpeed;
      _defaultYAxisSpeed = _camera.m_YAxis.m_MaxSpeed;
    }

    private void Update()
    {
      _camera.m_Lens.FieldOfView = Mathf.Lerp(_defaultFOV, _aimingFOV, _aiming.ReadyPercentage);
      _camera.m_XAxis.m_MaxSpeed = Mathf.Lerp(_defaultXAxisSpeed, _aimingXAxisSpeed, _aiming.ReadyPercentage);
      _camera.m_YAxis.m_MaxSpeed = Mathf.Lerp(_defaultYAxisSpeed, _aimingYAxisSpeed, _aiming.ReadyPercentage);
    }

    public void Pause() =>
      gameObject.SetActive(false);

    public void Resume() =>
      gameObject.SetActive(true);
  }
}