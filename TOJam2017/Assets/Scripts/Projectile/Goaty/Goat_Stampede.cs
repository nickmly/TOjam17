using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Goat_Stampede : Goaty
    {
        GameObject player;
        GameObject babyGoat;

        // ------ Base mobile attributes ------
        private int bDamage = 10;
        public bool beginStampede;
        // ------ Base mobile attributes ------

        protected override void Start()
        {
            beginStampede = true;
            babyGoat = GameObject.Find("babyGoat(Clone)");
            Destroy(gameObject, 10.0f);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        void Update()
        {
            if (!babyGoat)
                Destroy(gameObject);

            if (beginStampede)
            {
                if (!babyGoat)
                    Destroy(gameObject);
                this.transform.position = Vector2.MoveTowards(this.transform.position, babyGoat.transform.position, 0.1f);
            }
           
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "DeadShot")
            {
                Destroy(gameObject);
            }

            if (other.gameObject.name == "babyGoat(Clone)")
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);
            }
        }
    }
}
