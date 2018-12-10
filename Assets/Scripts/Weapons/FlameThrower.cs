﻿using UnityEngine;

public class FlameThrower : Weapon {

    public float bulletSpeed = 5;
 
	protected override void Awake() {
        base.Awake();
        
    }
    private void Start() {
        if(!GameManager.instance.weaponSprites.Contains(GunSide))
            GameManager.instance.weaponSprites.Add(GunSide);
    }

    private void OnEnable() {
        ChangeSprites();
    }
    protected override void Update() {
      
    }


    public override void Shoot() {
        Quaternion bulletAngle = Quaternion.Euler(rotateAngle * Vector3.forward);
        Instantiate(Bullet, bulletSpawnPoint.position, bulletAngle);
        Bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawnPoint.up * bulletSpeed);
    }
    
}
