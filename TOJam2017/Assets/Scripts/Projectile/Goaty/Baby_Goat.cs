using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Baby_Goat : Goaty
    {
        GameObject player;
        GameObject hammerGoat;
        public GameObject[] playerTargets;

        // ------ Base mobile attributes ------
        private int bDamage = 10;

        private float stampedeDelay = 0.5f;
        // ------ Base mobile attributes ------

        void Update()
        {
            playerTargets = GameObject.FindGameObjectsWithTag("Player");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }


        void OnCollisionEnter2D(Collision2D other)
        {

            if (other.gameObject.tag == "Player")
            {
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                foreach (GameObject target in playerTargets)
                {
                    target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    target.GetComponent<PolygonCollider2D>().isTrigger = true;
                }
                hammerGoat = GameObject.Find("goatHammer(Clone)");
                hammerGoat.GetComponent<Goat_Hammer>().babyGoatStart = false;
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                StartCoroutine(MultiShots());
                Destroy(gameObject, 10.0f);
            }

            if (other.gameObject.name == "goatStampede(Clone)")
            {
                StartCoroutine(CleanUp());
            }
        }

        IEnumerator MultiShots()
        {
            for (int i = 0; i <= 2; i++)
            {
                InstEffects("Goaty/Attack", "goatStampede", hammerGoat.transform);
                yield return new WaitForSeconds(stampedeDelay);
            }
        }

        IEnumerator CleanUp()
        {
            yield return new WaitForSeconds(2.0f);
            
            foreach (GameObject target in playerTargets)
            {
                target.GetComponent<PolygonCollider2D>().isTrigger = false;
                target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            Destroy(hammerGoat);
            Destroy(gameObject);
        }
    }
}
