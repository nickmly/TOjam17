﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skitty Mobile
/// </summary>

namespace AllMobiles
{
    public class Skitty : Mobiles
    {
        // ------ Base mobile attributes ------
        private int bHealth = 100;
        private int bArmour = 25;
        private int bMobileMass = 10;
        private int bProjectileMass = 5;

        private bool canShoot = false;
        private float shootDelay = 3;
        // ------ Base mobile attributes ------

        [SerializeField]
        Transform gunTransform;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            // Will have to make a proper spawn location once models are implemented
            AmmoSpawn = gunTransform;

            //MobilePosition; (random location to be spawned on the map)
            Health = bHealth;
            Armour = bArmour;
            MobileMass = bMobileMass;
            ProjectileMass = bProjectileMass;

            Force = 100.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (canShoot)
            {
                shootDelay -= Time.deltaTime;
                if (shootDelay < 0)
                {
                    shootDelay = 3;
                    canShoot = false;
                }
            }
        }

        public override void AttackShot(float power)
        {
            base.AttackShot(power);
            
            switch (attack)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        canShoot = true;
                        if (canShoot)
                        {
                            Shoot("Skitty/Attack", "ammo1", power);
                        }
                        Debug.Log("Attack 1");
                    }
                    break;
                case 1:
                    Shoot("skitty/attack", "ammo2", power);
                    Debug.Log("Attack 2");
                    break;
                case 2:
                    Shoot("skitty/attack", "ammo3", power);
                    Debug.Log("Attack 3");
                    break;
                default:
                    break;
            }
        }

        public override void TakeDamage(int damageTaken)
        {
            base.TakeDamage(damageTaken);
        }
    }
}
