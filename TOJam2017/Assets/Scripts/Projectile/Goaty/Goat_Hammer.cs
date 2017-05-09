using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllMobiles
{

    public class Goat_Hammer : Goaty
    {
        GameObject player;
        GameObject babyGoat;
        public GameObject[] playerTargets;

        // ------ Base mobile attributes ------
        private int bDamage = 10;
        public bool babyGoatStart = false;
        // ------ Base mobile attributes ------

        // Use this for initialization
        protected override void Start()
        {
            StartCoroutine(CleanUp());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        void Update()
        {
            if (!babyGoatStart)
            {
                babyGoat = GameObject.Find("babyGoat(Clone)");
            }
            if (babyGoatStart)
            {
                babyGoat.transform.position = Vector2.MoveTowards(babyGoat.transform.position, this.transform.position, 0.1f);
            }

            playerTargets = GameObject.FindGameObjectsWithTag("Player");
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Hit");

            if (other.gameObject.tag == "Player")
            {
                babyGoatStart = true;
                player = other.gameObject;
                player.GetComponent<AllMobiles.Mobiles>().TakeDamage(bDamage);

                this.transform.rotation = Quaternion.identity;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.GetComponent<PolygonCollider2D>().isTrigger = true;
            }

            if (other.gameObject.tag == "Map")
            {
                babyGoatStart = true;
                this.transform.rotation = Quaternion.identity;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                this.GetComponent<PolygonCollider2D>().isTrigger = true;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Map")
            {
                this.transform.rotation = Quaternion.identity;
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }

            if (other.gameObject.name == "babyGoat(Clone)")
            {
                babyGoatStart = false;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        IEnumerator CleanUp()
        {
            yield return new WaitForSeconds(10.0f);

            foreach (GameObject target in playerTargets)
            {
                target.GetComponent<PolygonCollider2D>().isTrigger = false;
                target.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            Destroy(gameObject);
        }
    }
}
