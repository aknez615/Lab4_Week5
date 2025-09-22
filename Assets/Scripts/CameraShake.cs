using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeDuration = 0.2f;
    private float shakeTimer;
    private float intensity = 2f;

    private void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineCamera>();
        noise = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                StopShake();
            }
        }
    }

    public void Shake()
    {
        noise.AmplitudeGain = intensity;
        shakeTimer = shakeDuration;
    }

    private void StopShake()
    {
        noise.AmplitudeGain = 0f;
    }
}
