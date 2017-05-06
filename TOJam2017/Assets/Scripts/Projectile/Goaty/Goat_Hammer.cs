﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Goat_Hammer : Goaty
    {
        GameObject player;
        GameObject babyGoat;

        // ------ Base mobile attributes ------
        private int bDamage = 10;
        public bool babyGoatStart = false;
        // ------ Base mobile attributes ------

        // Use this for initialization
        protected override void Start()
        {
            babyGoat = GameObject.Find("babyGoat(Clone)");
        }

        void Update()
        {
            if (babyGoatStart)
            {
                babyGoat.transform.position = Vector2.MoveTowards(babyGoat.transform.position, this.transform.position, 0.02f);
            }
        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hit");

            if (other.gameObject.tag == "Player")
            {
                babyGoatStart = true;
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);
            }

            if (other.gameObject.tag == "Map")
            {
                babyGoatStart = true;
            }

            if (other.gameObject.name == "babyGoat(Clone)")
            {
                babyGoatStart = false;
            }
        }
    }
}