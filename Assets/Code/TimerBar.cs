using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerBar : MonoBehaviour
{
    public int maxTime = 31;
    public int currentTime;

    public Slider slider;
    public Image fill;
    public TextMeshProUGUI timerText;

    void Start()
    {
        currentTime = maxTime;
        slider.maxValue = 31;
        slider.value = slider.maxValue;
    }

    void Update()
    {
        if (currentTime <= 5) {
            timerText.color = Color.red;
        } 
        
        if (currentTime <= 0) {
            currentTime = 0;

            // TODO: open shop after time hits 0
            openShop();
        } else if (currentTime > 0) {
            countDown();
        } 
    }

    private void countDown() {
        currentTime = (int)(maxTime - Time.time);
        slider.value = currentTime;
        timerText.text = currentTime.ToString();
    }

    private void openShop() {
        Debug.Log("shop has opened!");
    }
}
