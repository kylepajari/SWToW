using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{


  public bool isPaused;
  public GameObject PauseCanvas;
  public GameObject hudCanvas;
  public GameObject scoreCanvas;
  public GameObject objCanvas;

  void Start()
  {
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    isPaused = false;
    hudCanvas = GameObject.Find("HUDCanvas");
    scoreCanvas = GameObject.Find("ScoreUI");
    objCanvas = GameObject.Find("ObjectiveUI");

    PauseCanvas = GameObject.Find("PauseCanvas");
    PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;

  }


  void Update()
  {

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameObject.Find("XPlayer") != null)
      {
        PlayerControls XPlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
        if (XPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("APlayer") != null)
      {
        PlayerControls APlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
        if (APlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("YPlayer") != null)
      {
        PlayerControls YPlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
        if (YPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("TPlayer") != null)
      {
        PlayerControls TPlayerScript = GameObject.Find("TPlayer").GetComponent<PlayerControls>();
        if (TPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("FPlayer") != null)
      {
        PlayerControls FPlayerScript = GameObject.Find("FPlayer").GetComponent<PlayerControls>();
        if (FPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("SPlayer") != null)
      {
        PlayerControls SPlayerScript = GameObject.Find("SPlayer").GetComponent<PlayerControls>();
        if (SPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

      if (GameObject.Find("NPlayer") != null)
      {
        PlayerControls NPlayerScript = GameObject.Find("NPlayer").GetComponent<PlayerControls>();
        if (NPlayerScript.gameover == false)
        {
          if (!isPaused)
          {
            Pause();
          }
          else if (isPaused)
          {
            UnPause();
          }
        }
      }

    }

  }

  public void Pause()
  {
    isPaused = true;
    PauseCanvas.GetComponent<CanvasGroup>().alpha = 1;
    hudCanvas.GetComponent<CanvasGroup>().alpha = 0;
    scoreCanvas.GetComponent<CanvasGroup>().alpha = 0;
    objCanvas.GetComponent<CanvasGroup>().alpha = 0;
    AudioListener.pause = true;
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.Confined;
    Time.timeScale = 0;
  }

  public void UnPause()
  {
    isPaused = false;
    PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
    hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
    if (Application.loadedLevelName.Contains("level2"))
    {
      scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }
    objCanvas.GetComponent<CanvasGroup>().alpha = 1;
    AudioListener.pause = false;
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
    Time.timeScale = 1;
  }

  public void Restart()
  {
    Application.LoadLevel(Application.loadedLevelName.ToString());
  }

  public void QuitToMain()
  {
    isPaused = false;
    AudioListener.pause = false;
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.Confined;
    Time.timeScale = 1;
    Application.LoadLevel("MainMenu");
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
