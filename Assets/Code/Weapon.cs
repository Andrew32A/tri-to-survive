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
    private int pistolPrice = 20;
    private int smgPrice = 100;
    private int shotgunPrice = 250;
    private int godGunPrice = 1000;

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

    public void buyItem(int itemCost) {
        if (checkIfPlayerCanAfford(itemCost)) {
            // check to see what player bought and equip it
            if (itemCost == pistolPrice && !isPistolUnlocked) {
                isPistolUnlocked = true;
                currentWeapon = EquipPistol();

                // update player currency
                storeManager.playerCurrency -= itemCost;
                storeManager.updateCurrencyText();
            } else if (itemCost == smgPrice && !isSmgUnlocked) {
                isSmgUnlocked = true;
                currentWeapon = EquipSmg();

                // update player currency
                storeManager.playerCurrency -= itemCost;
                storeManager.updateCurrencyText();
            } else if (itemCost == shotgunPrice && !isShotgunUnlocked) {
                isShotgunUnlocked = true;
                currentWeapon = EquipShotgun();

                // update player currency
                storeManager.playerCurrency -= itemCost;
                storeManager.updateCurrencyText();
            } else if (itemCost == godGunPrice && !isGodGunUnlocked) {
                isGodGunUnlocked = true;
                currentWeapon = EquipGodGun();

                // update player currency
                storeManager.playerCurrency -= itemCost;
                storeManager.updateCurrencyText();
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
