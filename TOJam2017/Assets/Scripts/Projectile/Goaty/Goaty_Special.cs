using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Goaty_Special : Goaty
    {
        GameObject player;
        GameObject babyGoat;
        GameObject[] playerTargets;

        // ------ Base mobile attributes ------
        private int bProjectileMass = 5;
        // ------ Base mobile attributes ------

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            ProjectilePosition = this.transform;
            ProjectileMass = bProjectileMass;

            Destroy(gameObject, 7.0f);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        // Update is called once per frame
        void Update()
        {
            babyGoat = GameObject.Find("babyGoat(Clone)");
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Map")
            {
                Vector2 newPosition = new Vector2(this.transform.position.x, this.transform.position.y + 20);
                this.transform.position = newPosition;
                InstEffects("Goaty/Attack", "goatHammer", this.transform);
                Destroy(gameObject);
            }
            if (other.gameObject.tag == "DeadShot")
            {
                playerTargets = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject target in playerTargets)
                {
                    target.GetComponent<PolygonCollider2D>().isTrigger = false;
                    target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }

                Destroy(babyGoat);
                Destroy(gameObject);
            }
        }
    }
}
