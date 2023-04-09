using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public int playerCurrency = 0;

    public TextMeshProUGUI currencyText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void addCurrency() {
        playerCurrency += 1;
        Debug.Log(playerCurrency);

        currencyText.text = playerCurrency.ToString();
    }
}
