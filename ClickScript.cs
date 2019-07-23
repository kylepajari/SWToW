using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClickScript : MonoBehaviour
{

  public Dropdown graphicsDropdown;
  public Toggle vsyncCheckbox;
  public Toggle soundCheckbox;

  public Toggle pitchCheckbox;
  private static ClickScript instance = null;
  public int qualityLevel = 0;
  public int vsyncOnOff = 0;
  public float soundOnOff = 0;

  public int pitchInv = 0;
  public GameObject QuitCanvas;
  public GameObject MenuCanvas;

  void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(this.gameObject);
      return;
    }
    else
    {
      instance = this;
    }
    DontDestroyOnLoad(this.gameObject);
  }

  // Use this for initialization
  void Start()
  {

    AudioListener.pause = false;
    AudioListener.volume = 1;
    QualitySettings.vSyncCount = 1;
    qualityLevel = QualitySettings.GetQualityLevel();
    vsyncOnOff = QualitySettings.vSyncCount;
    soundOnOff = AudioListener.volume;
    pitchInv = Globals.pitchInverted;

  }

  void Update()
  {
    if (Application.loadedLevelName == "MainMenu")
    {
      MenuCanvas = GameObject.Find("MenuCanvas");
      QuitCanvas = GameObject.Find("QuitCanvas");
    }
    else if (Application.loadedLevelName == "OptionsMenu")
    {
      if (graphicsDropdown == null)
      {
        graphicsDropdown = GameObject.Find("graphicsdrop").GetComponent<Dropdown>();
        graphicsDropdown.value = qualityLevel;
        graphicsDropdown.onValueChanged.AddListener(delegate { Quality(graphicsDropdown); });
      }
      if (vsyncCheckbox == null)
      {
        vsyncCheckbox = GameObject.Find("VSyncToggle").GetComponent<Toggle>();
        if (vsyncOnOff == 0)
        {
          vsyncCheckbox.isOn = false;
        }
        else if (vsyncOnOff != 0)
        {
          vsyncCheckbox.isOn = true;
        }
        vsyncCheckbox.onValueChanged.AddListener(delegate { VSyncToggle(vsyncCheckbox); });
      }
      if (soundCheckbox == null)
      {
        soundCheckbox = GameObject.Find("SoundToggle").GetComponent<Toggle>();
        if (soundOnOff == 0)
        {
          soundCheckbox.isOn = false;
        }
        else if (soundOnOff != 0)
        {
          soundCheckbox.isOn = true;
        }
        soundCheckbox.onValueChanged.AddListener(delegate { SoundToggle(soundCheckbox); });
      }
      if (pitchCheckbox == null)
      {
        pitchCheckbox = GameObject.Find("PitchToggle").GetComponent<Toggle>();
        if (pitchInv == 0)
        {
          pitchCheckbox.isOn = false;
        }
        else
        {
          pitchCheckbox.isOn = true;
        }
        pitchCheckbox.onValueChanged.AddListener(delegate { PitchToggle(pitchCheckbox); });
      }
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (Application.loadedLevelName == "MainMenu")
      {
        if (QuitCanvas.GetComponent<CanvasGroup>().alpha == 0)
        {
          QuitCanvas.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
          QuitCanvas.GetComponent<CanvasGroup>().alpha = 0;
        }
      }
      else if (Application.loadedLevelName == "OptionsMenu")
      {
        Application.LoadLevel("MainMenu");
      }
      else if (Application.loadedLevelName == "levelselect")
      {
        Application.LoadLevel("MainMenu");
      }
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

  public void SoundToggle(Toggle change)
  {
    if (change.isOn)
    {
      print("sound on");
      AudioListener.volume = 1;
      soundOnOff = AudioListener.volume;
    }
    else
    {
      print("sound off");
      AudioListener.volume = 0;
      soundOnOff = AudioListener.volume;
    }

  }

  public void VSyncToggle(Toggle change)
  {
    if (change.isOn)
    {
      print("vsync on");
      QualitySettings.vSyncCount = 1;
      vsyncOnOff = QualitySettings.vSyncCount;
    }
    else
    {
      print("vsync off");
      QualitySettings.vSyncCount = 0;
      vsyncOnOff = QualitySettings.vSyncCount;
    }

  }

  public void PitchToggle(Toggle change)
  {
    if (change.isOn)
    {
      print("pitch inverted");
      Globals.pitchInverted = 1;
      pitchInv = Globals.pitchInverted;
    }
    else
    {
      print("pitch normal");
      Globals.pitchInverted = 0;
      pitchInv = Globals.pitchInverted;
    }

  }

  public void MainMenu()
  {

    Application.LoadLevel("MainMenu");

  }

  public void MainMenuQuit()
  {
    QuitCanvas = GameObject.Find("QuitCanvas");
    QuitCanvas.GetComponent<CanvasGroup>().alpha = 1;
    EventSystem.current.SetSelectedGameObject(null);
  }

  public void Quit()
  {

    //Quits game when is standalone/built
    Application.Quit();

    //Stops play mode if in editor(Remove Before Building)//////////////////////////////
    //UnityEditor.EditorApplication.isPlaying = false;
  }

  public void NotQuit()
  {
    QuitCanvas = GameObject.Find("QuitCanvas");
    QuitCanvas.GetComponent<CanvasGroup>().alpha = 0;
    EventSystem.current.SetSelectedGameObject(null);
  }



  public void Quality(Dropdown change)
  {
    if (graphicsDropdown.value == 0)
    {
      QualitySettings.SetQualityLevel(0);
      qualityLevel = 0;
    }
    else if (graphicsDropdown.value == 1)
    {
      QualitySettings.SetQualityLevel(1);
      qualityLevel = 1;
    }
    else if (graphicsDropdown.value == 2)
    {
      QualitySettings.SetQualityLevel(2);
      qualityLevel = 2;
    }
    else if (graphicsDropdown.value == 3)
    {
      QualitySettings.SetQualityLevel(3);
      qualityLevel = 3;
    }
    else if (graphicsDropdown.value == 4)
    {
      QualitySettings.SetQualityLevel(4);
      qualityLevel = 4;
    }
    else if (graphicsDropdown.value == 5)
    {
      QualitySettings.SetQualityLevel(5);
      qualityLevel = 5;
    }
  }

}
