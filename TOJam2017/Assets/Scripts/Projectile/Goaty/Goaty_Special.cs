using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Goaty_Special : Goaty
    {
        GameObject player;

        // ------ Base mobile attributes ------
        private int bProjectileMass = 5;
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

            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Map")
            {
                Vector2 newPosition = new Vector2(this.transform.position.x, this.transform.position.y + 30);
                this.transform.position = newPosition;
                InstEffects("Goaty/Attack", "goatHammer", this.transform);
                Destroy(gameObject, 1.0f);
            }
        }
    }
}
