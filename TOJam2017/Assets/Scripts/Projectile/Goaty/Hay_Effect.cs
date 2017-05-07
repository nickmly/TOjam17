using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Hay_Effect : Goaty
    {
        GameObject player;

        // ------ Base mobile attributes ------
        private float defaultImpaired = 1.0f;
        private float impairment = 0.4f;
        // ------ Base mobile attributes ------

        protected override void Start()
        {
            StartCoroutine(DeadTime());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<ControllerScript>().speedImpairment = impairment;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<ControllerScript>().speedImpairment = defaultImpaired;
            }
        }

        IEnumerator DeadTime()
        {
            yield return new WaitForSeconds(25.0f);
            Destroy(gameObject);
        }
    }
}
