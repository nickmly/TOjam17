using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Baby_Goat : Goaty
    {
        GameObject player;
        GameObject hammerGoat;
        GameObject[] playerTargets;

        // ------ Base mobile attributes ------
        private int bDamage = 10;

        private float stampedeDelay = 0.3f;
        // ------ Base mobile attributes ------


        protected override void OnDestroy()
        {
            base.OnDestroy();
        }


        void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.tag == "Player")
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                playerTargets = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject target in playerTargets)
                {
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                    target.GetComponent<BoxCollider>().isTrigger = true;
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
            for (int i = 0; i <= 4; i++)
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
                target.GetComponent<BoxCollider>().isTrigger = false;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }
            Destroy(hammerGoat);
            Destroy(gameObject);
        }
    }
}
