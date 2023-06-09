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
    public TextMeshProUGUI waveCompleteText;

    public AudioSource audioSource;
    public float originalVolume;

    public GameObject enemySpawner;
    public GameObject storeMenu;
    public GameObject player;
    public GameObject playerUI;
    public GameObject crosshair;

    void Start() {
        currentTime = maxTime;
        slider.maxValue = 30;
        slider.value = slider.maxValue;
        timerStart = true;
    }

    public void RestartTimer() {
        currentTime = maxTime;
        slider.maxValue = 30;
        slider.value = slider.maxValue;
        timerText.color = Color.white;
        timerStart = true;
    }

    void Update() {
        if (timerStart == true) {
            // turn timer text to red when at 5 or below
            if (currentTime <= 5) {
                timerText.color = Color.red;
            } 
            
            if (currentTime <= 0) {
                timerStart = false;
                currentTime = 0;
                StartCoroutine(openShop());

            } else if (currentTime > 0) {
                countDown();
            } 
        }
    }

    private void countDown() {
        // decrease timer by delta time float
        currentTime = (currentTime - Time.deltaTime);

        // update slider
        slider.value = currentTime;

        // round timer count up
        timerText.text = Mathf.Ceil(currentTime).ToString();
    }

    private IEnumerator openShop() {
        // display wave complete message
        StartCoroutine(waveCompleteMessage());

        // disable player input
        player.GetComponent<Weapon>().canInput = false;

        // disable player movement and stop velocity
        player.GetComponent<Player>().moveSpeed = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // disable enemy spawner
        enemySpawner.SetActive(false);

        // remove enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Destroy(enemy);
        }

        // wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // disable player
        player.SetActive(false);

        // reset text to blank once done
        waveCompleteText.text = "";

        // store the original volume of the audio source
        originalVolume = audioSource.volume;

        // lower volume by 80% when entering store
        audioSource.volume *= 0.2f;

        // disable crosshair
        crosshair.SetActive(false);

        // enable default cursor
        Cursor.visible = true;

        // enable store menu
        storeMenu.SetActive(true);

        // disable player UI (NOTE: includes timer bar and this script)
        playerUI.SetActive(false);
    }

    private IEnumerator waveCompleteMessage() {
        string message = "Wave Complete";
        float typingSpeed = 0.1f; // controls how fast the text is typed out

        // display message by 'typing' it out
        for (int i = 0; i <= message.Length; i++) {
            waveCompleteText.text = message.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
