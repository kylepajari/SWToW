using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ATATscript : MonoBehaviour
{

  public Transform head;
  public Transform cannon1;
  public Transform cannon2;
  public Transform frontLeft_Hinge;
  public Transform frontRight_Hinge;
  public Transform backLeft_Hinge;
  public Transform backRight_Hinge;

  public Transform frontLeft_Thigh;
  public Transform frontRight_Thigh;
  public Transform backLeft_Thigh;
  public Transform backRight_Thigh;

  public Transform frontLeft_Shin;
  public Transform frontRight_Shin;
  public Transform backLeft_Shin;
  public Transform backRight_Shin;

  public Transform frontLeft_Ankle;
  public Transform frontRight_Ankle;
  public Transform backLeft_Ankle;
  public Transform backRight_Ankle;



  public bool isWalking;
  public bool stand;
  public int speed;

  public float enemyHealth;
  public float sightRange;
  public GameObject laser;
  public Transform target;

  public Transform WalkTarget;
  public float Enemydistance;
  public float angle;
  public float headrotateSpeed;
  public AudioSource[] sounds;

  public AudioSource walkSound;
  public AudioSource shoot;
  public float fireRate = 4.0f;
  public float nextFire = 0f;
  public float nextFire2 = 0f;
  public Quaternion targetRotation;
  public Quaternion headtargetRotation;
  public GameObject explosion;
  public GameObject sparks;
  public bool canWalk;

  public Transform ShieldGenerator;

  // Use this for initialization
  void Start()
  {
    head = gameObject.FindInChildren("head1").transform;
    cannon1 = gameObject.FindInChildren("cannon").transform;
    cannon2 = gameObject.FindInChildren("cannon2").transform;
    frontLeft_Hinge = gameObject.FindInChildren("frontleft_hinge").transform;
    frontRight_Hinge = gameObject.FindInChildren("frontright_hinge").transform;
    backLeft_Hinge = gameObject.FindInChildren("backleft_hinge").transform;
    backRight_Hinge = gameObject.FindInChildren("backright_hinge").transform;

    frontLeft_Thigh = gameObject.FindInChildren("fl_thigh").transform;
    frontRight_Thigh = gameObject.FindInChildren("fr_thigh").transform;
    backLeft_Thigh = gameObject.FindInChildren("bl_thigh").transform;
    backRight_Thigh = gameObject.FindInChildren("br_thigh").transform;

    frontLeft_Shin = gameObject.FindInChildren("fl_shin").transform;
    frontRight_Shin = gameObject.FindInChildren("fr_shin").transform;
    backLeft_Shin = gameObject.FindInChildren("bl_shin").transform;
    backRight_Shin = gameObject.FindInChildren("br_shin").transform;

    frontLeft_Ankle = gameObject.FindInChildren("fl_ankle").transform;
    frontRight_Ankle = gameObject.FindInChildren("fr_ankle").transform;
    backLeft_Ankle = gameObject.FindInChildren("bl_ankle").transform;
    backRight_Ankle = gameObject.FindInChildren("br_ankle").transform;

    target = null;
    if (transform.name == "AT-AT")
    {
      WalkTarget = GameObject.Find("AI-Destination1").transform;
    }
    else if (transform.name == "AT-AT (1)")
    {
      WalkTarget = GameObject.Find("AI-Destination2").transform;
    }
    else if (transform.name == "AT-AT (2)")
    {
      WalkTarget = GameObject.Find("AI-Destination3").transform;
    }

    ShieldGenerator = GameObject.Find("ShieldGenerator").transform;
    sounds = GetComponents<AudioSource>();
    shoot = sounds[0];
    walkSound = sounds[1];
    canWalk = true;
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
    if (canWalk)
    {
      transform.GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * (speed * 0.01f));
      transform.LookAt(WalkTarget);
    }
    else
    {
      StopCoroutine("walk");
      if (!stand)
      {
        StartCoroutine("returnToNeutral");
      }

    }
    if (!isWalking && canWalk)
    {
      StartCoroutine("walk");
    }

    if (target)
    {
      Enemydistance = Vector3.Distance(target.transform.position, head.transform.position);
      Vector3 targetDir = target.transform.position - head.transform.position;
      Vector3 newDir = Vector3.RotateTowards(head.transform.forward, targetDir, headrotateSpeed, 0.0f);
      float angle = Vector3.Angle(transform.forward, newDir);
      //If distance to target is less than or equal to sight range
      if (Enemydistance <= sightRange)
      {
        // Look at target
        // headtargetRotation = Quaternion.LookRotation(target.transform.position - head.transform.position);

        // head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, headtargetRotation, headrotateSpeed);
        // var rotX = Mathf.Clamp(head.transform.localEulerAngles.x, -5, 20);
        // var rotY = Mathf.Clamp(head.transform.localEulerAngles.y, 155, 205);
        // head.transform.rotation.x = rotX;
        // head.transform.rotation.y = rotY;
        if (angle < 65)
        {
          head.transform.rotation = Quaternion.LookRotation(newDir);
          Shoot();
        }

      }
      else
      {
        head.transform.localRotation = Quaternion.Euler(0, 180, 0);
      }

    }
    //If Target is false
    else if (!target)
    {
      //Execute FindTarget method
      if (canWalk)
      {
        FindTarget();
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
      shoot.Play();
      GameObject laser1 = Instantiate(laser, cannon1.transform.position, cannon1.transform.rotation) as GameObject;
      laser1.GetComponent<Rigidbody>().AddForce(head.transform.forward * 200f, ForceMode.Impulse);
    }
    if (Time.time > nextFire2)
    {
      nextFire2 = nextFire + 0.4f;
      GameObject laser2 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
      laser2.GetComponent<Rigidbody>().AddForce(head.transform.forward * 200f, ForceMode.Impulse);
    }
  }

  public IEnumerator returnToNeutral()
  {
    stand = true;
    StartCoroutine(RotateOverTime(transform, Quaternion.Euler(0, 180, 0), 1f));
    StartCoroutine(RotateOverTime(frontRight_Hinge.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(frontRight_Thigh.transform, Quaternion.Euler(0, 90, 0), 1f));
    StartCoroutine(RotateOverTime(frontRight_Shin.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(frontRight_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(frontLeft_Hinge.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(frontLeft_Thigh.transform, Quaternion.Euler(0, 90, 0), 1f));
    StartCoroutine(RotateOverTime(frontLeft_Shin.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(frontLeft_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backRight_Hinge.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backRight_Thigh.transform, Quaternion.Euler(0, 90, 0), 1f));
    StartCoroutine(RotateOverTime(backRight_Shin.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backRight_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backLeft_Hinge.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backLeft_Thigh.transform, Quaternion.Euler(0, 90, 0), 1f));
    StartCoroutine(RotateOverTime(backLeft_Shin.transform, Quaternion.Euler(0, 0, 0), 1f));
    StartCoroutine(RotateOverTime(backLeft_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));
    yield return new WaitForSeconds(1f);

  }
  public IEnumerator walk()
  {
    isWalking = true;
    //LEFT LEGS
    /////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////
    //right side legs keeping up with rotation
    StartCoroutine(RotateOverTime(frontRight_Hinge.transform, Quaternion.Euler(0, 0, 0), 2f));
    StartCoroutine(RotateOverTime(frontRight_Thigh.transform, Quaternion.Euler(0, 90, -15), 2f));
    StartCoroutine(RotateOverTime(backRight_Hinge.transform, Quaternion.Euler(0, 0, 0), 2f));
    StartCoroutine(RotateOverTime(backRight_Thigh.transform, Quaternion.Euler(0, 90, -15), 2f));
    StartCoroutine(RotateOverTime(frontRight_Ankle.transform, Quaternion.Euler(0, 0, 15), 2f));
    StartCoroutine(RotateOverTime(backRight_Ankle.transform, Quaternion.Euler(0, 0, 15), 2f));

    //front left hinge up, back hinge follows
    StartCoroutine(RotateOverTime(frontLeft_Hinge.transform, Quaternion.Euler(5, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(backLeft_Hinge.transform, Quaternion.Euler(20, 0, 0), 1f));
    //front left shin bending
    StartCoroutine(RotateOverTime(frontLeft_Shin.transform, Quaternion.Euler(0, 0, -50), 0.25f));
    //left side thighs and ankles moving
    StartCoroutine(RotateOverTime(frontLeft_Thigh.transform, Quaternion.Euler(0, 90, 40), 1f));
    StartCoroutine(RotateOverTime(backLeft_Thigh.transform, Quaternion.Euler(0, 90, -50), 1f));
    StartCoroutine(RotateOverTime(frontLeft_Ankle.transform, Quaternion.Euler(0, 0, -25), 1f));
    StartCoroutine(RotateOverTime(backLeft_Ankle.transform, Quaternion.Euler(0, 0, 30), 1f));


    yield return new WaitForSeconds(0.5f);
    //shin and hinge moving back down
    StartCoroutine(RotateOverTime(frontLeft_Shin.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontLeft_Hinge.transform, Quaternion.Euler(-15, 0, 0), 0.5f));
    yield return new WaitForSeconds(0.5f);


    //back left leg up, front hinge follows
    StartCoroutine(RotateOverTime(backLeft_Hinge.transform, Quaternion.Euler(-10, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontLeft_Hinge.transform, Quaternion.Euler(-5, 0, 0), 1f));
    //back left shin bending
    StartCoroutine(RotateOverTime(backLeft_Shin.transform, Quaternion.Euler(0, 0, -20), 0.25f));
    //left side thighs and ankles moving
    StartCoroutine(RotateOverTime(backLeft_Thigh.transform, Quaternion.Euler(0, 90, 10), 0.5f));
    StartCoroutine(RotateOverTime(frontLeft_Thigh.transform, Quaternion.Euler(0, 90, 15), 1f));
    StartCoroutine(RotateOverTime(backLeft_Ankle.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontLeft_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));

    yield return new WaitForSeconds(0.5f);
    //thigh and ankle moving back down
    StartCoroutine(RotateOverTime(backLeft_Ankle.transform, Quaternion.Euler(0, 0, -20), 0.5f));
    StartCoroutine(RotateOverTime(backLeft_Shin.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(backLeft_Hinge.transform, Quaternion.Euler(10, 0, 0), 0.5f));
    yield return new WaitForSeconds(0.5f);
    /////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////


    //RIGHT LEGS
    /////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////
    //left side legs keeping up with rotation
    StartCoroutine(RotateOverTime(frontLeft_Hinge.transform, Quaternion.Euler(-15, 0, 0), 2f));
    StartCoroutine(RotateOverTime(frontLeft_Thigh.transform, Quaternion.Euler(0, 90, 0), 2f));
    StartCoroutine(RotateOverTime(backLeft_Hinge.transform, Quaternion.Euler(15, 0, 0), 2f));
    StartCoroutine(RotateOverTime(backLeft_Thigh.transform, Quaternion.Euler(0, 90, -30), 2f));
    StartCoroutine(RotateOverTime(frontLeft_Ankle.transform, Quaternion.Euler(0, 0, 15), 2f));
    StartCoroutine(RotateOverTime(backLeft_Ankle.transform, Quaternion.Euler(0, 0, 15), 2f));

    //front right leg up
    StartCoroutine(RotateOverTime(frontRight_Hinge.transform, Quaternion.Euler(5, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(backRight_Hinge.transform, Quaternion.Euler(20, 0, 0), 1f));
    //shin bending
    StartCoroutine(RotateOverTime(frontRight_Shin.transform, Quaternion.Euler(0, 0, -50), 0.25f));
    //right side thighs and ankles moving
    StartCoroutine(RotateOverTime(frontRight_Thigh.transform, Quaternion.Euler(0, 90, 40), 1f));
    StartCoroutine(RotateOverTime(backRight_Thigh.transform, Quaternion.Euler(0, 90, -50), 1f));
    StartCoroutine(RotateOverTime(frontRight_Ankle.transform, Quaternion.Euler(0, 0, -25), 1f));
    StartCoroutine(RotateOverTime(backRight_Ankle.transform, Quaternion.Euler(0, 0, 30), 1f));

    yield return new WaitForSeconds(0.5f);
    //thigh and ankle moving back down
    StartCoroutine(RotateOverTime(frontRight_Shin.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontRight_Hinge.transform, Quaternion.Euler(-15, 0, 0), 0.5f));

    yield return new WaitForSeconds(0.5f);
    //back right leg up, front hinge follows
    StartCoroutine(RotateOverTime(backRight_Hinge.transform, Quaternion.Euler(-10, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontRight_Hinge.transform, Quaternion.Euler(-5, 0, 0), 1f));
    //back left shin bending
    StartCoroutine(RotateOverTime(backRight_Shin.transform, Quaternion.Euler(0, 0, -20), 0.25f));
    //right side thighs and ankles moving
    StartCoroutine(RotateOverTime(backRight_Thigh.transform, Quaternion.Euler(0, 90, 10), 0.5f));
    StartCoroutine(RotateOverTime(frontRight_Thigh.transform, Quaternion.Euler(0, 90, 15), 1f));
    StartCoroutine(RotateOverTime(backRight_Ankle.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(frontRight_Ankle.transform, Quaternion.Euler(0, 0, 0), 1f));

    yield return new WaitForSeconds(0.5f);
    //thigh and ankle moving back down
    StartCoroutine(RotateOverTime(backRight_Ankle.transform, Quaternion.Euler(0, 0, -20), 0.5f));
    StartCoroutine(RotateOverTime(backRight_Shin.transform, Quaternion.Euler(0, 0, 0), 0.5f));
    StartCoroutine(RotateOverTime(backRight_Hinge.transform, Quaternion.Euler(10, 0, 0), 0.5f));
    yield return new WaitForSeconds(0.5f);
    isWalking = false;
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

  // static IEnumerator MoveOverTime(Transform theTransform, Vector3 d, float t)
  // {
  //   float rate = 1 / t;
  //   float index = 0f;
  //   Vector3 startPosition = theTransform.position;
  //   Vector3 endPosition = startPosition + d;
  //   while (index < 1)
  //   {

  //     theTransform.position = Vector3.Lerp(startPosition, endPosition, index);
  //     index += rate * Time.deltaTime;
  //     yield return index;
  //   }
  //   theTransform.position = endPosition;
  // }

  static IEnumerator MoveOverTime(Transform theTransform, Vector3 d, float t)
  {
    float rate = 1 / t;
    float index = 0f;
    Vector3 startPosition = theTransform.localPosition;
    Vector3 endPosition = d;
    while (index < 1)
    {

      theTransform.localPosition = Vector3.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.localPosition = endPosition;
  }

  void OnTriggerEnter(Collider col)
  {
    //Check to see if ship collided with redlaser
    if (col.name == "redlaser(Clone)" || col.tag == "PlayersLaser")
    {
      //Destroy red laser
      Instantiate(sparks, col.transform.position, col.transform.rotation);
      Destroy(col.gameObject);
      //Set enemy health back 20 points
      enemyHealth -= 10f;

      //Check if enemy health is 0 or less
      if (enemyHealth <= 0)
      {
        hothScript hScript = GameObject.Find("LevelManager").GetComponent<hothScript>();
        hScript.atatsDestroyed += 1;
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
        hothScript hScript = GameObject.Find("LevelManager").GetComponent<hothScript>();
        hScript.atatsDestroyed += 1;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
      }

    }

    //unit reached destination point
    if (col.tag == "AiCheckpoint")
    {
      //stop walking
      canWalk = false;
      walkSound.Stop();
      target = ShieldGenerator;
    }
  }
}
