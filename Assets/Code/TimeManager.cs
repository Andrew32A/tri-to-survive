using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // lower the number, the slower it will be
    public float slowdownLength = 4f;
    
    public AudioSource audioSource;
    private float originalPitch;

    public Volume postProcessVolume;
    private DepthOfField depthOfField;
    private Vignette vignette;
    private LensDistortion lensDistortion;

    void Start() {
        // get the components from the post-processing volume
        postProcessVolume.profile.TryGet(out depthOfField);
        postProcessVolume.profile.TryGet(out vignette);
        postProcessVolume.profile.TryGet(out lensDistortion);

        // store the original pitch of the audio source
        originalPitch = audioSource.pitch;
    }

    void Update() {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        // disable post processing effect when not in bullet time
        depthOfField.active = (Time.timeScale < 1f);
        vignette.active = (Time.timeScale < 1f);
        lensDistortion.active = (Time.timeScale < 1f);

        // set min and max audio pitch
        audioSource.pitch = Mathf.Clamp(audioSource.pitch, 0.8f, 1f);
        
        // raise the pitch slowly back to normal
        if (Time.timeScale >= 1f)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, originalPitch, Time.deltaTime);
        }
    }

    public void BulletTime() {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        audioSource.pitch *= 0.95f;
    }
}
