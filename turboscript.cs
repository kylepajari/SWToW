using UnityEngine;
using System.Collections;

public class turboscript : MonoBehaviour {
    public float enemyHealth;
    public float sightRange;
    public GameObject laser;
    public Transform cannons;
    public Transform cannon1;
    public Transform cannon2;
    public Transform target;
    public Transform turbotop;
    public float Enemydistance;
    public float angle;
    public float cannonangle;
    public float rotateSpeed;
    public AudioSource[] sounds;
    public AudioSource shoot;
    public float fireRate = 0.5f;
    public float nextFire = 0f;
    public Quaternion targetRotation;
    public Quaternion cannonRotation;
    public GameObject explosion;
    public GameObject burningWreck;

    public Vector3 targetPosition;
    // Use this for initialization
    void Start () {
        turbotop = transform.Find("turbotop");
        cannons = turbotop.transform.Find("turbocannons");
        cannon1 = cannons.transform.Find("turbocannon");
        cannon2 = cannons.transform.Find("turbocannon (1)");
        target = null;
        sounds = GetComponents<AudioSource>();
        shoot = sounds[0];
        Enemydistance = 500;
    }
	
	// Update is called once per frame
	void Update () {

        if (target)
        {
            Enemydistance = Vector3.Distance(target.transform.position, turbotop.transform.position);
            //Finds the current difference in distance between player and enemy 
            Vector3 playerDir = target.transform.position - turbotop.transform.position;
            //Calculate angle between forward vector of player and enemy
            //angle = Vector3.Angle(turbotop.transform.forward, playerDir);
            cannonangle = Vector3.Angle(cannons.transform.forward, playerDir);




            //Check if distance to target is less than or equal to attack range and angle to target is less than or equal to 5 degrees
            if (Enemydistance <= sightRange && cannonangle <=2)
            {   //Execute Shoot method
                Shoot();
                
            }
        }
        //If Target is false
        else if (!target)
        {
            //Execute FindTarget method
            FindTarget();
        }
    }

  void FixedUpdate()
  {
    if (target)
    {
        //If distance to target is less than or equal to sight range, hasTarget turns true
        if (Enemydistance <= sightRange)
        {
            // Look at Player
            targetPosition = target.transform.position;
            // get vector to player but take y coordinate from self to make sure we are not getting any rotation on the wrong axis
            Vector3 turretLookAt = new Vector3(targetPosition.x, turbotop.position.y, targetPosition.z);
   
            // create rotations for socket and gun
            Quaternion targetRotationTurret = Quaternion.LookRotation(turretLookAt - turbotop.position);
            Quaternion targetRotationCannon = Quaternion.LookRotation(targetPosition - cannons.position);
   
            // slerp rotations and assign
            turbotop.rotation = Quaternion.RotateTowards(turbotop.rotation, targetRotationTurret, Time.deltaTime * rotateSpeed);
            cannons.rotation = Quaternion.RotateTowards(cannons.rotation, targetRotationCannon, Time.deltaTime * rotateSpeed);
   
            // important: reset local euler angles rotation of gun to make sure that we are getting rotation only on one axis
            cannons.localEulerAngles = new Vector3(cannons.localEulerAngles.x, 0,0);
        }
    }
  }

    void FindTarget()
    {
        var possibleTargets = GameObject.FindGameObjectsWithTag("Rebel");

        if (possibleTargets.Length != 0)
        {
            target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        //If time is greater than nextFire
        if (Time.time > nextFire)
        {   //nextFire becomes current variable added to fireRate variable 
            nextFire = Time.time + fireRate;
            //Create laser prefab on cannons position
            GameObject laser1 = Instantiate(laser, cannon1.transform.position, cannon1.transform.rotation) as GameObject;
            GameObject laser2 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;

            laser1.GetComponent<Rigidbody>().AddForce(cannon1.transform.forward * 150f, ForceMode.Impulse);
            laser2.GetComponent<Rigidbody>().AddForce(cannon2.transform.forward * 150f, ForceMode.Impulse);
            //Play laser fire sound
            shoot.Play();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Check to see if ship collided with Player
        if (col.collider.name == "APlayer" || col.collider.name == "XPlayer" || col.collider.name == "YPlayer" || col.collider.name == "FPlayer" || col.collider.name == "TPlayer")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        //Check to see if ship collided with redlaser
        if (col.name == "redlaser(Clone)")
        {
            //Destroy red laser
            Destroy(col.gameObject);
            //Set enemy health back 10 points
            enemyHealth -= 10f;

            //Check if enemy health is 0 or less
            if (enemyHealth <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(burningWreck, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        //Check to see if ship collided with torpedo
        if (col.name == "protonTorpedo(Clone)")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(burningWreck, transform.position, transform.rotation);
            //Destroy torpedo
            Destroy(col.gameObject);
            Destroy(gameObject);

        }
    }
}
