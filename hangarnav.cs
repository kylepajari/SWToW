using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hangarnav : MonoBehaviour
{

  public GameObject xwing;
  public GameObject xwingcamera;
  public GameObject ywing;
  public GameObject ywingcamera;
  public GameObject awing;
  public GameObject awingcamera;
  public GameObject falcon;
  public GameObject falconcamera;
  public GameObject tie;
  public GameObject tiecamera;

  public bool onXwing;
  public bool onYwing;
  public bool onAwing;
  public bool onFalcon;
  public bool onTie;
  public bool tieAV;

  public bool cheat1;
  public bool cheat2;
  public bool cheatUsed;

  public AudioSource[] sounds;
  public AudioSource choose;
  public AudioSource xDesc;
  public AudioSource yDesc;
  public AudioSource aDesc;
  public AudioSource ready;
  public AudioSource cantChoose;
  public AudioSource fDesc;
  public AudioSource tDesc;

  public Text TitleText;
  public Text shiptext;
  public Text subtext;
  public Text shipdetails;
  public bool canControl;

  private float xtempValx;
  private float xtempValy;
  private float xtempValz;

  private float ytempValx;
  private float ytempValy;
  private float ytempValz;

  private float atempValx;
  private float atempValy;
  private float atempValz;

  private float ftempValx;
  private float ftempValy;
  private float ftempValz;

  private float ttempValx;
  private float ttempValy;
  private float ttempValz;

  private Vector3 xtempPos;
  private Vector3 ytempPos;
  private Vector3 atempPos;
  private Vector3 ftempPos;
  private Vector3 ttempPos;
  GameObject gm;
  public GameObject loadingCanvas;
  public Task t;
  public Task xwMove;
  public Task awMove;
  public Task ywMove;
  public Task falcMove;
  public Task tieMove;



  // Use this for initialization
  void Start()
  {

    gm = GameObject.Find("levelCounterObject");

    loadingCanvas = GameObject.Find("LoadingUI");
    loadingCanvas.GetComponent<CanvasGroup>().alpha = 0;

    onXwing = true;
    onYwing = false;
    onAwing = false;
    onTie = false;
    onFalcon = false;
    canControl = true;
    cheat1 = false;
    cheat2 = false;

    xwing = GameObject.Find("X-WING");
    ywing = GameObject.Find("Y-WING");
    awing = GameObject.Find("A-WING");
    falcon = GameObject.Find("FALCON");
    tie = GameObject.Find("TIE");

    ywingcamera = GameObject.Find("ywingCamera");
    xwingcamera = GameObject.Find("xwingCamera");
    awingcamera = GameObject.Find("awingCamera");
    falconcamera = GameObject.Find("falconCamera");
    tiecamera = GameObject.Find("tieCamera");

    xtempValx = xwing.transform.position.x;
    xtempValy = xwing.transform.position.y;
    xtempValz = xwing.transform.position.z;

    ytempValx = ywing.transform.position.x;
    ytempValy = ywing.transform.position.y;
    ytempValz = ywing.transform.position.z;

    atempValx = awing.transform.position.x;
    atempValy = awing.transform.position.y;
    atempValz = awing.transform.position.z;

    ftempValx = falcon.transform.position.x;
    ftempValy = falcon.transform.position.y;
    ftempValz = falcon.transform.position.z;

    sounds = GetComponents<AudioSource>();
    choose = sounds[0];
    xDesc = sounds[1];
    yDesc = sounds[2];
    aDesc = sounds[4];
    ready = sounds[5];
    cantChoose = sounds[6];
    fDesc = sounds[7];
    tDesc = sounds[8];

  }

  // Update is called once per frame
  void Update()
  {

    if (t == null)
    {
      xtempPos.x = xtempValx;
      xtempPos.y = xtempValy + 7 * Mathf.Sin(1 * Time.time);
      xtempPos.z = xtempValz;
      xwing.transform.position = xtempPos;

      ytempPos.x = ytempValx;
      ytempPos.y = ytempValy + 7 * Mathf.Sin(1 * Time.time);
      ytempPos.z = ytempValz;
      ywing.transform.position = ytempPos;

      atempPos.x = atempValx;
      atempPos.y = atempValy + 7 * Mathf.Sin(1 * Time.time);
      atempPos.z = atempValz;
      awing.transform.position = atempPos;

      ftempPos.x = ftempValx;
      ftempPos.y = ftempValy + 7 * Mathf.Sin(1 * Time.time);
      ftempPos.z = ftempValz;
      falcon.transform.position = ftempPos;

      if (tieAV)
      {
        ttempValx = tie.transform.position.x;
        ttempValy = tie.transform.position.y;
        ttempValz = tie.transform.position.z;
        ttempPos.x = ttempValx;
        ttempPos.y = ttempValy + 0.1f * Mathf.Sin(1 * Time.time);
        ttempPos.z = ttempValz;
        tie.transform.position = ttempPos;
      }
    }

    //If X-wing is currently selected
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (onXwing)
    {
      if (t != null && t.Running)
      {
        var targetRotation = Quaternion.LookRotation(xwing.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
      }
      if (canControl)
      {
        if ((xwMove != null && xwMove.Running) || (t != null && t.Running))
        {
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
        }
        else
        {
          TitleText.text = "SELECT   YOUR   CRAFT";
          shiptext.text = "X-WING";
          subtext.text = "PRESS SPACEBAR FOR CRAFT DESCRIPTION OR ESC TO CANCEL";
          shipdetails.text = "ARMOR: 150   SPEED: 20      SECONDARY WEAPON: PROTON TORPEDOES";
        }

      }

      if (Input.GetKeyDown(KeyCode.Return))
      {
        if (t != null && t.Running)
        {
          loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
          if (gm.GetComponent<levelcountScript>().levelcount == 1)
          {
            Application.LoadLevel("level2xwing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 2)
          {
            Application.LoadLevel("level3xwing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 4)
          {
            Application.LoadLevel("level5xwing");
          }
        }
      }

    }



    //If Y-wing is currently selected
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (onYwing)
    {
      if (t != null && t.Running)
      {
        var targetRotation = Quaternion.LookRotation(ywing.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
      }
      if (canControl)
      {
        if (ywMove != null && ywMove.Running || (t != null && t.Running))
        {
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
        }
        else
        {
          TitleText.text = "SELECT   YOUR   CRAFT";
          shiptext.text = "Y-WING";
          subtext.text = "PRESS SPACEBAR FOR CRAFT DESCRIPTION OR ESC TO CANCEL";
          shipdetails.text = "ARMOR: 200   SPEED: 15      SECONDARY WEAPON: PROTON TORPEDOES";
        }

      }

      if (Input.GetKeyDown(KeyCode.Return))
      {
        if (t != null && t.Running)
        {
          loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
          if (gm.GetComponent<levelcountScript>().levelcount == 1)
          {
            Application.LoadLevel("level2ywing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 2)
          {
            Application.LoadLevel("level3ywing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 4)
          {
            Application.LoadLevel("level5ywing");
          }
        }
      }

    }


    //If A-wing is currently selected
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (onAwing)
    {
      if (t != null && t.Running)
      {
        var targetRotation = Quaternion.LookRotation(awing.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
      }
      if (canControl)
      {
        if (awMove != null && awMove.Running || (t != null && t.Running))
        {
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
        }
        else
        {
          TitleText.text = "SELECT   YOUR   CRAFT";
          shiptext.text = "A-WING";
          subtext.text = "PRESS SPACEBAR FOR CRAFT DESCRIPTION OR ESC TO CANCEL";
          shipdetails.text = "ARMOR: 100   SPEED: 25      SECONDARY WEAPON: PROTON TORPEDOES";
        }

      }

      if (Input.GetKeyDown(KeyCode.Return))
      {
        if (t != null && t.Running)
        {
          loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
          if (gm.GetComponent<levelcountScript>().levelcount == 1)
          {
            Application.LoadLevel("level2awing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 2)
          {
            Application.LoadLevel("level3awing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 3)
          {
            Application.LoadLevel("level4awing");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 4)
          {
            Application.LoadLevel("level5awing");
          }
        }
      }

    }


    //If Millenium Falcon is currently selected
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (onFalcon)
    {
      if (t != null && t.Running)
      {
        var targetRotation = Quaternion.LookRotation(falcon.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
      }
      if (canControl)
      {
        if (falcMove != null && falcMove.Running || (t != null && t.Running))
        {
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
        }
        else
        {
          TitleText.text = "SELECT   YOUR   CRAFT";
          shiptext.text = "MILLENIUM FALCON";
          subtext.text = "PRESS SPACEBAR FOR CRAFT DESCRIPTION OR ESC TO CANCEL";
          shipdetails.text = "ARMOR: 500   SPEED: 25      SECONDARY WEAPON: PROTON TORPEDOES";
        }

      }

      if (Input.GetKeyDown(KeyCode.Return))
      {
        if (t != null && t.Running)
        {
          loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
          if (gm.GetComponent<levelcountScript>().levelcount == 1)
          {
            Application.LoadLevel("level2falcon");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 2)
          {
            Application.LoadLevel("level3falcon");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 4)
          {
            Application.LoadLevel("level5falcon");
          }
        }
      }

    }


    //If Tie Interceptor is currently selected
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (onTie)
    {
      if (t != null && t.Running)
      {
        var targetRotation = Quaternion.LookRotation(tie.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
      }
      if (canControl)
      {
        if (tieMove != null && tieMove.Running || (t != null && t.Running))
        {
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
        }
        else
        {
          TitleText.text = "SELECT   YOUR   CRAFT";
          shiptext.text = "TIE INTERCEPTOR";
          subtext.text = "PRESS SPACEBAR FOR CRAFT DESCRIPTION OR ESC TO CANCEL";
          shipdetails.text = "ARMOR: 100   SPEED: 25      SECONDARY WEAPON: NONE";
        }

      }

      if (Input.GetKeyDown(KeyCode.Return))
      {
        if (t != null && t.Running)
        {
          loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
          if (gm.GetComponent<levelcountScript>().levelcount == 1)
          {
            Application.LoadLevel("level2tie");
          }
          if (gm.GetComponent<levelcountScript>().levelcount == 2)
          {
            Application.LoadLevel("level3tie");
          }
        }
      }

    }

    //If user is allowed input(arrow keys, spacebar, enter, etc.)
    /////////////////////////////////////////////////////////////////////////////////////////////////
    if (canControl)
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {

        if (onXwing)
        {
          if (yDesc.isPlaying)
          {
            aDesc.Stop();
            yDesc.Stop();
            xDesc.Play();
          }
          if (!xDesc.isPlaying)
          {
            xDesc.Play();
          }
          else
          {
            xDesc.Stop();
          }
        }
        if (onYwing)
        {
          if (xDesc.isPlaying)
          {
            aDesc.Stop();
            xDesc.Stop();
            yDesc.Play();
          }
          if (!yDesc.isPlaying)
          {
            yDesc.Play();
          }
          else
          {
            yDesc.Stop();
          }

        }
        if (onAwing)
        {
          if (xDesc.isPlaying)
          {
            yDesc.Stop();
            xDesc.Stop();
            aDesc.Play();
          }
          if (yDesc.isPlaying)
          {
            yDesc.Stop();
            xDesc.Stop();
            aDesc.Play();
          }
          if (!aDesc.isPlaying)
          {
            aDesc.Play();
          }
          else
          {
            aDesc.Stop();
          }

        }

        if (onFalcon)
        {
          if (xDesc.isPlaying)
          {
            yDesc.Stop();
            xDesc.Stop();
            aDesc.Stop();
            fDesc.Play();
          }
          if (yDesc.isPlaying)
          {
            aDesc.Stop();
            yDesc.Stop();
            xDesc.Stop();
            fDesc.Play();
          }
          if (!fDesc.isPlaying)
          {
            fDesc.Play();
          }
          else
          {
            fDesc.Stop();
          }

        }

        if (onTie)
        {
          if (xDesc.isPlaying)
          {
            yDesc.Stop();
            xDesc.Stop();
            aDesc.Stop();
            fDesc.Stop();
            tDesc.Play();
          }
          if (yDesc.isPlaying)
          {
            aDesc.Stop();
            yDesc.Stop();
            xDesc.Stop();
            fDesc.Stop();
            tDesc.Play();
          }
          if (!tDesc.isPlaying)
          {
            tDesc.Play();
          }
          else
          {
            tDesc.Stop();
          }

        }

      }

      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
        StopAllCoroutines();
        if (awMove != null && awMove.Running)
        {
          awMove.Stop();
        }
        if (ywMove != null && ywMove.Running)
        {
          ywMove.Stop();
        }
        if (tieMove != null && tieMove.Running)
        {
          tieMove.Stop();
        }
        if (falcMove != null && falcMove.Running)
        {
          falcMove.Stop();
        }
        if (xwMove != null && xwMove.Running)
        {
          xwMove.Stop();
        }

        cheat1 = false;
        cheat2 = false;
        if (onXwing)
        {
          awMove = new Task(MoveOverTime(transform, awingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, awingcamera.transform.rotation, 3f));
          onXwing = false;
          onYwing = false;
          onTie = false;
          onAwing = true;
          onFalcon = false;
          if (xDesc.isPlaying)
          {
            xDesc.Stop();
          }

        }
        else if (onAwing)
        {
          ywMove = new Task(MoveOverTime(transform, ywingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, ywingcamera.transform.rotation, 3f));
          onAwing = false;
          onXwing = false;
          onTie = false;
          onYwing = true;
          onFalcon = false;
          if (aDesc.isPlaying)
          {
            aDesc.Stop();
          }
        }
        else if (onYwing)
        {
          if (tieAV == true)
          {
            tieMove = new Task(MoveOverTime(transform, tiecamera.transform.position - transform.position, 3f));
            StartCoroutine(RotateOverTime(transform, tiecamera.transform.rotation, 3f));
            onXwing = false;
            onYwing = false;
            onAwing = false;
            onTie = true;
            onFalcon = false;
          }
          else
          {
            falcMove = new Task(MoveOverTime(transform, falconcamera.transform.position - transform.position, 3f));
            StartCoroutine(RotateOverTime(transform, falconcamera.transform.rotation, 3f));
            onYwing = false;
            onAwing = false;
            onXwing = false;
            onTie = false;
            onFalcon = true;
          }
          if (yDesc.isPlaying)
          {
            yDesc.Stop();
          }
        }
        else if (onTie)
        {
          falcMove = new Task(MoveOverTime(transform, falconcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, falconcamera.transform.rotation, 3f));
          onXwing = false;
          onTie = false;
          onYwing = false;
          onAwing = false;
          onFalcon = true;
          if (tDesc.isPlaying)
          {
            tDesc.Stop();
          }
        }
        else if (onFalcon)
        {
          xwMove = new Task(MoveOverTime(transform, xwingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, xwingcamera.transform.rotation, 3f));
          onFalcon = false;
          onYwing = false;
          onAwing = false;
          onTie = false;
          onXwing = true;
          if (fDesc.isPlaying)
          {
            fDesc.Stop();
          }
        }

      }
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
        StopAllCoroutines();
        if (awMove != null && awMove.Running)
        {
          awMove.Stop();
        }
        if (ywMove != null && ywMove.Running)
        {
          ywMove.Stop();
        }
        if (tieMove != null && tieMove.Running)
        {
          tieMove.Stop();
        }
        if (falcMove != null && falcMove.Running)
        {
          falcMove.Stop();
        }
        if (xwMove != null && xwMove.Running)
        {
          xwMove.Stop();
        }

        cheat1 = false;
        cheat2 = false;
        if (onXwing)
        {
          falcMove = new Task(MoveOverTime(transform, falconcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, falconcamera.transform.rotation, 3f));
          onAwing = false;
          onXwing = false;
          onYwing = false;
          onTie = false;
          onFalcon = true;
          if (xDesc.isPlaying)
          {
            xDesc.Stop();
          }

        }
        else if (onFalcon)
        {
          if (tieAV == true)
          {
            tieMove = new Task(MoveOverTime(transform, tiecamera.transform.position - transform.position, 3f));
            StartCoroutine(RotateOverTime(transform, tiecamera.transform.rotation, 3f));
            onXwing = false;
            onYwing = false;
            onAwing = false;
            onTie = true;
            onFalcon = false;
          }
          else
          {
            ywMove = new Task(MoveOverTime(transform, ywingcamera.transform.position - transform.position, 3f));
            StartCoroutine(RotateOverTime(transform, ywingcamera.transform.rotation, 3f));
            onFalcon = false;
            onAwing = false;
            onXwing = false;
            onTie = false;
            onYwing = true;
          }
          if (fDesc.isPlaying)
          {
            fDesc.Stop();
          }

        }
        else if (onYwing)
        {
          awMove = new Task(MoveOverTime(transform, awingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, awingcamera.transform.rotation, 3f));
          onFalcon = false;
          onXwing = false;
          onYwing = false;
          onTie = false;
          onAwing = true;
          if (yDesc.isPlaying)
          {
            yDesc.Stop();
          }
        }
        else if (onAwing)
        {
          xwMove = new Task(MoveOverTime(transform, xwingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, xwingcamera.transform.rotation, 3f));
          onFalcon = false;
          onAwing = false;
          onYwing = false;
          onXwing = true;

          if (aDesc.isPlaying)
          {
            aDesc.Stop();
          }
        }
        else if (onTie)
        {
          ywMove = new Task(MoveOverTime(transform, ywingcamera.transform.position - transform.position, 3f));
          StartCoroutine(RotateOverTime(transform, ywingcamera.transform.rotation, 3f));
          onTie = false;
          onYwing = true;
          onAwing = false;
          onXwing = false;
          onFalcon = false;
          if (tDesc.isPlaying)
          {
            tDesc.Stop();
          }
        }
      }

      if (Input.GetKeyDown(KeyCode.DownArrow))
      {
        cheat1 = true;
      }
      if (Input.GetKeyDown(KeyCode.UpArrow))
      {
        cheat2 = true;
        if (!cheatUsed)
        {
          if (cheat1 == true && cheat2 == true)
          {
            tieMove = new Task(MoveOverTime(transform, tiecamera.transform.position - transform.position, 3f));
            StartCoroutine(RotateOverTime(transform, tiecamera.transform.rotation, 3f));
            TitleText.text = "";
            shiptext.text = "";
            subtext.text = "";
            shipdetails.text = "";
            onXwing = false;
            onAwing = false;
            onYwing = false;
            onFalcon = false;
            StartCoroutine(tcheatshipanim());
            cheatUsed = true;
          }

        }

        cheat1 = false;
        cheat2 = false;
      }

      if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
      {
        if (onXwing)
        {
          //If Bespin level is chosen, play Unavailable sound when selecting ship
          if (gm.GetComponent<levelcountScript>().levelcount == 3)
          {
            cantChoose.Play();
          }
          else
          {
            choose.Play();
            ready.Play();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            xDesc.Stop();
            TitleText.text = "";
            shiptext.text = "";
            subtext.text = "";
            shipdetails.text = "";
            t = new Task(xshipanim());
          }

        }
        else if (onAwing)
        {
          choose.Play();
          ready.Play();
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          aDesc.Stop();
          TitleText.text = "";
          shiptext.text = "";
          subtext.text = "";
          shipdetails.text = "";
          t = new Task(ashipanim());
        }
        else if (onYwing)
        {
          //If Bespin level is chosen, play Unavailable sound when selecting ship
          if (gm.GetComponent<levelcountScript>().levelcount == 3)
          {
            cantChoose.Play();
          }
          else
          {
            choose.Play();
            ready.Play();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            yDesc.Stop();
            TitleText.text = "";
            shiptext.text = "";
            subtext.text = "";
            shipdetails.text = "";
            t = new Task(yshipanim());
          }
        }
        else if (onFalcon)
        {
          //If Bespin level is chosen, play Unavailable sound when selecting ship
          if (gm.GetComponent<levelcountScript>().levelcount == 3)
          {
            cantChoose.Play();
          }
          else
          {
            choose.Play();
            ready.Play();
            fDesc.Stop();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            TitleText.text = "";
            shiptext.text = "";
            shipdetails.text = "";
            t = new Task(fshipanim());
          }

        }
        else if (onTie)
        {
          //If Bespin or Endor levels are chosen, play Unavailable sound when selecting ship
          if (gm.GetComponent<levelcountScript>().levelcount == 3 || gm.GetComponent<levelcountScript>().levelcount == 4)
          {
            cantChoose.Play();
          }
          else
          {
            choose.Play();
            ready.Play();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            xDesc.Stop();
            aDesc.Stop();
            yDesc.Stop();
            tDesc.Stop();
            TitleText.text = "";
            shiptext.text = "";
            subtext.text = "";
            shipdetails.text = "";
            t = new Task(tshipanim());
          }

        }
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
        Application.LoadLevel("levelselect");
      }
    }

  }

  static IEnumerator MoveOverTime(Transform theTransform, Vector3 d, float t)
  {
    float rate = 1 / t;
    float index = 0f;
    Vector3 startPosition = theTransform.position;
    Vector3 endPosition = startPosition + d;
    while (index < 1)
    {

      theTransform.position = Vector3.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.position = endPosition;
  }

  static IEnumerator RotateOverTime(Transform theTransform, Quaternion d, float t)
  {
    float rate = 1 / t;
    float index = 0f;
    Quaternion startPosition = theTransform.rotation;
    Quaternion endPosition = d;
    while (index < 1)
    {

      theTransform.rotation = Quaternion.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.rotation = endPosition;
  }

  //Animation for X-wing
  public IEnumerator xshipanim()
  {
    canControl = false;

    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(xwing.transform, (xwing.transform.up * 80), 4));
      yield return new WaitForSeconds(4);
      num = 4;
    }
    if (num == 4)
    {
      StartCoroutine(MoveOverTime(xwing.transform, (xwing.transform.forward * 800), 4));
      yield return new WaitForSeconds(4);
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(RotateOverTime(xwing.transform, Quaternion.Euler(0, -90, 0), 3));
      yield return new WaitForSeconds(3);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(xwing.transform, (xwing.transform.forward * 3800), 5f));
      yield return new WaitForSeconds(4.5f);
      num = 1;
    }
    if (num == 1)
    {
      loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
      if (gm.GetComponent<levelcountScript>().levelcount == 1)
      {
        Application.LoadLevel("level2xwing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 2)
      {
        Application.LoadLevel("level3xwing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 4)
      {
        Application.LoadLevel("level5xwing");
      }

    }
  }


  //Animation for Y-wing
  public IEnumerator yshipanim()
  {
    canControl = false;

    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(ywing.transform, (ywing.transform.up * 80), 4));
      yield return new WaitForSeconds(4);
      num = 4;
    }
    if (num == 4)
    {
      StartCoroutine(MoveOverTime(ywing.transform, (ywing.transform.forward * 800), 4));
      yield return new WaitForSeconds(4);
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(RotateOverTime(ywing.transform, Quaternion.Euler(0, -90, 0), 3));
      yield return new WaitForSeconds(3);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(ywing.transform, (ywing.transform.forward * 3800), 5f));
      yield return new WaitForSeconds(4.5f);
      num = 1;
    }
    if (num == 1)
    {
      loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
      if (gm.GetComponent<levelcountScript>().levelcount == 1)
      {
        Application.LoadLevel("level2ywing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 2)
      {
        Application.LoadLevel("level3ywing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 4)
      {
        Application.LoadLevel("level5ywing");
      }
    }
  }

  //Animation for A-wing
  public IEnumerator ashipanim()
  {
    canControl = false;

    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(awing.transform, (awing.transform.up * 80), 4));
      yield return new WaitForSeconds(4);
      num = 4;
    }
    if (num == 4)
    {
      StartCoroutine(MoveOverTime(awing.transform, (awing.transform.forward * 800), 4));
      yield return new WaitForSeconds(4);
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(RotateOverTime(awing.transform, Quaternion.Euler(0, -90, 0), 3));
      yield return new WaitForSeconds(3);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(awing.transform, (awing.transform.forward * 3800), 5f));
      yield return new WaitForSeconds(4.5f);
      num = 1;
    }
    if (num == 1)
    {
      loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
      if (gm.GetComponent<levelcountScript>().levelcount == 1)
      {
        Application.LoadLevel("level2awing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 2)
      {
        Application.LoadLevel("level3awing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 3)
      {
        Application.LoadLevel("level4awing");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 4)
      {
        Application.LoadLevel("level5awing");
      }
    }
  }

  //Animation for Millenium Falcon
  public IEnumerator fshipanim()
  {
    canControl = false;

    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(falcon.transform, (falcon.transform.up * 80), 4));
      yield return new WaitForSeconds(4);
      num = 4;
    }
    if (num == 4)
    {
      StartCoroutine(MoveOverTime(falcon.transform, (falcon.transform.forward * 1100), 4));
      yield return new WaitForSeconds(4);
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(RotateOverTime(falcon.transform, Quaternion.Euler(0, -90, 0), 3));
      yield return new WaitForSeconds(3);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(falcon.transform, (falcon.transform.forward * 3800), 5f));
      yield return new WaitForSeconds(4.5f);
      num = 1;
    }
    if (num == 1)
    {
      loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
      if (gm.GetComponent<levelcountScript>().levelcount == 1)
      {
        Application.LoadLevel("level2falcon");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 2)
      {
        Application.LoadLevel("level3falcon");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 4)
      {
        Application.LoadLevel("level5falcon");
      }
    }
  }

  //Animation for Tie Interceptor
  public IEnumerator tshipanim()
  {
    canControl = false;

    int num = 3;
    if (num == 3)
    {
      StartCoroutine(MoveOverTime(transform, ywingcamera.transform.position - transform.position, 2f));
      yield return new WaitForSeconds(0);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(tie.transform, (tie.transform.up * 80), 3));
      yield return new WaitForSeconds(3);
      num = 1;
    }
    if (num == 1)
    {
      StartCoroutine(MoveOverTime(tie.transform, (tie.transform.forward * 3800), 5));
      yield return new WaitForSeconds(5);
      num = 0;
    }
    if (num == 0)
    {
      loadingCanvas.GetComponent<CanvasGroup>().alpha = 1;
      if (gm.GetComponent<levelcountScript>().levelcount == 1)
      {
        Application.LoadLevel("level2tie");
      }
      if (gm.GetComponent<levelcountScript>().levelcount == 2)
      {
        Application.LoadLevel("level3tie");
      }
    }
  }




  //Animation for Cheat used to reveal Tie Interceptor
  public IEnumerator tcheatshipanim()
  {
    canControl = false;
    int num = 4;
    if (num == 4)
    {
      yield return new WaitForSeconds(3);
      StartCoroutine(MoveOverTime(tie.transform, (tie.transform.forward * 1250), 4));
      yield return new WaitForSeconds(4);
      StartCoroutine(MoveOverTime(tie.transform, (tie.transform.up * 40), 1));
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(RotateOverTime(tie.transform, Quaternion.Euler(0, -90, 0), 2));
      yield return new WaitForSeconds(2);
      num = 2;
    }
    if (num == 2)
    {
      StartCoroutine(MoveOverTime(tie.transform, (tie.transform.forward * 300), 1));
      yield return new WaitForSeconds(1);
      num = 1;
    }
    if (num == 1)
    {
      onTie = true;
      tieAV = true;
    }
    canControl = true;
  }
}
