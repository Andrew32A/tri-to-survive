using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerBar : MonoBehaviour
{
    public float maxTime = 30;
    public float currentTime;
    public bool timerStart = false;

    public Slider slider;
    public Image fill;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = maxTime;
        slider.maxValue = 30;
        slider.value = slider.maxValue;
        timerStart = true;
    }

    void Update()
    {
        if (timerStart == true) {
            if (currentTime <= 5) {
                timerText.color = Color.red;
            } 
            
            if (currentTime <= 0) {
                timerStart = false;
                currentTime = 0;

                // TODO: open shop after time hits 0
                openShop();

            } else if (currentTime > 0) {
                countDown();
            } 
        }
    }

    private void countDown() {
        currentTime = (currentTime - Time.deltaTime);
        slider.value = currentTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }

    private void openShop() {
        Debug.Log("shop has opened!");
    }
}
