using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Skitty_Attack2 : Skitty
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Map")
            {
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "DeadShot")
            {
                Destroy(gameObject);
            }

        }

        /*
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                foreach (target in player)
                {

                }
            }
        }*/
    }
}
