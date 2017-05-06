using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Baby_Goat : Goaty
    {
        GameObject player;
        GameObject hammerGoat;

        // ------ Base mobile attributes ------
        private int bDamage = 10;
        // ------ Base mobile attributes ------

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hit");

            if (other.gameObject.tag == "Player")
            {
                hammerGoat = GameObject.Find("goatHammer(Clone)");
                hammerGoat.GetComponent<Goat_Hammer>().babyGoatStart = false;
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);
                for (int i = 0; i <= 3; i++)
                {
                    InstEffects("Goaty/Attack", "goatStampede", hammerGoat.transform);
                }
            }
        }
    }
}
