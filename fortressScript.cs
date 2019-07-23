using UnityEngine;
using System.Collections;

public class fortressScript : MonoBehaviour {

    public float buildingHealth;
    public GameObject explosion;

    void OnTriggerEnter(Collider col)
    {
        //Check to see if ship collided with redlaser
        if (col.name == "redlaser(Clone)")
        {
            //Destroy red laser
            Destroy(col.gameObject);
            buildingHealth -= 10f;

            //Check if enemy health is 0 or less
            if (buildingHealth <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                if (GameObject.Find("XPlayer") != null)
                {
                    GameObject.Find("XPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("APlayer") != null)
                {
                    GameObject.Find("APlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("YPlayer") != null)
                {
                    GameObject.Find("YPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("FPlayer") != null)
                {
                    GameObject.Find("FPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
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
                if (GameObject.Find("XPlayer") != null)
                {
                    GameObject.Find("XPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("APlayer") != null)
                {
                    GameObject.Find("APlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("YPlayer") != null)
                {
                    GameObject.Find("YPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                if (GameObject.Find("FPlayer") != null)
                {
                    GameObject.Find("FPlayer").GetComponent<PlayerControls>().buildingsDestroyed++;
                }
                Destroy(gameObject);
            }

        }
    }


	
}
