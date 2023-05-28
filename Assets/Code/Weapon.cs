using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// menu of functions that it's expecting the class to have, polymorphism
public interface IWeapon {
    void Shoot();
}

public class Weapon : MonoBehaviour
{
    private bool devMode = false;
    public bool canInput;

    public IWeapon currentWeapon;
    public GameObject pistolScript;
    public GameObject smgScript;
    public GameObject shotgunScript;
    public GameObject godGunScript;
    public StoreManager storeManager;
    
    // weapon prices
    private int pistolPrice = 0;
    private int smgPrice = 20;
    private int shotgunPrice = 100;
    private int godGunPrice = 500;

    // is weapon unlocked
    private bool isPistolUnlocked = true;
    private bool isSmgUnlocked = false;
    private bool isShotgunUnlocked = false;
    private bool isGodGunUnlocked = false;

    void Start() {
        // set pistol as default weapon
        currentWeapon = EquipPistol();

        // enable shooting
        canInput = true;
    }

    void Update() {
        if (canInput == true) {
            PlayerInput();
        }
    }

    public void PlayerInput() {
        // fire1 == mouse 0 (left click)
        if (Input.GetButton("Fire1")) {
            currentWeapon.Shoot();
        } 

        // enable dev mode
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            devMode = true;

            // give money and update text
            storeManager.playerCurrency += 10000000;
            storeManager.updateCurrencyText();
        }

        // dev mode controls
        if (devMode) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = EquipPistol();
            Debug.Log("pistol equipped");
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                currentWeapon = EquipSmg();
                Debug.Log("smg equipped");
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                currentWeapon = EquipShotgun();
                Debug.Log("shotgun equipped");
            } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
                currentWeapon = EquipGodGun();
                Debug.Log("god gun equipped");
            }
        }
    }

    public bool checkIfPlayerCanAfford(int itemCost) {
        return storeManager.playerCurrency >= itemCost;
    }

    private void updatePlayerCurrency(int itemCost) {
        storeManager.playerCurrency -= itemCost;
        storeManager.updateCurrencyText();
    }

    public void buyItem(int itemCost) {
        if (checkIfPlayerCanAfford(itemCost)) {
            // check to see what player bought and equip it then update player's currency
            if (itemCost == pistolPrice && !isPistolUnlocked) {
                isPistolUnlocked = true;
                currentWeapon = EquipPistol();
                updatePlayerCurrency(itemCost);
            } else if (itemCost == smgPrice && !isSmgUnlocked) {
                isSmgUnlocked = true;
                currentWeapon = EquipSmg();
                updatePlayerCurrency(itemCost);
            } else if (itemCost == shotgunPrice && !isShotgunUnlocked) {
                isShotgunUnlocked = true;
                currentWeapon = EquipShotgun();
                updatePlayerCurrency(itemCost);
            } else if (itemCost == godGunPrice && !isGodGunUnlocked) {
                isGodGunUnlocked = true;
                currentWeapon = EquipGodGun();
                updatePlayerCurrency(itemCost);
            } else {
                Debug.LogError("Invalid item cost or gun is already unlocked.");
            }

            Debug.Log("player spent: " + itemCost);

        } else {
            // TODO: code to handle if the player can't afford the item
            Debug.Log("player doesn't have enough money, tried to buy: " + itemCost);
        }
    }

    public IWeapon EquipPistol() {
        return pistolScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipSmg() {
        return smgScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipShotgun() {
        return shotgunScript.GetComponent<IWeapon>();
    }

    public IWeapon EquipGodGun() {
        return godGunScript.GetComponent<IWeapon>();
    }
}
