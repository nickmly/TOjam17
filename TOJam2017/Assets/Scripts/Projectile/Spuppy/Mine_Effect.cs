using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Mine_Effect : Goaty
    {
        GameObject player;

        // ------ Base mobile attributes ------
        private int bDamage = 10;
        // ------ Base mobile attributes ------

        protected override void Start()
        {
            Destroy(gameObject, 30.0f);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                MapEffects("Explosions/Explosion1", "explosion1", this.transform);
                Destroy(gameObject);
            }
        }
    }
}
