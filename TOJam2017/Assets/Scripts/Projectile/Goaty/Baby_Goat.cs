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

        private float stampedeDelay = 0.6f;
        // ------ Base mobile attributes ------

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hit");

            if (other.gameObject.tag == "Player")
            {
                playerTargets = GameObject.FindGameObjectsWithTag("Player");

                foreach(GameObject target in playerTargets)
                {
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                    target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    target.GetComponent<BoxCollider>().isTrigger = true;
                }

                hammerGoat = GameObject.Find("goatHammer(Clone)");
                hammerGoat.GetComponent<Goat_Hammer>().babyGoatStart = false;
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                StartCoroutine(MultiShots());
            }

            if (other.gameObject.name == "goatStampede(Clone)")
            {
                Destroy(other.gameObject);
                Destroy(gameObject, 4.0f);
            }
        }

        IEnumerator MultiShots()
        {
            for (int i = 0; i <= 4; i++)
            {
                InstEffects("Goaty/Attack", "goatStampede", hammerGoat.GetComponent<Goat_Hammer>().stampedeSpawn);
                yield return new WaitForSeconds(stampedeDelay);
            }
        }
    }
}
