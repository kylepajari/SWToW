using UnityEngine;
using System.Collections;

public class StarDestoyerScript : MonoBehaviour {


    public GameObject explosion;
    public bool TcanSpawn;
    public bool TIcanSpawn;
    public bool TBcanSpawn;
    public GameObject Tie;
    public GameObject TieInt;
    public GameObject TieBomb;
    public Transform Hangar;
    public Transform Hangar2;
    public Transform Hangar3;
	public Transform Hangar4;
	public Transform Hangar5;
	public Transform Hangar6;
	public Transform Hangar7;
	public Transform Hangar8;
	public Transform Hangar9;
	public Transform Hangar10;


    public float TCount;
    public float TICount;
    public float TBCount;

    void Start()
    {
        Hangar = transform.Find("Hangar");
        Hangar2 = transform.Find("Hangar2");
        Hangar3 = transform.Find("Hangar3");
        Hangar4 = transform.Find("Hangar4");
        Hangar5 = transform.Find("Hangar5");
        Hangar6 = transform.Find("Hangar6");
        Hangar7 = transform.Find("Hangar7");
        Hangar8 = transform.Find("Hangar8");
        Hangar9 = transform.Find("Hangar9");
        Hangar10 = transform.Find("Hangar10");
        Instantiate(Tie, Hangar.position, Hangar.rotation);
        Instantiate(TieInt, Hangar2.position, Hangar2.rotation);
        Instantiate(TieBomb, Hangar3.position, Hangar3.rotation);
        Instantiate(Tie, Hangar4.position, Hangar4.rotation);
        Instantiate(Tie, Hangar5.position, Hangar5.rotation);
        Instantiate(TieInt, Hangar6.position, Hangar6.rotation);
        Instantiate(Tie, Hangar7.position, Hangar7.rotation);
        Instantiate(TieInt, Hangar8.position, Hangar8.rotation);
        Instantiate(Tie, Hangar9.position, Hangar9.rotation);
        Instantiate(Tie, Hangar10.position, Hangar10.rotation);


        TcanSpawn = false;
        TIcanSpawn = false;
        TBcanSpawn = false;
        TCount = 0;
        TICount = 0;
        TBCount = 0;
    }


    void Update()
    {

        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar.position, Hangar.rotation);
            TcanSpawn = false;
        }
        if (TICount >= 1 && TIcanSpawn)
        {
            Instantiate(TieInt, Hangar2.position, Hangar2.rotation);
            TIcanSpawn = false;
        }
        if (TBCount >= 1 && TBcanSpawn)
        {
            Instantiate(TieBomb, Hangar3.position, Hangar3.rotation);
            TBcanSpawn = false;
        }
        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar4.position, Hangar4.rotation);
            TcanSpawn = false;
        }
        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar5.position, Hangar5.rotation);
            TcanSpawn = false;
        }
        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar6.position, Hangar6.rotation);
            TcanSpawn = false;
            TCount = 0;
        }
        if (TICount >= 1 && TIcanSpawn)
        {
            Instantiate(TieInt, Hangar7.position, Hangar7.rotation);
            TIcanSpawn = false;
        }
        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar8.position, Hangar8.rotation);
            TcanSpawn = false;
            TCount = 0;
        }
        if (TICount >= 1 && TIcanSpawn)
        {
            Instantiate(TieInt, Hangar9.position, Hangar9.rotation);
            TIcanSpawn = false;
        }
        if (TCount >= 1 && TcanSpawn)
        {
            Instantiate(Tie, Hangar10.position, Hangar10.rotation);
            TcanSpawn = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        //Check to see if ship collided with redlaser
        if (col.name == "redlaser(Clone)")
        {
            Destroy(col.gameObject);
        }

        //Check to see if ship collided with greenlaser
        if (col.name == "greenlaser(Clone)")
        {
            Destroy(col.gameObject);

        }

        if (col.name == "protonTorpedo(Clone)")
        {
            Instantiate(explosion, col.transform.position, col.transform.rotation);
            Destroy(col.gameObject);
        }
    }
}
