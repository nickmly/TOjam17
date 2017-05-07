using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{
    public class Gravity_Effect : Skitty
    {
        GameObject player;

        protected override void Start()
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, 1.0f);
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                other.transform.position = Vector2.MoveTowards(other.transform.position, this.transform.position, 0.06f);
            }

            if (other.tag == "Map")
            {
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}

