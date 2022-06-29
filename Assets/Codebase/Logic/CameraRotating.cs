using Cinemachine;
using Codebase.Player;
using Codebase.Services.Input;
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
    private IInputService _inputService;
    private float _defaultFOV;
    private float _defaultXAxisSpeed;
    private float _defaultYAxisSpeed;

    public bool IsPaused { get; }

    public void Construct(Aiming aiming, IInputService inputService)
    {
      _aiming = aiming;
      _inputService = inputService;
    }

    private void Awake()
    {
      _camera = GetComponent<CinemachineFreeLook>();
#if UNITY_ANDROID
      ConfigureMobileSettings();
#endif
    }

    private void Start()
    {
      _defaultFOV = _camera.m_Lens.FieldOfView;
      _defaultXAxisSpeed = _camera.m_XAxis.m_MaxSpeed;
      _defaultYAxisSpeed = _camera.m_YAxis.m_MaxSpeed;
    }

    private void Update()
    {
      if (IsPaused)
        return;

#if UNITY_ANDROID
      RotateCamera();
#endif
      ToAimingState();
    }

    public void Pause() =>
      gameObject.SetActive(false);

    public void Resume() =>
      gameObject.SetActive(true);

    private void ConfigureMobileSettings()
    {
      Input.simulateMouseWithTouches = false;
      _camera.m_XAxis.m_InputAxisName = "";
      _camera.m_YAxis.m_InputAxisName = "";
    }

    private void RotateCamera()
    {
      _camera.m_XAxis.m_InputAxisValue = _inputService.MouseAxis.x;
      _camera.m_YAxis.m_InputAxisValue = _inputService.MouseAxis.y;
    }

    private void ToAimingState()
    {
      _camera.m_Lens.FieldOfView = Mathf.Lerp(_defaultFOV, _aimingFOV, _aiming.ReadyPercentage);
      _camera.m_XAxis.m_MaxSpeed = Mathf.Lerp(_defaultXAxisSpeed, _aimingXAxisSpeed, _aiming.ReadyPercentage);
      _camera.m_YAxis.m_MaxSpeed = Mathf.Lerp(_defaultYAxisSpeed, _aimingYAxisSpeed, _aiming.ReadyPercentage);
    }
  }
}