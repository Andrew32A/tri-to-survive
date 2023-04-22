using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public int playerCurrency = 0;

    public TextMeshProUGUI currencyText;
    public Animator currencySquish;

    public GameObject enemySpawner;
    public GameObject storeMenu;
    public GameObject player;
    public GameObject timer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void addCurrency(int currencyAmount) {
        playerCurrency += currencyAmount;
        Debug.Log(playerCurrency);

        // temporarily change color to #FFFF00
        currencyText.color = new Color(1f, 1f, 0f, 1f);
        StartCoroutine(ResetTextColor());

        // play animation
        currencySquish.SetTrigger("CurrencySquish");

        // update currency text
        currencyText.text = "$" + playerCurrency.ToString();
    }

    private IEnumerator ResetTextColor() {
        yield return new WaitForSeconds(0.5f); // wait for 0.5 seconds
        currencyText.color = new Color(1f, 1f, 1f, 1f); // reset to original color
    }

    public void exitStore() {
        // enable player
        player.SetActive(true);

        // enable player input
        player.GetComponent<Weapon>().canInput = true;

        // reset player movement (might need to change to variable instead of 12f later)
        player.GetComponent<Player>().moveSpeed = 12f;
        // player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // enable enemy spawner
        enemySpawner.SetActive(true);
        enemySpawner.GetComponent<EnemySpawner>().StartNextWave();

        // reset timer
        timer.GetComponent<TimerBar>().RestartTimer();

        // disable store menu
        storeMenu.SetActive(false);
    }
}
