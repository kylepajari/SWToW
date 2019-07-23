using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

    public GameObject explosion;
    public bool XcanSpawn;
    public bool YcanSpawn;
    public bool AcanSpawn;
    public GameObject xwing;
    public GameObject awing;
    public GameObject ywing;
    public Transform spot1;
    public Transform spot2;
    public Transform spot3;
	public Transform spot4;
	public Transform spot5;
	public Transform spot6;
	public Transform spot7;
	public Transform spot8;
	public Transform spot9;
	public Transform spot10;

    public float XallyCount;
    public float YallyCount;
    public float AallyCount;

    void Start()
    {
        spot1 = transform.Find("spot1");
        spot2 = transform.Find("spot2");
        spot3 = transform.Find("spot3");
        spot4 = transform.Find("spot4");
        spot5 = transform.Find("spot5");
        spot6 = transform.Find("spot6");
        spot7 = transform.Find("spot7");
        spot8 = transform.Find("spot8");
        spot9 = transform.Find("spot9");
        spot10 = transform.Find("spot10");

        XcanSpawn = false;
        YcanSpawn = false;
        AcanSpawn = false;
        XallyCount = 0;
        YallyCount = 0;
        AallyCount = 0;
        Instantiate(awing, spot1.position, spot1.rotation);
        Instantiate(awing, spot2.position, spot2.rotation);
        Instantiate(xwing, spot3.position, spot3.rotation);
        Instantiate(xwing, spot4.position, spot4.rotation);
        Instantiate(xwing, spot5.position, spot5.rotation);
        Instantiate(awing, spot6.position, spot6.rotation);
        Instantiate(xwing, spot7.position, spot7.rotation);
        Instantiate(awing, spot8.position, spot8.rotation);
        Instantiate(xwing, spot9.position, spot9.rotation);
        Instantiate(ywing, spot10.position, spot10.rotation);

    }


    void Update()
    {

        if (XallyCount >= 1 && XcanSpawn)
        {
            Instantiate(xwing, spot1.position, spot1.rotation);
            XcanSpawn = false;
        }
        if (YallyCount >= 1 && YcanSpawn)
        {
            Instantiate(ywing, spot2.position, spot2.rotation);
            YcanSpawn = false;
        }
        if (AallyCount >= 1 && AcanSpawn)
        {
            Instantiate(awing, spot3.position, spot3.rotation);
            AcanSpawn = false;
        }
        if (XallyCount >= 1 && AcanSpawn)
        {
            Instantiate(xwing, spot4.position, spot4.rotation);
            XcanSpawn = false;
        }
        if (XallyCount >= 1 && AcanSpawn)
        {
            Instantiate(xwing, spot5.position, spot5.rotation);
            XcanSpawn = false;
        }
        if (YallyCount >= 1 && AcanSpawn)
        {
            Instantiate(ywing, spot6.position, spot6.rotation);
            YcanSpawn = false;
        }
        if (AallyCount >= 1 && AcanSpawn)
        {
            Instantiate(awing, spot7.position, spot7.rotation);
            AcanSpawn = false;
        }
        if (AallyCount >= 1 && AcanSpawn)
        {
            Instantiate(awing, spot8.position, spot8.rotation);
            AcanSpawn = false;
        }
        if (XallyCount >= 1 && AcanSpawn)
        {
            Instantiate(xwing, spot9.position, spot9.rotation);
            XcanSpawn = false;
        }
        if (XallyCount >= 1 && AcanSpawn)
        {
            Instantiate(xwing, spot10.position, spot10.rotation);
            XcanSpawn = false;
        }


    }
}
