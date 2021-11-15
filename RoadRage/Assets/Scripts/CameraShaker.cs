using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShaker : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private float mShakeTimer=0;

    public static CameraShaker Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mShakeTimer > 0)
        {
            mShakeTimer -= Time.deltaTime;
        }
        if (mShakeTimer <= 0)
        {
            CinemachineBasicMultiChannelPerlin mPerline = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            mPerline.m_AmplitudeGain = 0;
        }
    }

    /// <summary>
    /// Shakes Camera When Car is Hit
    /// </summary>
    /// <param name="intensity"></param>
    /// <param name="time"></param>
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin mPerline = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        mPerline.m_AmplitudeGain = intensity;
        mShakeTimer = time;
    }
}
