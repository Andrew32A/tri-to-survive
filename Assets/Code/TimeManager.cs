using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // lower the number, the slower it will be
    public float slowdownLength = 5f;

    void Update() {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
    }

    public void BulletTime() {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
