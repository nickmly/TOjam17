using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Spuppy_Attack2 : Spuppy
    {
        GameObject player;

        // ------ Base mobile attributes ------
        private int bProjectileMass = 5;
        private int bDamage = 10;
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
            Debug.Log("Hit");
            Destroy(gameObject);

            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);
                InstEffects("Spuppy/Attack", "mine", this.transform);

                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Map")
            {
                InstEffects("Spuppy/Attack", "mine", this.transform);
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "DeadShot")
            {
                Destroy(gameObject);
            }
        }
    }
}
