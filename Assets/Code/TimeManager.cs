using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // Lower the number, the slower it will be
    public float slowdownLength = 3f;
    private float originalTimeScale;
    private float originalFixedDeltaTime;
    public float revertDuration = 2f; // Duration for the time and pitch reversion
    private float revertTimer = 0f; // Timer for the reversion

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

    private bool isBulletTimeActive = false;
    private float bulletTimeTimer = 0f;

    private void Start()
    {
        // Store the original time scale and fixed delta time
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;

        // Get the components from the post-processing volume
        postProcessVolume.profile.TryGet(out depthOfField);
        postProcessVolume.profile.TryGet(out vignette);
        postProcessVolume.profile.TryGet(out lensDistortion);

        // Store the original pitch and volume of the audio source
        originalPitch = audioSource.pitch;
        originalVolume = audioSource.volume;

        // Get components
        lowPassFilter = audioSource.GetComponent<AudioLowPassFilter>();
        reverbFilter = audioSource.GetComponent<AudioReverbFilter>();
    }

    private void Update()
    {
        if (isBulletTimeActive)
        {
            bulletTimeTimer += Time.unscaledDeltaTime;

            if (bulletTimeTimer < slowdownLength)
            {
                float progress = bulletTimeTimer / slowdownLength;
                float targetPitch = Mathf.Lerp(originalPitch, slowdownFactor * originalPitch, progress);
                float targetVolume = Mathf.Lerp(originalVolume, originalVolume * 0.5f, progress);

                audioSource.pitch = targetPitch;
                audioSource.volume = targetVolume;

                // Set low pass filter cutoff frequency
                lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassFilter.cutoffFrequency, lowPassFilterCutoff, progress);

                // Enable reverb when in slow motion
                reverbFilter.enabled = true;

                // Disable post-processing effects when not in bullet time
                depthOfField.active = true;
                vignette.active = true;
                lensDistortion.active = true;
            }
                    else
        {
            if (revertTimer < revertDuration)
            {
                revertTimer += Time.unscaledDeltaTime;
                float progress = revertTimer / revertDuration;

                // Revert time scale gradually
                Time.timeScale = Mathf.Lerp(slowdownFactor, originalTimeScale, progress);
                Time.fixedDeltaTime = Time.timeScale * 0.02f;

                // Revert pitch and volume gradually
                audioSource.pitch = Mathf.Lerp(slowdownFactor * originalPitch, originalPitch, progress);
                audioSource.volume = Mathf.Lerp(originalVolume * 0.5f, originalVolume, progress);

                // Revert low pass filter cutoff frequency gradually
                lowPassFilter.cutoffFrequency = Mathf.Lerp(lowPassFilterCutoff, 22000, progress);
            }
            else
            {
                isBulletTimeActive = false;
                bulletTimeTimer = 0f;
                audioSource.pitch = originalPitch;
                audioSource.volume = originalVolume;
                lowPassFilter.cutoffFrequency = 22000;
                reverbFilter.enabled = false;

                depthOfField.active = false;
                vignette.active = false;
                lensDistortion.active = false;

                // Reset time scale and fixed delta time to their original values
                Time.timeScale = originalTimeScale;
                Time.fixedDeltaTime = originalFixedDeltaTime;
            } }

        }
    }

    public void BulletTime()
    {
        if (!isBulletTimeActive)
        {
            isBulletTimeActive = true;

            // Store the current time scale and fixed delta time
            originalTimeScale = Time.timeScale;
            originalFixedDeltaTime = Time.fixedDeltaTime;

            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            bulletTimeTimer = 0f; // Reset the bullet time timer
            revertTimer = 0f; // Reset the revert timer
        }
    }
}
