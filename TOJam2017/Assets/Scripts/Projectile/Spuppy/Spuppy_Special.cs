using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Spuppy_Special : Spuppy
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
            Debug.Log("Hit");
            Destroy(gameObject);

            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);
                MapEffects("Prefabs/Explosions/Explosion1", "explosion1", this.transform);

                Destroy(gameObject);
            }

            if (other.gameObject.tag == "Map")
            {
                MapEffects("Prefabs/Explosions/Explosion1", "explosion1", this.transform);
                Destroy(gameObject);
            }

            if (other.gameObject.tag == "DeadShot")
            {
                MapEffects("Prefabs/Explosions/Explosion1", "explosion1", this.transform);
                Destroy(gameObject);
            }
        }
    }
}
