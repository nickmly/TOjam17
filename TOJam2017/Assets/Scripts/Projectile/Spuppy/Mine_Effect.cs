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
            StartCoroutine(DeadTime());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;

                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                Destroy(gameObject, 1.0f);
            }
        }

        IEnumerator DeadTime()
        {
            yield return new WaitForSeconds(25.0f);
            Destroy(gameObject);
        }
    }
}
