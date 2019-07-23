using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class enemyControls : MonoBehaviour
{

  public float enemyHealth;
  public float speed;
  public float sightRange;
  public float attackRange;
  public float fireRate = 1f;
  public float nextFire = 0f;
  public float angle;
  public Transform cannon;
  public Transform cannon2;
  public Transform target;
  public float Enemydistance;
  private Transform ship;
  public AudioSource[] sounds;
  public AudioSource shoot;
  public AudioSource flyby;
  public bool hasHit;
  public GameObject explosion;
  public float rotateSpeed;
  public GameObject greenlaser;
  public bool canWander = false;
  public Vector3 randomDirection;

  void Start()
  {
    //Bind objects transform
    ship = transform;

    //Bind cannons from ship
    cannon = ship.Find("cannon1");
    cannon2 = ship.Find("cannon2");

    //Starts out with no hit detections
    hasHit = false;

    //Initialize sound bank
    sounds = GetComponents<AudioSource>();
    shoot = sounds[0];
    flyby = sounds[2];

    //Rate of fire
    fireRate = 1.0f;

    //Range of attack
    attackRange = 200;

    //Range to see enemies
    sightRange = 800;

    //Adjust rotate speed per ship type
    if (transform.name.Contains("TieParent"))
    {
      rotateSpeed = 0.8f;
    }
    if (transform.name.Contains("Tie_Interceptor"))
    {
      rotateSpeed = 1.1f;
    }
    if (transform.name.Contains("Tie_Bomber"))
    {
      rotateSpeed = 0.5f;
    }
    if (transform.name.Contains("Tie_Defender"))
    {
      rotateSpeed = 1.0f;
    }
  }

  void Update()
  {
    //If ship has no constraints applied, freeze rotation
    if (ship.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.None)
    {
      ship.GetComponent<Rigidbody>().freezeRotation = true;
    }

    //If enemy has target, and it is currently the Player
    if (target != null && target.name.Contains("Player"))
    {
      PlayerControls PlayerScript = target.GetComponent<PlayerControls>();
      if (PlayerScript.died)//If Player has died, remove target
      {
        target = null;
      }
    }



    //If target is valid
    if (target)
    {
      //If is currently wandering
      if (IsInvoking("Wander"))
      {   //Stop wandering
        CancelInvoke("Wander");
      }

      //Calculate distance between object and target
      Enemydistance = (target.transform.position - ship.position).magnitude;

      //Set so cannot wander
      canWander = false;

      //Finds the current difference in distance between player and enemy 
      Vector3 playerDir = target.position - transform.position;
      //Calculate angle between forward vector of player and enemy
      angle = Vector3.Angle(transform.forward, playerDir);
      //Check if distance to target is less than or equal to attack range and angle to target is less than or equal to 5 degrees
      if (Enemydistance <= attackRange && angle <= 2)
      {   //Execute Shoot method
        Shoot();
      }

      //If Enemy within 50
      if (Enemydistance <= 50f)
      {   //Play fly by sound
        flyby.Play();
      }
    }
    //If does not have Target
    else if (!target)
    {   //If currently not wandering
      if (!IsInvoking("Wander"))
      {   //Start wandering
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
    //Rigidbody rigid = transform.GetComponent<Rigidbody>();
    //rigid.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
    //rigid.drag = 1.0f;

    //Keep ship moving forward at ship speed
    transform.position += transform.forward * speed * Time.deltaTime;

    //Setup vectors for obstacle avoidance
    Vector3 fwd = transform.TransformDirection(Vector3.forward);
    Vector3 lft = transform.TransformDirection(Vector3.left);
    Vector3 rgt = transform.TransformDirection(Vector3.right);
    Vector3 top = transform.TransformDirection(Vector3.up);
    Vector3 btm = transform.TransformDirection(Vector3.down);
    float x = transform.rotation.x;
    float y = transform.rotation.y;
    //Draw rays for 5 seconds
    // Debug.DrawRay(transform.position, fwd, Color.green, 5);
    // Debug.DrawRay(transform.position, top, Color.green, 5);
    // Debug.DrawRay(transform.position, rgt, Color.green, 5);
    // Debug.DrawRay(transform.position, btm, Color.green, 5);
    // Debug.DrawRay(transform.position, lft, Color.green, 5);

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
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y + 45f, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, rgt, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y - 45f, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, top, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x + 45f, 0, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    if (Physics.Raycast(transform.position, btm, 50))
    {
      hasHit = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(x - 45f, 0, 0), rotateSpeed * Time.deltaTime);
    }
    else
    {
      hasHit = false;
    }

    //If not currently detecting obstacle
    if (!hasHit)
    {
      //If target is valid
      if (target)
      {
        //Vector3 TargetDirection = target.position - transform.position;
        //Look at and fly towards target
        //var RollAngle = Quaternion.LookRotation(TargetDirection, Vector3.forward);
        //RollAngle.y = 0;
        //var PitchAngle = Quaternion.LookRotation(TargetDirection, Vector3.forward);
        //PitchAngle.x = 0;
        //transform.rotation = Quaternion.Slerp(transform.rotation, PitchAngle, Time.deltaTime * rotateSpeed);
        //transform.rotation = Quaternion.Slerp(transform.rotation, RollAngle, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotateSpeed * Time.deltaTime);
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
      GameObject laser1 = Instantiate(greenlaser, cannon.transform.position, cannon.transform.rotation) as GameObject;
      GameObject laser2 = Instantiate(greenlaser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
      laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (speed * 10f), ForceMode.Impulse);
      laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (speed * 10f), ForceMode.Impulse);
      shoot.Play();
    }
  }

  void FindTarget()
  {
    //List<Transform> targetTransforms = new List<Transform>();
    var possibleTargets = GameObject.FindGameObjectsWithTag("Rebel");
    //foreach(GameObject go in possibleTargets)
    //{
    //targetTransforms.Add(go.transform);
    //}
    if (possibleTargets.Length != 0)
    {
      target = possibleTargets[Random.Range(0, possibleTargets.Length)].transform;
      if (target.name.Contains("Player"))
      {
        PlayerControls PlayerScript = target.GetComponent<PlayerControls>();
        if (PlayerScript.died)
        {
          target = null;
        }
      }
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
    //Choose a random direction
    randomDirection = new Vector3(Random.value, Random.value, Random.value);
    //Allow wandering
    canWander = true;
  }


  void OnCollisionEnter(Collision col)
  {
    //Check to see if ship collided with Enemy
    if (col.collider.tag == "Rebel")
    {
      enemyHealth -= 300;
      if (enemyHealth <= 0)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        if (Application.loadedLevelName.Contains("level5"))
        {
          Destroy(gameObject);
        }
        else
        {
          CheckDead();
        }

      }
    }
    else if (col.collider.tag == "structure")
    {   //Create explosion on ship position
      Instantiate(explosion, transform.position, transform.rotation);

      //Destroy ship
      if (Application.loadedLevelName.Contains("level2") || Application.loadedLevelName.Contains("level3") || Application.loadedLevelName.Contains("level4"))
      {
        CheckDead();
      }
      else
      {
        Destroy(gameObject);
      }
    }
    //Check to see if ship collided with Friendly
    else if (col.collider.tag == "Empire")
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
    GameObject SD = GameObject.Find("StarDestroyer");
    StarDestoyerScript SDScript = SD.GetComponent<StarDestoyerScript>();
    if (transform.name.Contains("TieParent"))
    {
      SDScript.TCount += 1;
      SDScript.TcanSpawn = true;
    }
    else if (transform.name.Contains("Tie_Interceptor"))
    {
      SDScript.TICount += 1;
      SDScript.TIcanSpawn = true;
    }
    else if (transform.name.Contains("Tie_Bomber"))
    {
      SDScript.TBCount += 1;
      SDScript.TBcanSpawn = true;
    }
    if (GameObject.Find("YPlayer") != null)
    {
      PlayerControls YPlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
      YPlayerScript.rebelScore += 1;
    }
    else if (GameObject.Find("XPlayer") != null)
    {
      PlayerControls XPlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
      XPlayerScript.rebelScore += 1;
    }
    else if (GameObject.Find("APlayer") != null)
    {
      PlayerControls APlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
      APlayerScript.rebelScore += 1;
    }
    else if (GameObject.Find("TPlayer") != null)
    {
      PlayerControls TPlayerScript = GameObject.Find("TPlayer").GetComponent<PlayerControls>();
      TPlayerScript.rebelScore += 1;
    }
    else if (GameObject.Find("FPlayer") != null)
    {
      PlayerControls FPlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
      FPlayerScript.rebelScore += 1;
    }

    Destroy(gameObject);

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
      if (enemyHealth <= 0)
      {
        if (col.tag == "PlayersLaser")
        {
          if (GameObject.Find("YPlayer") != null)
          {
            PlayerControls YPlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
            YPlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("XPlayer") != null)
          {
            PlayerControls XPlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
            XPlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("APlayer") != null)
          {
            PlayerControls APlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
            APlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("TPlayer") != null)
          {
            PlayerControls TPlayerScript = GameObject.Find("TPlayer").GetComponent<PlayerControls>();
            TPlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("FPlayer") != null)
          {
            PlayerControls FPlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
            FPlayerScript.playerKills += 1;
          }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (Application.loadedLevelName.Contains("level5"))
        {
          Destroy(gameObject);
        }
        else
        {
          CheckDead();
        }
      }
    }
    //Check to see if ship collided with ion laser
    else if (col.name == "ionlaser(Clone)")
    {
      //Destroy ion laser
      Destroy(col.gameObject);
      //Slow enemy speed down 2 points
      speed -= 1f;
      if (speed <= 5)
      {
        speed = 5;
      }
    }
    //Check to see if ship collided with redlaser
    else if (col.name == "protonTorpedo(Clone)")
    {
      //Destroy red laser
      Destroy(col.gameObject);
      //Set enemy health back 50 points
      enemyHealth -= 300f;
      if (enemyHealth <= 0)
      {
        if (col.tag == "PlayersMissile")
        {
          if (GameObject.Find("YPlayer") != null)
          {
            PlayerControls YPlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
            YPlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("XPlayer") != null)
          {
            PlayerControls XPlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
            XPlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("APlayer") != null)
          {
            PlayerControls APlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
            APlayerScript.playerKills += 1;
          }
          else if (GameObject.Find("FPlayer") != null)
          {
            PlayerControls FPlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
            FPlayerScript.playerKills += 1;
          }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (Application.loadedLevelName.Contains("level5"))
        {
          Destroy(gameObject);
        }
        else
        {
          CheckDead();
        }
      }
    }
  }
}
