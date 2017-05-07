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
            Destroy(gameObject, 1.0f);
        }

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                other.transform.position = Vector2.MoveTowards(other.transform.position, this.transform.position, 0.1f);
            }
        }
    }
}

