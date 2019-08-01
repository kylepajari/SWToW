using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

  public float playerSpeed;

  public float playerSpeedOrig;
  public float playerHealth;

  public float playerHealthOrig;
  public float sensitivityRoll = 0F;
  public float sensitivityPitch;
  public float sensitivityYaw;
  public float nextFire = 0f;
  public Transform spawnpoint;
  public Transform enemy;
  public Transform cannon;
  public Transform cannon2;
  public Transform cannon3;
  public Transform cannon4;
  public Transform IonCannon;

  public GameObject laser;
  public GameObject ionlaser;

  public Transform xwing;
  public Transform body;
  public Transform lwing;
  public Transform rwing;
  public bool FoilsLocked;

  public Transform etrail;
  public Transform etrail2;
  public Transform etrail3;
  public Transform etrail4;

  public Slider healthSlider;
  public Image damageImage;
  public float flashSpeed = 5f;
  public Color flashcolor = new Color(1f, 0f, 0f, 0.1f);

  public bool damaged;
  public float lifeCount;
  public Text livesText;

  public GameObject livesUI;

  public float playerKills;
  public float playerDeaths;



  public AudioSource[] sounds;
  public AudioSource shoot;
  public AudioSource ion;
  public AudioSource rapid;

  public AudioSource death1;
  public AudioSource death2;

  public AudioSource randomDeathSound;


  public GameObject explosion;

  public float view;
  public GameObject playercam;
  public Vector3 playercamOrigPos;
  public Quaternion playercamOrigRot;

  public bool lookingback;

  public float xRotation;
  public float yRotation;

  // controling the order of the lasers and the delays
  public int fireOrder;
  public float fireDelay;
  public bool nextShot;

  public Vector3 targetNormal;

  //Floats to keep track of team scores for level1
  public float rebelScore;
  public float impScore;
  public float scoreLimit;
  /////////////////////////////////////////////////

  //Text fields to bind scoring to
  public Text rebelcurr;
  public Text rebellimit;
  public Text impcurr;
  public Text implimit;
  /////////////////////////////////////////////////


  public Text TurnBack;
  public bool isRunning;
  public Text TurnBackTimer;





  //Set up HUD Elements
  public GameObject HealthUI;
  public GameObject Cross;
  public GameObject AmmoUI;
  public GameObject ScoreUI;
  public GameObject hudCanvas;
  public GameObject objCanvas;

  public bool gameover;
  public bool gameoverrunning;
  public GameObject youwin;
  public Text winScreen;

  public Text ScoreText;

  public float buildingsDestroyed;
  public float buildingsToWin;
  public bool died;
  public Image Fill;




  public bool keyboardControls;
  public bool mouseControls;


  //The max variables are to make the rotation framerate independent.
  //You could alternatively do the work in FixedUpdate,
  //but the controls might be less responsive there.


  //Tilt
  public float maxTilt = 180f; //Degrees/second
  public float tiltScale = 80f; //Degrees/unitInput*second
  public float tiltRange = 360f; //Degrees
  public float rotX = 0f; //Degrees


  //Turn
  public float maxTurn = 360f; //Degrees/second
  public float turnScale = 80f; //Degrees/unitInput*second
  public float turnRange = 360f; //Degrees
  public float rotY = 0f; //Degrees


  //Bank
  public float maxBank = 90f; //Degrees/second
  public float bankScale = 60f; //Degrees/unitInput*second
  public float returnSpeed = 50f;//Degrees/second
  public float bankRange = 45f; //Degrees
  public float rotZ = 0f; //Degrees


  //Input
  public float mouseScale = 0.5f; //Gs of acceleration/pixel
  public float deltaX = 0f; //Units of input
  public float deltaY = 0f; //Units of input

  //Start information
  public Quaternion originalRot = Quaternion.identity;
  private Transform engineGlow1;

  private Transform engineGlow2;
  private Transform engineGlow3;
  private Transform engineGlow4;

  public bool respawnRunning;

  public Task task;

  public GameObject checkpoint1;
  public GameObject checkpoint2;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    sounds = GetComponents<AudioSource>();
    healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    Fill = GameObject.Find("Fill").GetComponent<Image>();
    TurnBack = GameObject.Find("TurnBack").GetComponent<Text>();
    TurnBackTimer = GameObject.Find("turnbacktimer").GetComponent<Text>();
    HealthUI = GameObject.Find("HealthUI");
    Cross = GameObject.Find("Crosshair");
    AmmoUI = GameObject.Find("AmmoUI");
    ScoreUI = GameObject.Find("ScoreUI");
    youwin = GameObject.Find("YouWin");
    winScreen = GameObject.Find("YouWin").GetComponent<Text>();
    ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    objCanvas = GameObject.Find("ObjectiveUI");
    Globals.Fade = GameObject.Find("FadeToBlack");
    damageImage = GameObject.Find("DamageImage").GetComponent<Image>();
    spawnpoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
    hudCanvas = GameObject.Find("HUDCanvas");
    youwin = GameObject.Find("YouWin");
    youwin.GetComponent<CanvasGroup>().alpha = 0;
    playercam = Camera.main.gameObject;
    if (transform.name == "XPlayer")
    {
      playercam.transform.localPosition = new Vector3(0, 0.51f, -2.0f);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon (1)");
      cannon3 = transform.Find("cannon (2)");
      cannon4 = transform.Find("cannon (3)");
      xwing = transform.Find("___DUMMY_xwing2__fr");
      body = xwing.transform.Find("___DUMMY_body4__2");
      lwing = body.Find("___DUMMY_wing3__2");
      rwing = body.Find("___DUMMY_wing4__2");
      etrail = transform.Find("etrail");
      etrail2 = transform.Find("etrail2");
      etrail3 = transform.Find("etrail3");
      etrail4 = transform.Find("etrail4");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      etrail3.GetComponent<Renderer>().enabled = false;
      etrail4.GetComponent<Renderer>().enabled = false;
      engineGlow1 = transform.Find("___DUMMY_xwing2__fr/___DUMMY_body4__2/___DUMMY_wing3__2/___DUMMY_wing3/engine1glow");
      engineGlow2 = transform.Find("___DUMMY_xwing2__fr/___DUMMY_body4__2/___DUMMY_wing4__2/___DUMMY_wing4/engine2glow");
      engineGlow3 = transform.Find("___DUMMY_xwing2__fr/___DUMMY_body4__2/___DUMMY_wing3__2/___DUMMY_wing2/engine3glow");
      engineGlow4 = transform.Find("___DUMMY_xwing2__fr/___DUMMY_body4__2/___DUMMY_wing4__2/___DUMMY_wing1/engine4glow");
      shoot = sounds[0];
      rapid = sounds[2];
      death1 = sounds[6];
      death2 = sounds[7];
      rapid.mute = true;
      FoilsLocked = false;
    }
    if (transform.name == "APlayer")
    {
      playercam.transform.localPosition = new Vector3(0, 120f, -450f);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon2");
      etrail = transform.Find("etrail1");
      etrail2 = transform.Find("etrail2");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      engineGlow1 = transform.Find("engine1glow");
      engineGlow2 = transform.Find("engine2glow");
      shoot = sounds[1];
      death1 = sounds[5];
      death2 = sounds[6];
    }
    if (transform.name == "YPlayer")
    {
      playercam.transform.localPosition = new Vector3(0, 700f, -2500f);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon2");
      IonCannon = transform.Find("protonCannon");
      etrail = transform.Find("etrail1");
      etrail2 = transform.Find("etrail2");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      engineGlow2 = transform.Find("engine2glow");
      engineGlow1 = transform.Find("engine1glow");
      shoot = sounds[1];
      ion = sounds[5];
      death1 = sounds[6];
      death2 = sounds[7];
    }
    if (transform.name == "FPlayer")
    {
      playercam.transform.localPosition = new Vector3(0, 8.2f, -26.65f);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon2");
      cannon3 = transform.Find("cannon3");
      cannon4 = transform.Find("cannon4");
      etrail = transform.Find("etrail1");
      etrail2 = transform.Find("etrail2");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      shoot = sounds[1];
      death1 = sounds[6];
      death2 = sounds[7];
    }
    if (transform.name == "TPlayer")
    {
      playercam.transform.localPosition = new Vector3(0f, 9f, -31f);
      cannon = transform.Find("cannon1");
      cannon2 = transform.Find("cannon2");
      cannon3 = transform.Find("cannon3");
      cannon4 = transform.Find("cannon4");
      engineGlow1 = transform.Find("ion1");
      shoot = sounds[0];
      death1 = sounds[2];
      death2 = sounds[3];
      fireDelay = 0.18f;
      AmmoUI.GetComponent<CanvasGroup>().alpha = 0;
    }
    if (transform.name == "SPlayer")
    {
      playercam.transform.localPosition = new Vector3(0, 260, -950);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon2");
      etrail = transform.Find("etrail1");
      etrail2 = transform.Find("etrail2");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      shoot = sounds[1];
      death1 = sounds[2];
      death2 = sounds[3];
    }
    if (transform.name == "NPlayer")
    {
      playercam.transform.localPosition = new Vector3(0f, 250f, -1000f);
      cannon = transform.Find("cannon");
      cannon2 = transform.Find("cannon2");
      etrail = transform.Find("etrail");
      etrail2 = transform.Find("etrail2");
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      shoot = sounds[1];
      death1 = sounds[2];
      death2 = sounds[3];
    }

    lifeCount = 3;
    livesText = Globals.FindObject(HealthUI, "LivesCounter").GetComponent<Text>();
    livesText.text = lifeCount.ToString();
    livesUI = Globals.FindObject(HealthUI, "LivesText");
    if (Application.loadedLevelName == "level1" && livesUI.active)
    {
      livesUI.SetActive(false);
    }
    if (Application.loadedLevelName == "level4")
    {
      checkpoint1 = GameObject.Find("Checkpoint1");
      checkpoint2 = GameObject.Find("Checkpoint2");
    }
    playercamOrigPos = playercam.transform.localPosition;
    playercamOrigRot = playercam.transform.localRotation;

    fireOrder = 1;
    nextShot = true;

    view = 0;


    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;

    rebelcurr = GameObject.Find("rebelcurr").GetComponent<Text>();
    impcurr = GameObject.Find("impcurr").GetComponent<Text>();
    rebellimit = GameObject.Find("rebellimit").GetComponent<Text>();
    implimit = GameObject.Find("implimit").GetComponent<Text>();
    rebellimit.text = scoreLimit.ToString();
    implimit.text = scoreLimit.ToString();
    rebelcurr.text = "0";
    impcurr.text = "0";

    AudioListener.pause = false;

    TurnBack.enabled = false;
    TurnBackTimer.enabled = false;

    isRunning = false;

    healthSlider.maxValue = playerHealth;
    healthSlider.value = playerHealth;

    buildingsDestroyed = 0;
    buildingsToWin = 1;

    if (GetComponent<Rigidbody>())
    {
      GetComponent<Rigidbody>().freezeRotation = true;
    }
    originalRot = transform.localRotation;
    mouseControls = true;
    keyboardControls = true;
    mouseScale = 0.5f;
    lookingback = false;
    playerSpeedOrig = playerSpeed;
    playerHealthOrig = playerHealth;

  }

  public static class CoroutineUtil
  {
    public static IEnumerator WaitForRealSeconds(float time)
    {
      float start = Time.realtimeSinceStartup;
      while (Time.realtimeSinceStartup < start + time)
      {
        yield return null;
      }
    }
  }


  public IEnumerator closeWings(float rotateSpeed)
  {
    float rate = 1.0F / rotateSpeed;
    float timeScale = 0.0F;

    while (timeScale < rate)
    {
      timeScale += Time.deltaTime;
      lwing.transform.localRotation = Quaternion.Slerp(lwing.transform.localRotation, Quaternion.Euler(0F, 0F, 0F), timeScale / rate);
      rwing.transform.localRotation = Quaternion.Slerp(rwing.transform.localRotation, Quaternion.Euler(0F, 0F, 0F), timeScale / rate);
      yield return new WaitForEndOfFrame();
    }
  }
  public IEnumerator openWings(float rotateSpeed)
  {
    float rate = 1.0F / rotateSpeed;
    float timeScale = 0.0F;

    while (timeScale < rate)
    {
      timeScale += Time.deltaTime;
      lwing.transform.localRotation = Quaternion.Slerp(lwing.transform.localRotation, Quaternion.Euler(0F, -14F, 0F), timeScale / rate);
      rwing.transform.localRotation = Quaternion.Slerp(rwing.transform.localRotation, Quaternion.Euler(0F, 14F, 0F), timeScale / rate);
      yield return new WaitForEndOfFrame();
    }
  }

  void UpdateHealthBar()
  {
    healthSlider.value = playerHealth;
    Fill.color = Color.Lerp(Color.red, Color.green, (float)playerHealth / healthSlider.maxValue);
  }


  void Update()
  {
    if (died)
    {
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, 0, 0), 1f * Time.deltaTime);
      transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
      mouseControls = false;
      keyboardControls = false;
    }
    else if (respawnRunning)
    {
      mouseControls = false;
      keyboardControls = false;
    }
    else
    {
      mouseControls = true;
      keyboardControls = true;
    }

    if (!died && !respawnRunning)
    {
      //Change View
      if (Input.GetKeyDown(KeyCode.V))
      {
        if (task != null && task.Running)
        {
          task.Stop();
        }
        CycleView();
      }

      //Look Behind
      if (Input.GetKey(KeyCode.C))
      {
        lookingback = true;
        if (task != null && task.Running)
        {
          task.Stop();
        }
        if (transform.name == "APlayer")
        {
          playercam.transform.localPosition = new Vector3(0, 150, 450);
        }
        if (transform.name == "XPlayer")
        {
          playercam.transform.localPosition = new Vector3(0, 0.51f, 2.0f);
        }
        if (transform.name == "YPlayer")
        {
          playercam.transform.localPosition = new Vector3(0, 500f, 2000f);
        }
        if (transform.name == "FPlayer")
        {
          playercam.transform.localPosition = new Vector3(0, 8.2f, 33f);
        }
        if (transform.name == "TPlayer")
        {
          playercam.transform.localPosition = new Vector3(0f, 9f, 36f);
        }
        if (transform.name == "SPlayer")
        {
          playercam.transform.localPosition = new Vector3(0f, 150f, 800f);
        }
        if (transform.name == "NPlayer")
        {
          playercam.transform.localPosition = new Vector3(0f, 215f, 980f);
        }
        playercam.transform.localRotation = Quaternion.Euler(0, 180, 0);
        //If Player presses speed up or slow down keys while looking back, adjust speed approprietly
        if (Input.GetKeyDown(KeyCode.W))
        {
          playerSpeed += 15f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
          playerSpeed -= 15f;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
          playerSpeed = playerSpeedOrig;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
          playerSpeed = playerSpeedOrig;
        }
      }
      //When Player releases look behind key, return to original view
      if (Input.GetKeyUp(KeyCode.C))
      {
        lookingback = false;
        playercam.transform.localPosition = playercamOrigPos;
        if (transform.name == "SPlayer" && view == 2)
        {
          playercam.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
          playercam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

      }

      //Special funstions(varys between ships)
      if (Input.GetKeyDown(KeyCode.F))
      {
        //X-wing: Open/Close S-Foils
        if (transform.name == "XPlayer")
        {
          if (!FoilsLocked)
          {
            FoilsLocked = true;
            StartCoroutine(closeWings(1f));
            etrail.GetComponent<Renderer>().enabled = true;
            etrail2.GetComponent<Renderer>().enabled = true;
            etrail3.GetComponent<Renderer>().enabled = true;
            etrail4.GetComponent<Renderer>().enabled = true;

          }
          else if (FoilsLocked)
          {
            FoilsLocked = false;
            StartCoroutine(openWings(1f));
            etrail.GetComponent<Renderer>().enabled = false;
            etrail2.GetComponent<Renderer>().enabled = false;
            etrail3.GetComponent<Renderer>().enabled = false;
            etrail4.GetComponent<Renderer>().enabled = false;

          }
        }
        //Y-wing: Fire Ion Cannon, slows enemy speed on contact
        else if (transform.name == "YPlayer")
        {
          GameObject ionbolt = Instantiate(ionlaser, IonCannon.transform.position, IonCannon.transform.rotation) as GameObject;
          ionbolt.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 25f), ForceMode.Impulse);
          ion.Play();
        }

      }

      //If not currently looking behind
      if (!lookingback)
      {
        //Make Ship Boost Speed
        if (Input.GetKeyDown(KeyCode.W))
        {
          if (task != null && task.Running)
          {
            task.Stop();
          }
          playerSpeed += 15f;
          if (transform.name == "XPlayer")
          {
            if (!FoilsLocked)
            {
              if (view != 2)
              {
                task = new Task(lerpPos(playercam.transform.localPosition,
                new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 1), 0.5f));
              }
            }
          }
          else if (transform.name == "APlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 120), 0.5f));
            }
          }
          else if (transform.name == "YPlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 600), 0.5f));
            }
          }
          else if (transform.name == "SPlayer")
          {
            if (view != 1 && view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 120), 0.5f));
            }
          }
          else if (transform.name == "NPlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 120), 0.5f));
            }
          }
          else
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z - 6), 0.5f));
            }
          }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
          playerSpeed -= 15f;
          if (task != null && task.Running)
          {
            task.Stop();
          }
          if (view != 2)
          {
            task = new Task(lerpPos(playercam.transform.localPosition,
            new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z), 0.5f));
          }
        }

        //Slow Ship Speed
        if (Input.GetKeyDown(KeyCode.S))
        {
          playerSpeed -= 15f;
          if (task != null && task.Running)
          {
            task.Stop();
          }
          if (transform.name == "XPlayer")
          {
            if (!FoilsLocked && view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 0.5f), 0.5f));
            }
          }
          else if (transform.name == "APlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 60), 0.5f));
            }
          }
          else if (transform.name == "YPlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 300), 0.5f));
            }
          }
          else if (transform.name == "SPlayer")
          {
            if (view != 1 && view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 80), 0.5f));
            }
          }
          else if (transform.name == "NPlayer")
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 80), 0.5f));
            }
          }
          else
          {
            if (view != 2)
            {
              task = new Task(lerpPos(playercam.transform.localPosition,
              new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z + 3), 0.5f));
            }
          }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
          playerSpeed += 15f;
          if (task != null && task.Running)
          {
            task.Stop();
          }
          if (view != 2)
          {
            task = new Task(lerpPos(playercam.transform.localPosition,
            new Vector3(playercamOrigPos.x, playercamOrigPos.y, playercamOrigPos.z), 0.5f));
          }
        }

      }


      //Make Ship Fire Lasers
      if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
      {
        if (transform.name == "XPlayer")
        {
          if (!FoilsLocked)
          {
            StopCoroutine(RapidFire());
            StartCoroutine(RapidFire());

            if (fireOrder == 1 && nextShot)
            {
              GameObject laser1 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
              laser1.tag = "PlayersLaser";
              laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
              shoot.Play();
              fireOrder += 1;
              nextShot = false;
              fireDelay = 0.12f;
            }

            if (fireOrder == 2 && nextShot)
            {
              GameObject laser2 = Instantiate(laser, cannon4.transform.position, cannon4.transform.rotation) as GameObject;
              laser2.tag = "PlayersLaser";
              laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
              shoot.Play();
              fireOrder += 1;
              nextShot = false;
              fireDelay = 0.12f;
            }

            if (fireOrder == 3 && nextShot)
            {
              GameObject laser3 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
              laser3.tag = "PlayersLaser";
              laser3.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
              shoot.Play();
              fireOrder += 1;
              nextShot = false;
              fireDelay = 0.12f;
            }

            if (fireOrder == 4 && nextShot)
            {
              GameObject laser4 = Instantiate(laser, cannon3.transform.position, cannon3.transform.rotation) as GameObject;
              laser4.tag = "PlayersLaser";
              laser4.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
              shoot.Play();
              fireOrder = 1;
              nextShot = false;
              fireDelay = 0.12f;
            }

          }
        }

        if (transform.name == "APlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.15f;
          }

          if (fireOrder == 2 && nextShot)
          {
            GameObject laser2 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.15f;
          }
        }

        if (transform.name == "YPlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.28f;
          }

          if (fireOrder == 2 && nextShot)
          {
            GameObject laser2 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.28f;
          }
        }

        if (transform.name == "FPlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.15f;
          }

          if (fireOrder == 2 && nextShot)
          {
            GameObject laser2 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.15f;
          }
          if (fireOrder == 3 && nextShot)
          {
            GameObject laser3 = Instantiate(laser, cannon3.transform.position, cannon3.transform.rotation) as GameObject;
            laser3.tag = "PlayersLaser";
            laser3.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.15f;
          }
          if (fireOrder == 4 && nextShot)
          {
            GameObject laser4 = Instantiate(laser, cannon4.transform.position, cannon4.transform.rotation) as GameObject;
            laser4.tag = "PlayersLaser";
            laser4.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.15f;
          }
        }

        if (transform.name == "TPlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 25f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.18f;
          }
          if (fireOrder == 2 && nextShot)
          {
            GameObject laser2 = Instantiate(laser, cannon3.transform.position, cannon3.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 25f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.18f;
          }
          if (fireOrder == 3 && nextShot)
          {
            GameObject laser3 = Instantiate(laser, cannon4.transform.position, cannon4.transform.rotation) as GameObject;
            laser3.tag = "PlayersLaser";
            laser3.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 25f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.18f;
          }
          if (fireOrder == 4 && nextShot)
          {
            GameObject laser4 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser4.tag = "PlayersLaser";
            laser4.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 25f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.18f;
          }
        }

        if (transform.name == "SPlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            GameObject laser2 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.30f;
          }
        }

        if (transform.name == "NPlayer")
        {
          if (fireOrder == 1 && nextShot)
          {
            GameObject laser1 = Instantiate(laser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
            laser1.tag = "PlayersLaser";
            laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder += 1;
            nextShot = false;
            fireDelay = 0.25f;
          }

          if (fireOrder == 2 && nextShot)
          {
            GameObject laser2 = Instantiate(laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
            laser2.tag = "PlayersLaser";
            laser2.GetComponent<Rigidbody>().AddForce(transform.forward * (playerSpeed * 10f), ForceMode.Impulse);
            shoot.Play();
            fireOrder = 1;
            nextShot = false;
            fireDelay = 0.25f;
          }
        }

      }
    }

    //Set time until next shot
    if (!nextShot)
    {
      fireDelay -= Time.deltaTime;
    }

    if (fireDelay <= 0)
    {
      nextShot = true;
    }

    //If Player damaged, flash screen red
    if (damaged)
    {
      damageImage.color = flashcolor;
      // Reset the damaged flag.
      damaged = false;
    }
    else
    {
      if (damageImage.color != Color.clear)
      {
        // ... transition the colour back to clear.
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
      }

    }




    //////////////////////////////////////////////////////////////////////////////////////
    //Check to see if current score matches score limit, if so victory!
    rebelcurr.text = rebelScore.ToString();
    impcurr.text = impScore.ToString();

    if (rebelScore >= scoreLimit)
    {
      if (!gameoverrunning)
      {
        AudioListener.pause = true;
        youwin.GetComponent<CanvasGroup>().alpha = 1;
        hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
        objCanvas.GetComponent<CanvasGroup>().alpha = 0;
        winScreen.text = "Victory";
        winScreen.color = Color.yellow;
        ScoreText.text = "Kills: " + playerKills + "    Deaths: " + playerDeaths + " ";
        ScoreText.color = Color.white;
        gameover = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Time.timeScale = 0;
        StartCoroutine(gameOver());
      }
    }

    if (impScore >= scoreLimit)
    {
      if (!gameoverrunning)
      {
        AudioListener.pause = true;
        youwin.GetComponent<CanvasGroup>().alpha = 1;
        hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
        objCanvas.GetComponent<CanvasGroup>().alpha = 0;
        winScreen.text = "Defeat";
        winScreen.color = Color.red;
        ScoreText.text = "Kills: " + playerKills + "    Deaths: " + playerDeaths + " ";
        ScoreText.color = Color.white;
        gameover = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Time.timeScale = 0;
        StartCoroutine(gameOver());
      }
    }

    //If not on level1(First mission)
    if (Application.loadedLevelName != "level1")
    {
      ///////////////////Check if players Lives reach 0, if so GameOver/////////////////////////////
      if (lifeCount <= -1)
      {
        if (!gameoverrunning)
        {
          youwin.GetComponent<CanvasGroup>().alpha = 1;
          hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
          objCanvas.GetComponent<CanvasGroup>().alpha = 0;
          ScoreUI.GetComponent<CanvasGroup>().alpha = 0;
          winScreen.text = "Game Over";
          winScreen.color = Color.red;
          ScoreText.text = "Kills: " + playerKills + "    Deaths: " + playerDeaths + " ";
          ScoreText.color = Color.white;
          gameover = true;
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          // Time.timeScale = 0;
          StartCoroutine(gameOver());
        }
      }
    }

    //If on level4(Fourth mission)
    if (Application.loadedLevelName == "level4")
    {
      if (buildingsDestroyed >= buildingsToWin)
      {
        if (!gameoverrunning)
        {
          AudioListener.pause = true;
          youwin.GetComponent<CanvasGroup>().alpha = 1;
          hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
          objCanvas.GetComponent<CanvasGroup>().alpha = 0;
          winScreen.text = "Mission Complete";
          winScreen.color = Color.yellow;
          ScoreText.text = "Kills: " + playerKills + "    Deaths: " + playerDeaths + " ";
          ScoreText.color = Color.white;
          gameover = true;
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          // Time.timeScale = 0;
          StartCoroutine(gameOver());
        }
      }
    }

  }

  static float ClampAngle(float angle, float min, float max)
  {
    while (angle < -360)
    {
      angle += 360;
    }
    while (angle > 360)
    {
      angle -= 360;
    }
    return Mathf.Clamp(angle, min, max);
  }

  void FixedUpdate()
  {
    if (keyboardControls && Input.GetAxis("Mouse X") == 0)
    {
      deltaX = Input.GetAxis("Horizontal");
      deltaY = Input.GetAxis("Vertical");
    }
    else if (mouseControls && Input.GetAxis("Mouse X") != 0)
    {
      deltaX = Input.GetAxis("Mouse X") * mouseScale;
      deltaY = Input.GetAxis("Mouse Y") * mouseScale;
    }
    else
    {
      deltaX = Input.acceleration.x;
      deltaY = Input.acceleration.y;
    }

    //Bank
    if (!Mathf.Approximately(deltaX, 0.0f))
    {
      rotZ = ClampAngle(rotZ - ClampAngle(deltaX * bankScale, -maxBank, maxBank) * Time.deltaTime, -bankRange, bankRange);
    }
    else if (rotZ > 0.0f)
    {
      rotZ = ClampAngle(rotZ - Time.deltaTime * returnSpeed, 0.0f, bankRange);
    }
    else
    {
      rotZ = ClampAngle(rotZ + Time.deltaTime * returnSpeed, -bankRange, 0.0f);
    }


    //Turn
    rotY = ClampAngle(rotY + ClampAngle(deltaX * turnScale, -maxTurn, maxTurn) * Time.deltaTime, -turnRange, turnRange);
    //Tilt
    if (Globals.pitchInverted == 0)
    {
      rotX = ClampAngle(rotX - ClampAngle(deltaY * tiltScale, -maxTilt, maxTilt) * Time.deltaTime, -tiltRange, tiltRange);
    }
    else
    {
      rotX = ClampAngle(rotX - ClampAngle(-deltaY * tiltScale, -maxTilt, maxTilt) * Time.deltaTime, -tiltRange, tiltRange);
    }


    transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ) * originalRot;

    if (!died)
    {
      transform.GetComponent<Rigidbody>().velocity = transform.forward * playerSpeed;
    }


    if (transform.name == "XPlayer")
    {
      if (FoilsLocked)
      {
        transform.GetComponent<Rigidbody>().velocity = transform.forward * (playerSpeed + 40f);
      }
    }

  }


  IEnumerator lerpPos(Vector3 startPos, Vector3 endPos, float lerpTime)
  {
    float startTime = Time.time;
    float endTime = startTime + lerpTime;

    while (Time.time < endTime)
    {
      float timeProgressed = (Time.time - startTime) / lerpTime;
      playercam.transform.localPosition = Vector3.Slerp(startPos, endPos, timeProgressed);

      yield return new WaitForFixedUpdate();
    }

  }

  IEnumerator RapidFire()
  {
    while (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
    {
      if (shoot.isPlaying)
      {
        shoot.mute = true;
        rapid.mute = false;
      }

      yield return null;
    }
    rapid.mute = true;
    shoot.mute = false;
  }




  void CycleView()
  {

    if (transform.name == "XPlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0, 0.61f, -3.0f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(0, 0.085f, -0.018f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0, 0.21f, -1.15f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0, 0.51f, -2.0f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 0;
      }
    }
    if (transform.name == "APlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0, 200f, -750f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(0, 26f, -31f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0, 90f, -320f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0, 120f, -450f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 0;
      }
    }

    if (transform.name == "YPlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0, 900f, -3300f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(5, 112f, 767f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0, 550f, -1800f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0, 700f, -2500f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 0;
      }
    }

    if (transform.name == "FPlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0, 12f, -40f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(8.25f, 0.8f, 7.2f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0, 5f, -19f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0, 8.2f, -26.65f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 0;
      }
    }

    if (transform.name == "TPlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0f, 10f, -44f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(0f, 0f, 1.6f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0f, 5f, -22f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0f, 9f, -31f);
        playercamOrigPos = playercam.transform.localPosition;
        view = 0;
      }
    }

    if (transform.name == "SPlayer")
    {
      if (view == 0)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(0, 63f, -5f);
        playercam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;

        view = 1;
      }
      else if (view == 1)
      {
        //Tow
        playercam.transform.localPosition = new Vector3(0f, 70f, -93f);
        playercam.transform.localRotation = Quaternion.Euler(0, 180, 0);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0, 180f, -730f);
        playercam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0, 260, -950);
        playercam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 0;
      }
    }

    if (transform.name == "NPlayer")
    {
      if (view == 0)
      {
        //Far
        playercam.transform.localPosition = new Vector3(0f, 310f, -1415f);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 1;
      }
      else if (view == 1)
      {
        //Cockpit
        playercam.transform.localPosition = new Vector3(0f, 60f, -75);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.1f;
        view = 2;
      }
      else if (view == 2)
      {
        //Close
        playercam.transform.localPosition = new Vector3(0f, 225f, -670f);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 3;
      }
      else if (view == 3)
      {
        //Normal
        playercam.transform.localPosition = new Vector3(0f, 250f, -1000f);
        playercamOrigPos = playercam.transform.localPosition;
        playercam.GetComponent<Camera>().nearClipPlane = 0.3f;
        view = 0;
      }
    }

  }



  void OnTriggerEnter(Collider col)
  {
    if (col.name == "greenlaser(Clone)" && col.tag != "PlayersLaser")
    {
      damaged = true;
      playerHealth -= 10;
      UpdateHealthBar();
      Destroy(col.gameObject);
      if (playerHealth <= 0)
      {
        if (!isRunning)
        {
          isRunning = true;
          StartCoroutine(deathSpin());
        }
      }
    }

    if (col.name == "atatlaser(Clone)")
    {
      damaged = true;
      playerHealth -= 75;
      UpdateHealthBar();
      Destroy(col.gameObject);
      if (playerHealth <= 0)
      {
        if (!isRunning)
        {
          isRunning = true;
          StartCoroutine(deathSpin());
        }
      }
    }

    if (col.gameObject.tag == "boundry")
    {
      if (!isRunning)
      {
        isRunning = true;
        TurnBack.enabled = true;
        TurnBackTimer.enabled = true;
        StartCoroutine(Wait());
      }
      else if (isRunning)
      {
        isRunning = false;
        TurnBack.enabled = false;
        TurnBackTimer.enabled = false;
        StopAllCoroutines();
      }

    }

    if (col.gameObject.tag == "checkpoint")
    {
      spawnpoint = col.GetComponent<Transform>();
    }
  }

  public IEnumerator respawnCutScene()
  {
    respawnRunning = true;
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    if (transform.name == "APlayer")
    {
      playercam.transform.localPosition = new Vector3(190, -31, 607);
      playercam.transform.localRotation = new Quaternion(0, -155, 0, 0);
    }
    else if (transform.name == "XPlayer")
    {
      playercam.transform.localPosition = new Vector3(1, 0, 3);
      playercam.transform.localRotation = new Quaternion(0, -180, 0, 0);
    }
    else if (transform.name == "YPlayer")
    {
      playercam.transform.localPosition = new Vector3(860, -70, 3185);
      playercam.transform.localRotation = new Quaternion(0, -169, 0, 0);
    }
    else if (transform.name == "FPlayer")
    {
      playercam.transform.localPosition = new Vector3(18, -3, 45);
      playercam.transform.localRotation = new Quaternion(0, -203, 0, 0);
    }
    else if (transform.name == "TPlayer")
    {
      playercam.transform.localPosition = new Vector3(17, -11, 59);
      playercam.transform.localRotation = new Quaternion(0, -161, 0, 0);
    }
    else if (transform.name == "SPlayer")
    {
      playercam.transform.localPosition = new Vector3(350, -30, 1050);
      playercam.transform.localRotation = new Quaternion(0, -203, 0, 0);
    }
    else if (transform.name == "NPlayer")
    {
      playercam.transform.localPosition = new Vector3(390, -30, 1200);
      playercam.transform.localRotation = new Quaternion(0, -169, 0, 0);
    }
    StartCoroutine(MoveOverTime(playercam.transform, playercamOrigPos, 3f));
    StartCoroutine(RotateOverTime(playercam.transform, playercamOrigRot, 3f));
    yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
    respawnRunning = false;

  }
  static IEnumerator MoveOverTime(Transform theTransform, Vector3 d, float t)
  {
    float rate = 1 / t;
    float index = 0f;
    Vector3 startPosition = theTransform.localPosition;
    Vector3 endPosition = d;
    while (index < 1)
    {

      theTransform.localPosition = Vector3.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.localPosition = endPosition;
  }

  static IEnumerator RotateOverTime(Transform theTransform, Quaternion d, float t)
  {
    float rate = 1 / t;
    float index = 0f;
    Quaternion startPosition = theTransform.localRotation;
    Quaternion endPosition = d;
    while (index < 1)
    {

      theTransform.localRotation = Quaternion.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.localRotation = endPosition;
  }

  void respawn()
  {
    playerSpeed = playerSpeedOrig;
    playercam.transform.localPosition = playercamOrigPos;
    playercam.transform.localRotation = playercamOrigRot;
    lookingback = false;
    transform.GetComponent<Rigidbody>().isKinematic = true;
    hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
    if (Application.loadedLevelName == "level1")
    {
      ScoreUI.GetComponent<CanvasGroup>().alpha = 1;
      impScore += 1;
    }
    objCanvas.GetComponent<CanvasGroup>().alpha = 1;
    livesText.text = lifeCount.ToString();
    rotX = 0;
    rotY = 0;
    rotZ = 0;
    transform.position = spawnpoint.position;
    transform.rotation = spawnpoint.rotation;
    StartCoroutine(respawnCutScene());
    transform.GetComponent<BoxCollider>().enabled = true;
    Renderer[] rs = GetComponentsInChildren<Renderer>();
    foreach (Renderer r in rs)
    {
      r.enabled = true;
    }
    if (transform.name == "XPlayer")
    {
      playerHealth = playerHealthOrig;
      GetComponent<LockOnScript>().missileammo = 3;
      GetComponent<LockOnScript>().missiles.text = "3";
      GetComponent<LockOnScript>().noMissiles = false;
      GetComponent<LockOnScript>().hasAmmo = true;
      GetComponent<LockOnScript>().locked = false;
      engineGlow1.GetComponent<Light>().enabled = true;
      engineGlow2.GetComponent<Light>().enabled = true;
      engineGlow3.GetComponent<Light>().enabled = true;
      engineGlow4.GetComponent<Light>().enabled = true;
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
      etrail3.GetComponent<Renderer>().enabled = false;
      etrail4.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "APlayer")
    {
      playerHealth = playerHealthOrig;
      GetComponent<LockOnScript>().missileammo = 3;
      GetComponent<LockOnScript>().missiles.text = "3";
      GetComponent<LockOnScript>().noMissiles = false;
      GetComponent<LockOnScript>().hasAmmo = true;
      GetComponent<LockOnScript>().locked = false;
      engineGlow1.GetComponent<Light>().enabled = true;
      engineGlow2.GetComponent<Light>().enabled = true;
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "SPlayer")
    {
      playerHealth = playerHealthOrig;
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "NPlayer")
    {
      playerHealth = playerHealthOrig;
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "YPlayer")
    {
      playerHealth = playerHealthOrig;
      GetComponent<LockOnScript>().missileammo = 6;
      GetComponent<LockOnScript>().missiles.text = "6";
      GetComponent<LockOnScript>().noMissiles = false;
      GetComponent<LockOnScript>().hasAmmo = true;
      GetComponent<LockOnScript>().locked = false;
      engineGlow1.GetComponent<Light>().enabled = true;
      engineGlow2.GetComponent<Light>().enabled = true;
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "FPlayer")
    {
      playerHealth = playerHealthOrig;
      GetComponent<LockOnScript>().missileammo = 6;
      GetComponent<LockOnScript>().missiles.text = "6";
      GetComponent<LockOnScript>().noMissiles = false;
      GetComponent<LockOnScript>().hasAmmo = true;
      GetComponent<LockOnScript>().locked = false;
      var engineglows = transform.GetComponentsInChildren<Light>();
      foreach (Light glow in engineglows)
      {
        glow.enabled = true;
      }
      etrail.GetComponent<Renderer>().enabled = false;
      etrail2.GetComponent<Renderer>().enabled = false;
    }
    if (transform.name == "TPlayer")
    {
      playerHealth = playerHealthOrig;
      engineGlow1.GetComponent<TrailRenderer>().enabled = true;
    }

    healthSlider.maxValue = playerHealth;
    healthSlider.value = playerHealth;
    Fill.color = Color.green;
    damaged = false;
    transform.GetComponent<Rigidbody>().isKinematic = false;
    died = false;
  }

  public IEnumerator deathSpin()
  {
    rotX = 0;
    rotY = 0;
    rotZ = 0;
    hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
    ScoreUI.GetComponent<CanvasGroup>().alpha = 0;
    objCanvas.GetComponent<CanvasGroup>().alpha = 0;
    died = true;
    transform.GetComponent<BoxCollider>().enabled = false;
    Renderer[] rs = GetComponentsInChildren<Renderer>();
    foreach (Renderer r in rs)
    {
      r.enabled = false;
    }
    Instantiate(explosion, transform.position, transform.rotation);

    if (transform.name == "XPlayer")
    {
      sounds[Random.Range(6, sounds.Length)].Play();
      engineGlow1.GetComponent<Light>().enabled = false;
      engineGlow2.GetComponent<Light>().enabled = false;
      engineGlow3.GetComponent<Light>().enabled = false;
      engineGlow4.GetComponent<Light>().enabled = false;
    }
    else if (transform.name == "APlayer")
    {
      sounds[Random.Range(5, sounds.Length)].Play();
      engineGlow1.GetComponent<Light>().enabled = false;
      engineGlow2.GetComponent<Light>().enabled = false;
    }
    else if (transform.name == "YPlayer")
    {
      sounds[Random.Range(6, sounds.Length)].Play();
      engineGlow1.GetComponent<Light>().enabled = false;
      engineGlow2.GetComponent<Light>().enabled = false;
    }
    else if (transform.name == "FPlayer")
    {
      sounds[Random.Range(6, sounds.Length)].Play();
      var engineglows = transform.GetComponentsInChildren<Light>();
      foreach (Light glow in engineglows)
      {
        glow.enabled = false;
      }
    }
    if (transform.name == "TPlayer")
    {
      sounds[Random.Range(2, sounds.Length)].Play();
      engineGlow1.GetComponent<TrailRenderer>().enabled = false;
    }
    if (transform.name == "SPlayer")
    {
      sounds[Random.Range(3, sounds.Length)].Play();
    }
    if (transform.name == "NPlayer")
    {
      sounds[Random.Range(2, sounds.Length)].Play();
    }
    yield return new WaitForSeconds(3f);
    lifeCount--;
    playerDeaths += 1;
    //If not on level1(First mission)
    if (Application.loadedLevelName != "level1")
    {
      ///////////////////Check if players Lives reach 0, if so GameOver/////////////////////////////
      if (lifeCount <= -1)
      {
        if (!gameoverrunning)
        {
          youwin.GetComponent<CanvasGroup>().alpha = 1;
          hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
          objCanvas.GetComponent<CanvasGroup>().alpha = 0;
          ScoreUI.GetComponent<CanvasGroup>().alpha = 0;
          winScreen.text = "Game Over";
          winScreen.color = Color.red;
          ScoreText.text = "Kills: " + playerKills + "    Deaths: " + playerDeaths + " ";
          ScoreText.color = Color.white;
          gameover = true;
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          // Time.timeScale = 0;
          StartCoroutine(gameOver());
        }
      }
    }
    if (!gameover)
    {
      respawn();
    }
    isRunning = false;
  }



  //Sets up timing for Out of Bounds countdown
  public IEnumerator Wait()
  {
    int num = 5;
    if (num == 5)
    {
      TurnBackTimer.text = "5";
      yield return new WaitForSeconds(1);
      num = 4;
    }
    if (num == 4)
    {
      TurnBackTimer.text = "4";
      yield return new WaitForSeconds(1);
      num = 3;
    }
    if (num == 3)
    {
      TurnBackTimer.text = "3";
      yield return new WaitForSeconds(1);
      num = 2;
    }
    if (num == 2)
    {
      TurnBackTimer.text = "2";
      yield return new WaitForSeconds(1);
      num = 1;
    }
    if (num == 1)
    {
      TurnBackTimer.text = "1";
      yield return new WaitForSeconds(1);
      num = 0;
    }
    if (num == 0)
    {

      TurnBackTimer.text = "0";
      yield return new WaitForSeconds(1);
      TurnBack.enabled = false;
      TurnBackTimer.enabled = false;
      StartCoroutine(deathSpin());
    }
    isRunning = false;
  }




  void OnCollisionEnter(Collision col)
  {
    if (col.collider.name == "Terrain")
    {
      if (died)
      {
        Instantiate(explosion, transform.position, transform.rotation);
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
          r.enabled = false;
        }
      }
    }

    if (col.collider.tag == "Empire")
    {
      damaged = true;
      playerHealth -= 100;
      UpdateHealthBar();
      if (playerHealth <= 0)
      {
        if (!isRunning)
        {
          isRunning = true;
          StartCoroutine(deathSpin());
        }
      }
    }

    if (col.collider.tag == "terrain" || col.collider.tag == "structure" || col.collider.tag == "ATAT")
    {
      damaged = true;
      playerHealth -= playerHealth;
      UpdateHealthBar();
      if (playerHealth <= 0)
      {
        if (!isRunning)
        {
          isRunning = true;
          StartCoroutine(deathSpin());
        }
      }
    }

  }
  public IEnumerator gameOver()
  {
    gameoverrunning = true;
    yield return new WaitForSeconds(2f);
    StartCoroutine(Globals.FadeToBlack(1f, 1.0f));
    yield return new WaitForSeconds(2f);
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.Confined;
    Time.timeScale = 1;
    Application.LoadLevel("levelselect");
  }
}
