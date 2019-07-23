using UnityEngine;
using System.Collections;

public class buildingScript : MonoBehaviour {

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
    }
}
