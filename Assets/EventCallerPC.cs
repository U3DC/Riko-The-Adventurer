﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class EventCallerPC : MonoBehaviour {

    public Player player;
    protected float coolDown = 0.2f;
    protected float lastShoot;

    public void Restart() {
        GameManager.instance.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        GameManager.instance.SaveData();
        Application.Quit();
    }

    public void SwitchWeapon() {
        GameManager.instance.SwitchWeapon();
    }
    //Death Menu And Respawn

     public void Respawn() {
        GameManager.instance.SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    void Update() {
        /*if (Input.GetMouseButton(0)) {
            player.Shoot();
        }
        */
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel != 0) {
            SwitchWeapon();
        }

    }

}
