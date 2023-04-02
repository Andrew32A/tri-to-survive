using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWarning : MonoBehaviour
{
    public float blinkInterval = 0.5f;
    private float timeSinceLastBlink = 0f;
    private UnityEngine.Rendering.Universal.Light2D warningLight;

    void Start()
    {
        warningLight = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        timeSinceLastBlink += Time.deltaTime;
        if (timeSinceLastBlink >= blinkInterval) {
            timeSinceLastBlink = 0f;
            warningLight.enabled = !warningLight.enabled;
        }
    }
}
