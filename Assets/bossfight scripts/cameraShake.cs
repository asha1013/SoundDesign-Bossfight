using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake Instance { get; private set; }
    CinemachineVirtualCamera vcam;
    private float shakeTimer;
    CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        Instance = this;
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float time)
    {
        
        noise.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            
        } else if (shakeTimer <= 0)
        {
            noise.m_AmplitudeGain = 0;
        }
    }
}
