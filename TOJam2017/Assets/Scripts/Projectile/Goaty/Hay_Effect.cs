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
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
            Destroy(gameObject, 30.0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<ControllerScript>().speedImpairment = impairment;
            }

            if (other.tag == "Map")
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 0;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<ControllerScript>().speedImpairment = defaultImpaired;
            }
        }
    }
}
