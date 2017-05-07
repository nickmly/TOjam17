using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mobile Parent Class
/// Base stats and functionalities implemented in here
/// </summary>

namespace AllMobiles
{
    public class Mobiles : MonoBehaviour
    {
        // ------------ Components ------------
        private Collider col;
        private Health healthBar;
        // ------------ Components ------------

        // ------------ Attack Effect Prefabs and Spawn Location ------------
        private GameObject attackEffect;
        // ------------ Attack Effect Prefabs and Spawn Location ------------

        // ------------ Ammo Prefabs and Spawn Location ------------
        private GameObject ammoPref;
        private Transform ammoSpawn;
        // ------------ Ammo Prefabs and Spawn Location ------------

        // ------------ Mobile and Projectile Location ------------
        private Transform mobilePosition;
        private Transform projectilePosition;
        // ------------ Mobile and Projectile Location ------------

        // ------------ Base Stats ------------
        private int health;
        private int armour;
        private int mobileMass;
        private int projectileMass;
        // ------------ Base Stats ------------

        // ------------ Mobile Functionality ------------
        private float angle;
        private float mobility;
        private float force;

        public int attack;

        private int delay;
        private float timer = 30.0f;

        private bool isAlive;
        private bool turnEnd;
        // ------------ Mobile Functionality ------------


        protected virtual void Start()
        {
            col = GetComponent<Collider>();
            healthBar = GetComponent<Health>();
            if(healthBar != null)
            {
                healthBar.enabled = true;
                healthBar.Initialize();
            }
             
        }

        // ------------ Setting Stats ------------
        public Mobiles()
        {
            ammoSpawn = AmmoSpawn;
            mobilePosition = MobilePosition;
            health = Health;
            armour = Armour;
            mobileMass = MobileMass;
            projectileMass = ProjectileMass;
            angle = Angle;
            mobility = Mobility;
            force = Force;
            delay = Delay;
            isAlive = IsAlive;
            turnEnd = TurnEnd;
        }
        // ------------ Setting Stats ------------

        // ------------ Getters and Setters ------------
        public Transform AmmoSpawn
        {
            get { return ammoSpawn; }
            set { ammoSpawn = value; }
        }
        public Transform MobilePosition
        {
            get { return mobilePosition; }
            set { mobilePosition = value; }
        }
        public Transform ProjectilePosition
        {
            get { return projectilePosition; }
            set { projectilePosition = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Armour
        {
            get { return armour; }
            set { armour = value; }
        }
        public int MobileMass
        {
            get { return mobileMass; }
            set { mobileMass = value; }
        }
        public int ProjectileMass
        {
            get { return projectileMass; }
            set { projectileMass = value; }
        }
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public float Mobility
        {
            get { return mobility; }
            set { mobility = value; }
        }
        public float Force
        {
            get { return force; }
            set { force = value; }
        }
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        public bool TurnEnd
        {
            get { return turnEnd; }
            set { turnEnd = value; }
        }
        // ------------ Getters and Setters ------------

        // ------------ Functionalities ------------
        public virtual void AttackShot(float power) {}

        public virtual void Shoot(string folder, string ammo, float power)
        {
            GameMaster.gameMode.shotTimer.PauseTimer(); //Stop shot timer

            GameObject newAmmo = Instantiate(Resources.Load("MobileModels/" + folder + "/" + ammo), ammoSpawn.position, ammoSpawn.rotation) as GameObject;
            Rigidbody newAmmoRb = newAmmo.GetComponent<Rigidbody>();
            newAmmoRb.AddForce(ammoSpawn.transform.up * Force * power);
            Physics.IgnoreCollision(newAmmo.GetComponent<Collider>(), col);

            //check if skitty multi shot, only focus on first "notskitty" will be set to false after the first projectile is shot 
            //remains false for the rest of the attacks... 
            if (Camera.main.GetComponent<CameraMovement>().notSkitty)
            {
                Camera.main.GetComponent<CameraMovement>().FollowTarget(newAmmoRb.transform);
            }
            Camera.main.GetComponent<CameraMovement>().notSkitty = true;
            GameMaster.gameMode.currentProjectile = newAmmo.transform;
        }

        public virtual void InstEffects(string folder, string effect, Transform spawnPosition)
        {
            GameObject attackEffect = Instantiate(Resources.Load("MobileModels/" + folder + "/" + effect), spawnPosition.position, Quaternion.identity) as GameObject;
            GameMaster.gameMode.currentProjectile = attackEffect.transform;
            Camera.main.GetComponent<CameraMovement>().FollowTarget(attackEffect.transform);
        }

        public virtual void MapEffects(string folder, string effect, Transform spawnPosition)
        {
            GameObject attackEffect = Instantiate(Resources.Load("MobileModels/" + folder + "/" + effect), spawnPosition.position, Quaternion.identity) as GameObject;
        }

        public virtual void TakeDamage(int damageTaken)
        {
            Debug.Log("Damage taken: " + damageTaken);
            // *** Need a better method for the armour and damage calculation ****
            //Health = Health - (damageTaken - Armour);
            // *** Need a better method for the armour and damage calculation ****
            Health = Health - damageTaken;
            healthBar.TakeDamage(damageTaken);
        }
        // ------------ Functionalities ------------

        protected virtual void OnDestroy()
        {
            if (transform == GameMaster.gameMode.currentProjectile)
            {
                GameMaster.gameMode.AdvanceTurn();
            }
        }
    
    }
}
