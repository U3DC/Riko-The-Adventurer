﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Collidable {

    private Rigidbody2D rBody;
    private Animator anim;
    public int numberofbullets = 1;
    protected override void Start() {
        base.Start();
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate() {
       /* if (canMove)
            rBody.AddForce(transform.right * bulletSpeed);*/
    }

    protected override void OnCollide(Collider2D col) {
        if (col.tag == "Blocking"){
            rBody.velocity = Vector2.zero;
            anim.SetInteger("Anim", 1);
            Destroy(gameObject, 0.2f);
        }
            
        
        if (col.tag == "Enemy") {
            rBody.velocity = Vector2.zero;
            //create a new damage object then we will send it to the enemy we hit
            Damage dmg = new Damage {
                damageAmount = GameManager.instance.weapon.damagePoint[GameManager.instance.weapon.weaponLevel] * numberofbullets,
                origin = transform.position,
                pushForce = GameManager.instance.weapon.pushForce[GameManager.instance.weapon.weaponLevel]
            };
            GetComponent<BoxCollider2D>().enabled = false;
            col.SendMessage("ReceiveDamage", dmg);
            anim.SetInteger("Anim", 1);
            Destroy(gameObject, 0.2f);

        }
    }
}
