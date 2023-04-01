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
    private float originalVolume;
    public float lowPassFilterCutoff = 5f;

    private AudioLowPassFilter lowPassFilter;
    private AudioReverbFilter reverbFilter;

    public Volume postProcessVolume;
    private DepthOfField depthOfField;
    private Vignette vignette;
    private LensDistortion lensDistortion;

    void Start() {
        // get the components from the post-processing volume
        postProcessVolume.profile.TryGet(out depthOfField);
        postProcessVolume.profile.TryGet(out vignette);
        postProcessVolume.profile.TryGet(out lensDistortion);

        // store the original pitch and volume of the audio source
        originalPitch = audioSource.pitch;
        originalVolume = audioSource.volume;

        // get components
        lowPassFilter = audioSource.GetComponent<AudioLowPassFilter>();
        reverbFilter = audioSource.GetComponent<AudioReverbFilter>();
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
        
        if (Time.timeScale < 1f) {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, originalPitch, Time.deltaTime);
            audioSource.volume = originalVolume * 0.5f;
            audioSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = lowPassFilterCutoff;
            reverbFilter.enabled = true; // enable reverb when in slow motion
        } else {
            audioSource.volume = originalVolume;
            audioSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            reverbFilter.enabled = false; // disable reverb when not in slow motion
        }
    }

    public void BulletTime() {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        audioSource.pitch *= 0.95f;
    }
}
