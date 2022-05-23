using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();    
    }
    
    public void Shake(float duration = 0.1F, float amplitude = 1.5F, float frequency = 20)
    {
        StopAllCoroutines();
        StartCoroutine(ApplyNoiseRoutine(duration,amplitude,frequency));
    }

    IEnumerator ApplyNoiseRoutine(float duration, float amplitude, float frequency)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0.0F;
        noise.m_FrequencyGain = 0.0F;
    }
}
