using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bespinScript : MonoBehaviour {

    public float canCount;
    public GameObject levelCanvas;
    public float platsDestroyed;
    public bool havenotRun;
    public Text lvlText;

    public Transform corvette;

    public Transform stopPoint1;
    public Transform stopPoint2;

    public float corvetteSpeed;
    public bool canMove;
    public bool plat1Cleared;
    public bool plat2Cleared;

    public bool isFrozen;

	// Use this for initialization
	void Start () {
        canCount = 0;
        levelCanvas = GameObject.Find("LevelManager");
        levelCanvas.GetComponent<CanvasGroup>().alpha = 0;
        lvlText = levelCanvas.GetComponent<Text>();
        platsDestroyed = 0;
        havenotRun = true;
        corvette = GameObject.Find("corvette").transform;
        corvetteSpeed = 30;
        canMove = false;
        isFrozen = false;
        plat1Cleared = false;
        plat2Cleared = false;
        stopPoint1 = GameObject.Find("stopPoint1").transform;
        stopPoint2 = GameObject.Find("stopPoint2").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (canCount == 23)
        {
            if (havenotRun)
            {
                havenotRun = false;
                lvlText.text = "Great job!  Two more platforms remaining!";
                StartCoroutine(goodjob());
                canMove = true;
                plat1Cleared = true;
                
            }

        }
        if(canCount == 46)
        {
            if (!havenotRun)
            {
                havenotRun = true;
                lvlText.text = "One more platform remaining!";
                StartCoroutine(goodjob());
                canMove = true;
                plat2Cleared = true;
                
            }
        }
        if (canCount == 69)
        {
            if (havenotRun)
            {
                havenotRun = false;
                lvlText.text = "";
                StartCoroutine(goodjob());
            }
        }


        if(canMove)
        {
            if(isFrozen)
            {
                UnFreezeCorvette();
            }
            corvette.GetComponent<Rigidbody>().velocity = corvette.forward * corvetteSpeed;
            corvette.GetComponent<Rigidbody>().freezeRotation = true;
            if(plat1Cleared)
            {
                corvette.rotation = Quaternion.RotateTowards(corvette.rotation, Quaternion.LookRotation(stopPoint1.position - corvette.position), 5.0f * Time.deltaTime);
                if(corvette.position.x >= stopPoint1.position.x && corvette.position.z >= stopPoint1.position.z)
                {
                    canMove = false;
                    plat1Cleared = false;
                }

            }
            if (plat2Cleared)
            {
                corvette.rotation = Quaternion.RotateTowards(corvette.rotation, Quaternion.LookRotation(stopPoint2.position - corvette.position), 5.0f * Time.deltaTime);
                if(corvette.position.x >= stopPoint2.position.x && corvette.position.z >= stopPoint2.position.z)
                {
                    canMove = false;
                    plat2Cleared = false;
                } 

            }
        }
        else
        {
            if(!isFrozen)
            {
                FreezeCorvette();
            }
            
        }

    }

    public void FreezeCorvette()
    {
        corvette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        corvette.GetComponent<Rigidbody>().velocity = Vector3.zero;
        isFrozen = true;

    }

    public void UnFreezeCorvette()
    {
        corvette.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        isFrozen = false;

    }

    public IEnumerator goodjob()
    {

        platsDestroyed++;
        if (platsDestroyed == 3)
        {
            Application.LoadLevel("level4Outro");
        }
        levelCanvas.GetComponent<CanvasGroup>().alpha = 1;
        yield return new WaitForSeconds(3f);
        levelCanvas.GetComponent<CanvasGroup>().alpha = 0;
        yield return new WaitForSeconds(2f);


    }

}
