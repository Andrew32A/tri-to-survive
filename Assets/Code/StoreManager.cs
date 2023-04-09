using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public int playerCurrency = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void addCurrency() {
        playerCurrency += 1;
        Debug.Log(playerCurrency);
    }
}
