using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private Coroutine _shakeRoutine;
    private Coroutine _zoomRoutine;
    private static CameraShake _instance;
    private float _defaultFov;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _defaultFov = _virtualCamera.m_Lens.FieldOfView;
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SetShake();
    }

    public static void SetShake(float strength = 1f, float duration = 0.25f)
    {
        _instance.SetShakeInternal(strength, duration);
    }

    private void SetShakeInternal(float strength, float duration)
    {
        if(_shakeRoutine != null)
            CoroutineHelper.StopGlobalCoroutine(_shakeRoutine);

        _noise.m_AmplitudeGain = strength;
        _noise.m_FrequencyGain = strength * 50;
        if(duration > 0f)
        {
            _shakeRoutine = Lerp.Delay(duration, () =>
            {
                _noise.m_AmplitudeGain = 0;
                _noise.m_FrequencyGain = 0;
            });
        }
    }

    public static void SetZoom(float zoom = 1f)
    {
        _instance.SetZoomInternal(zoom);
    }

    private void SetZoomInternal(float zoom)
    {
        if(_zoomRoutine != null)
        {
            CoroutineHelper.StopGlobalCoroutine(_zoomRoutine);
        }

        _zoomRoutine = Lerp.FromTo(0.2f, t =>
        {
            _virtualCamera.m_Lens.FieldOfView = t;
        }, _virtualCamera.m_Lens.FieldOfView, _defaultFov / zoom);
    }

    public static void SetFollowTarget(Transform target)
    {
        _instance.SetFollowTargetInternal(target);
    }

    private void SetFollowTargetInternal(Transform target)
    {
        _virtualCamera.Follow = target;
    }
}
