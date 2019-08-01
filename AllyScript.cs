using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AllyScript : MonoBehaviour
{

  public float allyHealth;
  public float speed;
  public float sightRange;
  public float attackRange;
  public float fireRate = 0.5f;
  public float nextFire = 0f;
  public float angle;
  public bool hasAllies;
  public Transform cannon1;
  public Transform cannon2;
  public Transform cannon3;
  public Transform cannon4;
  public Transform target;
  public Transform friendly;
  public float Enemydistance;
  private Transform ship;
  public AudioSource[] sounds;
  public AudioSource shoot;
  public GameObject explosion;

  public float rotateSpeed;

  public GameObject redlaser;
  public bool canWander = false;
  public Vector3 randomDirection;

  public bool hasHit;

  void Start()
  {

    ship = transform;
    cannon1 = ship.Find("cannon1");
    cannon2 = ship.Find("cannon2");
    hasAllies = false;
    attackRange = 300;
    sounds = GetComponents<AudioSource>();
    shoot = sounds[0];
    hasHit = false;
    fireRate = 1.0f;
    attackRange = 200;
    sightRange = 800;
    if (transform.name.Contains("XWingAlly"))
    {
      rotateSpeed = 0.8f;
    }
    if (transform.name.Contains("AWingAlly"))
    {
      rotateSpeed = 1.1f;
    }
    if (transform.name.Contains("YWingAlly"))
    {
      rotateSpeed = 0.5f;
    }

  }

  void Update()
  {
    if (ship.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.None)
    {
      ship.GetComponent<Rigidbody>().freezeRotation = true;
    }


    //If hasTarget is true
    if (target)
    {
      if (IsInvoking("Wander"))
      {
        CancelInvoke("Wander");
      }
      Enemydistance = Vector3.Distance(target.transform.position, ship.position);
      canWander = false;
      //Finds the current difference in distance between player and enemy 
      Vector3 playerDir = target.position - transform.position;
      //Calculate angle between forward vector of player and enemy
      angle = Vector3.Angle(transform.forward, playerDir);


      //Check if distance to target is less than or equal to attack range and angle to target is less than or equal to 5 degrees
      if (Enemydistance <= attackRange && angle <= 3)
      {   //Execute Shoot method
        Shoot();
      }
    }
    //If Target is false
    else if (!target)
    {
      if (!IsInvoking("Wander"))
      {
        InvokeRepeating("Wander", 5f, 15f);
      }
      //Execute FindTarget method
      FindTarget();
    }

    if (canWander)
    {
      transform.Rotate(randomDirection * Time.deltaTime);
    }
  }


  void FixedUpdate()
  {

    // Rigidbody rigid = transform.GetComponent<Rigidbody>();
    // rigid.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    // rigid.drag = 1.0f;

    transform.position += transform.forward * speed * Time.deltaTime;

    Vector3 fwd = transform.TransformDirection(Vector3.forward);
    Vector3 lft = transform.TransformDirection(Vector3.left);
    Vector3 rgt = transform.TransformDirection(Vector3.right);
    Vector3 top = transform.TransformDirection(Vector3.up);
    Vector3 btm = transform.TransformDirection(Vector3.down);
    float x = transform.rotation.x;
    float y = transform.rotation.y;
    // Debug.DrawRay(transform.position, transform.forward, Color.red, 5);
    // Debug.DrawRay(transform.position, transform.up, Color.red, 5);
    // Debug.DrawRay(transform.position, transform.right, Color.red, 5);
    // Debug.DrawRay(transform.position, -transform.up, Color.red, 5);
    // Debug.DrawRay(transform.position, -transform.right, Color.red, 5);

    if (Physics.Raycast(transform.position, fwd, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x + 10f, 0, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, lft, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y + 10f, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, rgt, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y - 10f, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, top, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x + 10f, 0, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, btm, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x - 10f, 0, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (!hasHit)
    {
      if (target)
      {
        // Vector3 TargetDirection = target.position - transform.position;
        // // Look at and fly towards target
        // var RollAngle = Quaternion.LookRotation(TargetDirection, Vector3.forward);
        // RollAngle.y = 0;
        // var PitchAngle = Quaternion.LookRotation(TargetDirection, Vector3.forward);
        // PitchAngle.x = 0;
        // transform.rotation = Quaternion.Slerp(transform.rotation, PitchAngle, Time.deltaTime * rotateSpeed);
        // transform.rotation = Quaternion.Slerp(transform.rotation, RollAngle, Time.deltaTime * rotateSpeed);

        // Look at and fly towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotateSpeed * Time.deltaTime);
        //transform.position += transform.forward * speed * Time.deltaTime;
      }

    }
  }

  void Shoot()
  {
    //If time is greater than nextFire
    if (Time.time > nextFire)
    {   //nextFire becomes current variable added to fireRate variable 
      nextFire = Time.time + fireRate;
      //Create laser prefab on cannons position
      GameObject laser1 = Instantiate(redlaser, cannon1.transform.position, cannon1.transform.rotation) as GameObject;
      GameObject laser2 = Instantiate(redlaser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
      laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (speed * 10f), ForceMode.Impulse);
      laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (speed * 10f), ForceMode.Impulse);
      shoot.Play();
    }

  }

  void FindTarget()
  {
    //List<Transform> targetTransforms = new List<Transform>();
    var possibleTargets = GameObject.FindGameObjectsWithTag("Empire");
    //foreach(GameObject go in possibleTargets)
    //{
    //targetTransforms.Add(go.transform);
    //}
    if (possibleTargets.Length != 0)
    {
      target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform;
    }
    else
    {
      target = null;
    }
    //target = GetClosestEnemy(targetTransforms);

  }

  // Transform GetClosestEnemy (List<Transform> enemies)
  // {
  //     Transform bestTarget = null;
  //     float closestDistanceSqr = Mathf.Infinity;
  //     Vector3 currentPosition = transform.position;
  //     foreach(Transform potentialTarget in enemies)
  //     {
  //         Vector3 directionToTarget = potentialTarget.position - currentPosition;
  //         float dSqrToTarget = directionToTarget.sqrMagnitude;
  //         if(dSqrToTarget < closestDistanceSqr)
  //         {
  //             closestDistanceSqr = dSqrToTarget;
  //             bestTarget = potentialTarget;
  //         }
  //     }

  //     return bestTarget;
  // }

  public void Wander()
  {
    randomDirection = new Vector3(Random.value, Random.value, Random.value);
    canWander = true;
  }


  void OnCollisionEnter(Collision col)
  {
    //Check to see if ship collided with Player(YWing)
    if (col.collider.tag == "Empire")
    {
      allyHealth -= 300;
      if (allyHealth <= 0)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        CheckDead();
      }

    }
    else if (col.collider.tag == "structure")
    {
      Instantiate(explosion, transform.position, transform.rotation);
      //Destroy ship
      if (Application.loadedLevelName == "level1" || Application.loadedLevelName == "level2" || Application.loadedLevelName == "level1tie" || Application.loadedLevelName == "level2tie")
      {
        CheckDead();
      }
      else
      {
        Destroy(gameObject);
      }

    }
    //Check to see if ship collided with Friendly
    else if (col.collider.tag == "Rebel")
    {
      //Debug.Log("Collided with " + col.collider.name);
      //hasHit = true;
      ////Turn gravity on to pull ship down to ground
      //ship.GetComponent<Rigidbody>().useGravity = true;
    }
    //Check to see if ship collided with Terrain
    else if (col.collider.name == "Terrain")
    {
      Instantiate(explosion, transform.position, transform.rotation);
      //Destroy ship
      Destroy(gameObject);

    }

  }

  void CheckDead()
  {
    GameObject SP = GameObject.Find("SpawnPoint");
    SpawnPointScript SPScript = SP.GetComponent<SpawnPointScript>();
    if (transform.name.Contains("XWingAlly"))
    {
      SPScript.XallyCount += 1;
      SPScript.XcanSpawn = true;
    }
    else if (transform.name.Contains("YWingAlly"))
    {
      SPScript.YallyCount += 1;
      SPScript.YcanSpawn = true;
    }
    else if (transform.name.Contains("AWingAlly"))
    {
      SPScript.AallyCount += 1;
      SPScript.AcanSpawn = true;
    }


    if (GameObject.Find("TPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("TPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;
    }
    else if (GameObject.Find("XPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;

    }
    else if (GameObject.Find("APlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;

    }
    else if (GameObject.Find("YPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;

    }
    else if (GameObject.Find("FPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;
    }
    else if (GameObject.Find("SPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;
    }
    else if (GameObject.Find("NPlayer") != null)
    {
      PlayerControls PlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
      PlayerScript.impScore += 1;
    }

    Destroy(gameObject);
  }


  void OnTriggerEnter(Collider col)
  {
    //Check to see if ship collided with green laser
    if (col.name == "greenlaser(Clone)")
    {
      //Destroy green laser
      Destroy(col.gameObject);
      //Set ally health back 10 points
      allyHealth -= 10f;
      if (allyHealth <= 0)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        CheckDead();
      }
    }

  }

}
