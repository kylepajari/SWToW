using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VeersShuttleScript : MonoBehaviour
{

  public float enemyHealth;
  public float speed;

  public GameObject explosion;
  public Transform target;
  public float Enemydistance;
  public float angle;

  private Transform ship;
  public AudioSource[] sounds;

  public float rotateSpeed;

  public Vector3 randomDirection;

  void Start()
  {
    ship = transform;
    target = GameObject.Find("CutPoint").transform;

  }

  void Update()
  {

    ship.GetComponent<Rigidbody>().freezeRotation = true;

    Enemydistance = Vector3.Distance(target.transform.position, ship.position);
    //Finds the current difference in distance between player and enemy 
    Vector3 playerDir = target.position - transform.position;
    //Calculate angle between forward vector of player and enemy
    angle = Vector3.Angle(transform.forward, playerDir);
    rotateSpeed = 0.6f;


    // Look at and fly towards target point
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotateSpeed * Time.deltaTime);
    //transform.position += transform.forward * speed * Time.deltaTime;

    if (Enemydistance <= 100)
    {
      Application.LoadLevel("level2BadOutro");
    }
  }

  void FixedUpdate()
  {
    transform.GetComponent<Rigidbody>().velocity = transform.forward * speed;
  }


  void CheckDead()
  {
    Application.LoadLevel("level2Outro");
  }


  void OnTriggerEnter(Collider col)
  {
    //Check to see if ship collided with redlaser
    if (col.name == "redlaser(Clone)" || col.name == "greenlaser(Clone)")
    {
      //Destroy red laser
      Destroy(col.gameObject);
      //Set enemy health back 10 points
      enemyHealth -= 10f;
      if (enemyHealth <= 0)
      {
        CheckDead();
      }
    }

    //Check to see if ship collided with ion laser
    if (col.name == "ionlaser(Clone)")
    {
      //Destroy ion laser
      Destroy(col.gameObject);
      //Slow enemy speed down 1 points
      speed -= 1f;
      if (speed <= 0)
      {
        speed = 10;
      }
    }

    //Check to see if ship collided with torpedo
    if (col.name == "protonTorpedo(Clone)")
    {
      Instantiate(explosion, transform.position, transform.rotation);
      //Destroy torpedo
      Destroy(col.gameObject);
      //Set enemy health back 200 points
      enemyHealth = enemyHealth - 200f;
      if (enemyHealth <= 0)
      {
        CheckDead();
      }

    }
  }
}
