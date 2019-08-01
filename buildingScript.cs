using UnityEngine;
using System.Collections;

public class buildingScript : MonoBehaviour
{

  public float buildingHealth;
  public GameObject explosion;

  void OnTriggerEnter(Collider col)
  {
    //Check to see if ship collided with redlaser
    if (col.name == "redlaser(Clone)" || col.tag == "PlayersLaser")
    {
      //Destroy red laser
      Destroy(col.gameObject);
      buildingHealth -= 10f;

      //Check if enemy health is 0 or less
      if (buildingHealth <= 0)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
      }
    }

    //Check to see if ship collided with torpedo
    if (col.name == "protonTorpedo(Clone)")
    {
      Destroy(col.gameObject);
      buildingHealth -= 50f;

      //Check if enemy health is 0 or less
      if (buildingHealth <= 0)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
      }

    }

    //Check to see if ship collided with redlaser
    if (col.name == "atatlaser(Clone)")
    {
      //Destroy red laser
      Instantiate(explosion, col.gameObject.transform.position, col.gameObject.transform.rotation);
      Destroy(col.gameObject);
      buildingHealth -= 75f;


      //Check if enemy health is 0 or less
      if (buildingHealth <= 0)
      {
        if (transform.name == "ShieldGenerator")
        {
          hothScript hScript = GameObject.Find("LevelManager").GetComponent<hothScript>();
          hScript.shieldDown = true;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
      }
    }
  }
}
