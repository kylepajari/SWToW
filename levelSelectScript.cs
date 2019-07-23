using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelSelectScript : MonoBehaviour
{

  public Dropdown lst;
  public GameObject lvlname;
  public GameObject lvldesc;
  public GameObject leftbtn;
  public GameObject rightbtn;
  public bool currentlevel;
  public GameObject image1;
  public GameObject image2;
  public GameObject image3;
  public GameObject image4;
  public GameObject loadingCanvas;
  public GameObject levelcounterObject;

  public GameObject OnHover;

  // Use this for initialization
  void Start()
  {

    Time.timeScale = 1;
    levelcounterObject = GameObject.Find("levelCounterObject");
    levelcounterObject.GetComponent<levelcountScript>().levelcount = 1;
    image1 = GameObject.Find("Image");
    image2 = GameObject.Find("Image2");
    image3 = GameObject.Find("Image3");
    image4 = GameObject.Find("Image4");
    loadingCanvas = GameObject.Find("LoadingUI");
    loadingCanvas.GetComponent<CanvasGroup>().alpha = 0;
    image1.SetActive(true);
    image2.SetActive(false);
    image3.SetActive(false);
    currentlevel = false;
    lvlname = GameObject.Find("levelName");
    lvldesc = GameObject.Find("levelDesc");
    leftbtn = GameObject.Find("LeftButton");
    rightbtn = GameObject.Find("RightButton");
    if (leftbtn != null)
    {
      leftbtn.SetActive(false);
    }
    OnHover = GameObject.Find("OnHoverObject");

  }

  // Update is called once per frame
  void Update()
  {

    if (!currentlevel)
    {
      if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 1)
      {
        image1.SetActive(true);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        lvlname.GetComponent<Text>().text = "'It's a Trap!'";
        lvldesc.GetComponent<Text>().text = "Our fleet has been ambushed by an Imperial Star Destroyer while on route to Yavin IV.  You must protect us while we make the jump to hyperspace.  Our shields won't last against those fighters!";
        rightbtn.SetActive(true);
        leftbtn.SetActive(false);
        currentlevel = true;
      }
      if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 2)
      {
        image1.SetActive(false);
        image2.SetActive(true);
        image3.SetActive(false);
        image4.SetActive(false);
        lvlname.GetComponent<Text>().text = "'Opportunity is at Hand'";
        lvldesc.GetComponent<Text>().text = "We've gathered information that a high ranking Imperial General will be conducting a training mission on Rhen Var.  We need you to intercept his shuttle and force him to surrender!";
        leftbtn.SetActive(true);
        rightbtn.SetActive(true);
        currentlevel = true;
      }
      if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 3)
      {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(true);
        image4.SetActive(false);
        lvlname.GetComponent<Text>().text = "'Early Morning Run'";
        lvldesc.GetComponent<Text>().text = "Our intel suggests that the Empire is storing excess fuel reserves in the Bespin system.   Locate the three floating platforms and destroy the fuel canisters!       *A-Wing Only*";
        leftbtn.SetActive(true);
        rightbtn.SetActive(true);
        currentlevel = true;
      }
      if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 4)
      {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(true);
        lvlname.GetComponent<Text>().text = "'Assault on Endor'";
        lvldesc.GetComponent<Text>().text = "We have discovered the location of an Imperial fortress on Endor.   The Fortress will be heavily guarded, but we can't spare any fighters to assist you.  If you succeed, the loss of this fortress will be a major blow to the Empire! ";
        leftbtn.SetActive(true);
        rightbtn.SetActive(false);
        currentlevel = true;
      }
    }

    if (Input.GetKeyDown(KeyCode.LeftArrow) && levelcounterObject.GetComponent<levelcountScript>().levelcount != 1)
    {
      PrevLevel();
    }
    if (Input.GetKeyDown(KeyCode.RightArrow) && levelcounterObject.GetComponent<levelcountScript>().levelcount != 4)
    {
      NextLevel();
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      MainMenu();
    }

    if (Input.GetKeyDown(KeyCode.Return))
    {
      OnHover.GetComponent<OnHoverScript>().buttonConfirm.Play();
      StartCoroutine("PlayLevel");
    }

  }

  public void VehicleSelect()
  {
    StartCoroutine("PlayLevel");
  }

  IEnumerator PlayLevel()
  {
    yield return new WaitForSecondsRealtime(2);
    loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
    if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 1 || levelcounterObject.GetComponent<levelcountScript>().levelcount == 3 || levelcounterObject.GetComponent<levelcountScript>().levelcount == 4)
    {
      Application.LoadLevel("vehicleselect");
    }
    else if (levelcounterObject.GetComponent<levelcountScript>().levelcount == 2)
    {
      Application.LoadLevel("level3Intro");
    }

  }

  public void LoadLevel()
  {
    Application.LoadLevel("levelselect");
  }

  public void OptionsMenu()
  {
    Application.LoadLevel("OptionsMenu");
  }

  public void MainMenu()
  {

    Application.LoadLevel("MainMenu");

  }

  public void NextLevel()
  {
    levelcounterObject.GetComponent<levelcountScript>().levelcount++;
    currentlevel = false;

  }
  public void PrevLevel()
  {
    levelcounterObject.GetComponent<levelcountScript>().levelcount--;
    currentlevel = false;
  }

}
