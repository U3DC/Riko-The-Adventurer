﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    //Resources
    public List<Sprite> playerSprites;
    // public List<Sprite> weaponSprites;
    public Sprite weaponSprite;
    //public List<int> weaponPrices;
    public List<int> xpTable;
    //only changing animators change the player skins
    public RuntimeAnimatorController[] playerSkins = new RuntimeAnimatorController[3];

    //References

    public Player player;
    public Weapon weapon;

    public int gold;
    public int experience;
    public GameData data;
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        DataController.instance.RefreshData();

    }

    private void Start() {
        data = DataController.instance.data;
        LoadData();
    }

    //floatingText;
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextManager.instance.Show(msg, fontSize, color, position, motion, duration);

    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //weapon = player.weapon;
        //is the weapon max level
        if (weapon.weaponPrinces.Count == weapon.weaponLevel)
            return false;
        //do we have enough gold? if so upgrade and decrement the weaponprice from the gold
        else if(gold >= weapon.weaponPrinces[weapon.weaponLevel]){
            gold -= weapon.weaponPrinces[weapon.weaponLevel];
            data.weaponLevel[weapon.weaponID] = weapon.weaponLevel;
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
        
    }

    public bool TryUnlockSkin(int skinNumber, int skinPrice) {
        //do we have enough gold? if so upgrade and decrement the weaponprice from the gold
        if (data.skins[skinNumber])
            return true;
        if (gold >= skinPrice) {
            gold -= skinPrice;
            data.skins[skinNumber] = true;
            data.selectedSkin = skinNumber;
            player.ChangeSkin(skinNumber);
            return true;
        }
        return false;
    }

    public void SaveData() {
        data.gold = gold;
        data.experience = experience;
        data.weaponSelected = weapon.weaponID;
        data.weaponLevel[weapon.weaponID] = weapon.weaponLevel;
        DataController.instance.SaveData(data);
    }

    public void LoadData() {
        player.ChangeSkin(data.selectedSkin);
        gold = data.gold;
        experience = data.experience;
        int i = 0;
        //checking collected weapons
        foreach(Weapon wp in data.collectedWeapons) {
            Weapon wep = player.weaponContainer.AddComponent(wp.GetType()) as Weapon;
            wep.weaponLevel = data.weaponLevel[i];
            wep.weaponID = i;
            wep.enabled = IsSelectedWeapon(wep);
            i++;
        }

        //setting the default weapon
        if (data.weaponSelected == 0) {
            player.weaponContainer.GetComponent<FlameThrower>().enabled = true;
        }
    }

    public void UnlockWeapon() {
        Weapon wep = data.notCollectedWeapons[Random.Range(0, data.notCollectedWeapons.Count)];
        data.collectedWeapons.Add(wep);
        data.notCollectedWeapons.Remove(wep);
        //adding new weapon
       // weapon.enabled = false;
        Weapon playerWep = player.weaponContainer.AddComponent(wep.GetType()) as Weapon;
        playerWep.weaponID = data.collectedWeapons.Count - 1;
        data.weaponSelected = data.collectedWeapons.Count - 1;
        playerWep.ChangeSprites();
    }

    public void SwitchWeapon() {
 
        if (weapon.weaponID == data.collectedWeapons.Count - 1) {
            //weapon.enabled = false;
            data.weaponSelected = 0;
            Weapon wep = player.weaponContainer.GetComponent(data.collectedWeapons[0].GetType()) as Weapon;
            weaponSprite = wep.GunSide;
            wep.enabled = true;
            wep.ChangeSprites();
        }
        else {
            //weapon.enabled = false;
            data.weaponSelected = weapon.weaponID + 1;
            Weapon wep = player.weaponContainer.GetComponent(data.collectedWeapons[weapon.weaponID + 1].GetType()) as Weapon;
            weaponSprite = wep.GunSide;
            wep.enabled = true;
            wep.ChangeSprites();
        }
    }
    public void OnDestroy() {
       
        SaveData();
    }

    private bool IsSelectedWeapon(Weapon weapon) {
        if (weapon.weaponID == data.weaponSelected) {
            weapon.ChangeSprites();
            return true;
        }
        return false;
    }

    public void QuitGame() {
        Application.Quit();
    }

    /*/// <summary>
/// int preferedSkin,
/// int pesosAmount,
/// int experience
/// int weaponLevel
/// </summary>
//save state
public void SaveState() {
    string s = "";
    s += "0" + "|";
    s += gold.ToString() + "|";
    s += experience.ToString();
    s += weapon.weaponLevel.ToString();

    PlayerPrefs.SetString("Setting", s);

    //PlayerPrefs.DeleteAll();
}


public void LoadState(Scene scene, LoadSceneMode mode) {
    if (!PlayerPrefs.HasKey("Setting"))
        return;
    string[] data = PlayerPrefs.GetString("Setting").Split('|');

    //Change PlayerScreen
    // amount of pessos
    gold = int.Parse(data[1]);
    experience = int.Parse(data[2]);
    //change weapon level
    weapon.weaponLevel = int.Parse(data[3]);
}*/

}