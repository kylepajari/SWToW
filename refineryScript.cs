using UnityEngine;
using System.Collections;

public class refineryScript : MonoBehaviour {

    public float canisterHealth;
    public GameObject explosion;
    //public GameObject canister1;
    //public GameObject canister2;
    //public GameObject canister3;
    //public GameObject canister4;
    //public GameObject canister5;
    //public GameObject canister6;
    //public GameObject canister7;
    //public GameObject canister8;
    //public GameObject canister9;
    //public GameObject canister10;
    //public GameObject canister11;
    //public GameObject canister12;
    //public GameObject canister13;
    //public GameObject canister14;
    //public GameObject canister15;
    //public GameObject canister16;
    //public GameObject canister17;
    //public GameObject canister18;
    //public GameObject canister19;
    //public GameObject canister20;
    //public GameObject canister21;
    //public GameObject canister22;
    //public GameObject canister23;

    // Use this for initialization
    void Start () {

        canisterHealth = 50;
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "redlaser(Clone)")
        {
            
            canisterHealth -= 10;
            if (canisterHealth <= 0)
            {
                Instantiate(explosion, col.transform.position, col.transform.rotation);
                bespinScript bscp = GameObject.Find("LevelManager").GetComponent<bespinScript>();
                bscp.canCount++;
                Destroy(col.gameObject);
                Destroy(gameObject);
                
            }
        }
        if (col.name == "protonTorpedo(Clone)")
        {
            
            canisterHealth -= 50;
            if (canisterHealth <= 0)
            {
                Instantiate(explosion, col.transform.position, col.transform.rotation);
                bespinScript bscp = GameObject.Find("LevelManager").GetComponent<bespinScript>();
                bscp.canCount++;
                Destroy(col.gameObject);
                Destroy(gameObject);
                
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Rebel")
        {
            bespinScript bscp = GameObject.Find("LevelManager").GetComponent<bespinScript>();
            bscp.canCount++;
            Destroy(gameObject);
            
        }
    }
}
