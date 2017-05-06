﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Skitty_Special : Skitty
    {
        GameObject player;

        // ------ Base mobile attributes ------
        private int bProjectileMass = 5;
        private int bDamage = 15;
        // ------ Base mobile attributes ------

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            ProjectilePosition = this.transform;
            ProjectileMass = bProjectileMass;

            Destroy(gameObject, 10.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            //Debug.Log("Hit");
            //Destroy(gameObject);

            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                Destroy(gameObject, 1.0f);
            }

            if (other.gameObject.tag == "Map")
            {
                Destroy(gameObject, 1.0f);
            }
           
        }
    }
}
