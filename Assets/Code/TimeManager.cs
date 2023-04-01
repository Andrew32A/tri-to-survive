using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // lower the number, the slower it will be
    public float slowdownLength = 4f;
    public Volume postProcessVolume;
    private DepthOfField depthOfField;

    void Start() {
        // get the DepthOfField component from the post-processing volume
        postProcessVolume.profile.TryGet(out depthOfField);
    }

    void Update() {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
        // disable depth of field effect when not in bullet time
        depthOfField.active = (Time.timeScale < 1f);
    }

    public void BulletTime() {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
