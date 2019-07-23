using UnityEngine;
using System.Collections;
using System.Linq;


public static class Helper
{
    public static GameObject FindInChildren(this GameObject go, string name)
    {
        return (from x in go.GetComponentsInChildren<Transform>()
                where x.gameObject.name == name
                select x.gameObject).First();
    }
}

public class ATSTscript : MonoBehaviour {

public Transform head;
public Transform cannon;
    public Transform leftLeg;
    public Transform leftlowerLeg;
    public Transform leftankle;
    public Transform leftfoot;

    public Transform rightLeg;
    public Transform rightlowerLeg;
    public Transform rightankle;
    public Transform rightfoot;

    public bool isRunning;
    public int speed;

    public float enemyHealth;
    public float sightRange;
    public GameObject laser;
    public GameObject cannons;
    public Transform target;
    public float Enemydistance;
    public float angle;
    public float rotateSpeed;
    public float headrotateSpeed;
    public AudioSource[] sounds;
    public AudioSource shoot;
    public float fireRate = 1.0f;
    public float nextFire = 0f;
    public Quaternion targetRotation;
    public Quaternion headtargetRotation;
    public Quaternion cannonRotation;
    public GameObject explosion;
    public bool canWalk;

    // Use this for initialization
    void Start()
    {
        head = gameObject.FindInChildren("obj1").transform;
        cannon = gameObject.FindInChildren("obj3").transform;
        leftLeg = gameObject.FindInChildren("obj01").transform;
        leftlowerLeg = gameObject.FindInChildren("obj04").transform;
        leftankle = gameObject.FindInChildren("obj06").transform;
        leftfoot = gameObject.FindInChildren("obj05").transform;

        rightLeg = gameObject.FindInChildren("obj2").transform;
        rightlowerLeg = gameObject.FindInChildren("obj6").transform;
        rightankle = gameObject.FindInChildren("obj8").transform;
        rightfoot = gameObject.FindInChildren("obj7").transform;

        cannons = gameObject.FindInChildren("cannons");
        target = null;
        sounds = GetComponents<AudioSource>();
        shoot = sounds[0];

        canWalk = false;


    }

    public static GameObject FindInChildren(GameObject gameObject, string name)
    {
        foreach (Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
            if (t.name == name)
                return t.gameObject;
        }

        return null;
    }
    void Update()
    {
        if(canWalk)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            if (!isRunning)
            {
                StartCoroutine("walk");
            }
            
        }
        else
        {
            if(isRunning)
            {
                StopAllCoroutines();
            }
        }

        if (target)
        {
            Enemydistance = Vector3.Distance(target.transform.position, head.transform.position);
            //Finds the current difference in distance between player and enemy 
            Vector3 playerDir = target.transform.position - head.transform.position;
            //Calculate angle between forward vector of player and enemy
            //angle = Vector3.Angle(turbotop.transform.forward, playerDir);
            angle = Vector3.Angle(head.transform.forward, playerDir);

            //If distance to target is less than or equal to sight range, hasTarget turns true
            if (Enemydistance <= sightRange)
            {
                if(!canWalk)
                {
                    canWalk = true;
                }
                // Look at Player
                headtargetRotation = Quaternion.LookRotation(target.transform.position - head.transform.position);
                targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                cannonRotation = Quaternion.LookRotation(target.transform.position - cannon.transform.position);
                targetRotation.z = 0;
                targetRotation.x = 0;
                headtargetRotation.z = 0;
                headtargetRotation.x = Mathf.Clamp(headtargetRotation.x, -22, 4);
                cannonRotation.z = 0;
                cannonRotation.y = 0;
                cannonRotation.x = Mathf.Clamp(cannonRotation.x, -20, 20);
                //head.transform.rotation = Quaternion.Slerp(head.transform.rotation, headtargetRotation, headrotateSpeed);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);

                head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, headtargetRotation, headrotateSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
                cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, cannonRotation, headrotateSpeed * 10f);
                cannon.localEulerAngles = new Vector3(cannon.localEulerAngles.x, 0,0);

            }
            else
            {
                if(canWalk)
                {
                    canWalk = false;
                }
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }




            //Check if distance to target is less than or equal to attack range and angle to target is less than or equal to 5 degrees
            if (Enemydistance <= sightRange && angle <= 5)
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
            GameObject laser1 = Instantiate(laser, cannons.transform.position, cannons.transform.rotation) as GameObject;
            laser1.GetComponent<Rigidbody>().AddForce(head.transform.forward * 200f, ForceMode.Impulse);
            //Play laser fire sound
            shoot.Play();
        }
    }
    public IEnumerator walk()
    {
        isRunning = true;
        StartCoroutine(RotateOverTime(leftLeg.transform, Quaternion.Euler(25, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftlowerLeg.transform, Quaternion.Euler(-35, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftankle.transform, Quaternion.Euler(-15, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftfoot.transform, Quaternion.Euler(35, 0, 0), 1f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(RotateOverTime(leftfoot.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftLeg.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftlowerLeg.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(leftankle.transform, Quaternion.Euler(0, 0, 0), 1f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(RotateOverTime(rightLeg.transform, Quaternion.Euler(25, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightlowerLeg.transform, Quaternion.Euler(-35, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightankle.transform, Quaternion.Euler(-15, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightfoot.transform, Quaternion.Euler(35, 0, 0), 1f));
        yield return new WaitForSeconds(1f);
        StartCoroutine(RotateOverTime(rightfoot.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightLeg.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightlowerLeg.transform, Quaternion.Euler(0, 0, 0), 1f));
        StartCoroutine(RotateOverTime(rightankle.transform, Quaternion.Euler(0, 0, 0), 1f));
        yield return new WaitForSeconds(1f);
        isRunning = false;
    }

    static IEnumerator RotateOverTime(Transform theTransform, Quaternion d, float t)
    {
        float rate = 1 / t;
        float index = 0f;
        Quaternion startPosition = theTransform.localRotation;
        Quaternion endPosition = d;
        while (index < 1)
        {

            theTransform.localRotation = Quaternion.Lerp(startPosition, endPosition, index);
            index += rate * Time.deltaTime;
            yield return index;
        }
        theTransform.localRotation = endPosition;
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
            //Set enemy health back 20 points
            enemyHealth -= 10f;

            //Check if enemy health is 0 or less
            if (enemyHealth <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        //Check to see if ship collided with torpedo
        if (col.name == "protonTorpedo(Clone)")
        {
            //Destroy torpedo
            Destroy(col.gameObject);
            enemyHealth -= 75f;
            //Check if enemy health is 0 or less
            if (enemyHealth <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    }
}
