using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // TODO: place weapons in a dict with values below

    // weapon text
    public TextMeshProUGUI pistolCardButtonText;
    public TextMeshProUGUI smgCardButtonText;
    public TextMeshProUGUI shotgunCardButtonText;
    public TextMeshProUGUI godGunCardButtonText;
    
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

    // conditionals go brr (im sure there's a better way to do this, but it works so im leaving it for now)
    public void buyItem(int itemCost) {
        // check to see what player bought and equip it then update player's currency
        if (itemCost == pistolPrice) {
            if (!isPistolUnlocked && checkIfPlayerCanAfford(itemCost)) {
                isPistolUnlocked = true;
                setWeaponsEquipText();
                currentWeapon = EquipPistol();
                pistolCardButtonText.text = "Equipped";
                updatePlayerCurrency(itemCost);
            } else if (isPistolUnlocked) {
                setWeaponsEquipText();
                currentWeapon = EquipPistol();
                pistolCardButtonText.text = "Equipped";
            } else {
                playerIsTooPoor(itemCost);
            }
        } else if (itemCost == smgPrice) {
            if (!isSmgUnlocked && checkIfPlayerCanAfford(itemCost)) {
                isSmgUnlocked = true;
                setWeaponsEquipText();
                currentWeapon = EquipSmg();
                smgCardButtonText.text = "Equipped";
                updatePlayerCurrency(itemCost);
            } else if (isSmgUnlocked) {
                setWeaponsEquipText();
                currentWeapon = EquipSmg();
                smgCardButtonText.text = "Equipped";
            } else {
                playerIsTooPoor(itemCost);
            }
        } else if (itemCost == shotgunPrice) {
            if (!isShotgunUnlocked && checkIfPlayerCanAfford(itemCost)) {
                isShotgunUnlocked = true;
                setWeaponsEquipText();
                currentWeapon = EquipShotgun();
                shotgunCardButtonText.text = "Equipped";
                updatePlayerCurrency(itemCost);
            } else if (isShotgunUnlocked) {
                setWeaponsEquipText();
                currentWeapon = EquipShotgun();
                shotgunCardButtonText.text = "Equipped";
            } else {
                playerIsTooPoor(itemCost);
            }
        } else if (itemCost == godGunPrice) {
            if (!isGodGunUnlocked && checkIfPlayerCanAfford(itemCost)) {
                setWeaponsEquipText();
                isGodGunUnlocked = true;
                currentWeapon = EquipGodGun();
                godGunCardButtonText.text = "Equipped";
                updatePlayerCurrency(itemCost);
            } else if (isGodGunUnlocked) {
                setWeaponsEquipText();
                currentWeapon = EquipGodGun();
                godGunCardButtonText.text = "Equipped";
            } else {
                playerIsTooPoor(itemCost);
            }
        }
    }

    public void playerIsTooPoor(int itemCost) {
        // TODO: code to handle if the player can't afford the item
        Debug.Log("player doesn't have enough money, tried to buy: " + itemCost);
    }

    // update all weapons card buy button to "equip" if weapon is already bought
    public void setWeaponsEquipText() {
        if (isPistolUnlocked) {
            pistolCardButtonText.text = "Equip";
        }

        if (isSmgUnlocked) {
            smgCardButtonText.text = "Equip";
        }

        if (isShotgunUnlocked) {
            shotgunCardButtonText.text = "Equip";
        }

        if (isGodGunUnlocked) {
            godGunCardButtonText.text = "Equip";
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
