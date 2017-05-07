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
            this.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 30.0f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<ControllerScript>().speedImpairment = impairment;
            }

            if (other.tag == "Map")
            {
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
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
    }
}
