using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin noise;

    [SerializeField] float amplitudeGain = 15;
    [SerializeField] float frequencyGain = 0.2f;

    public void Shake()
    {
        noise = GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StartCoroutine(ShakeCR());
    }

    IEnumerator ShakeCR(float duration = 0.5f)
    {
        SetNoise(amplitudeGain, frequencyGain);
        yield return new WaitForSeconds(duration);
        SetNoise(0, 0);
    }

    public void SetNoise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }
}
