using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private Coroutine _shakeRoutine;
    private static CameraShake _instance;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
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
        _shakeRoutine = Lerp.Delay(duration, () =>
        {
            _noise.m_AmplitudeGain = 0;
            _noise.m_FrequencyGain = 0;
        });
    }
}
