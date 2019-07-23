using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {


    public bool isPaused;
    public GameObject PauseCanvas;
    public GameObject hudCanvas;
    public GameObject scoreCanvas;
    public GameObject objCanvas;

    void Start ()
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
	

	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (GameObject.Find ("XPlayer") != null) 
			{
				PlayerControls XPlayerScript = GameObject.Find("XPlayer").GetComponent<PlayerControls>();
				if (XPlayerScript.gameover == false) 
				{
					if(!isPaused)
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
					else if(isPaused)
					{
						isPaused = false;
						PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
                        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        objCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        AudioListener.pause = false;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;

                    }
				}
			}

			if (GameObject.Find ("APlayer") != null) 
			{
				PlayerControls APlayerScript = GameObject.Find("APlayer").GetComponent<PlayerControls>();
				if (APlayerScript.gameover == false) 
				{
					if(!isPaused)
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
					else if(isPaused)
					{
						isPaused = false;
						PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
                        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        objCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        AudioListener.pause = false;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;

                    }
				}
			}

			if (GameObject.Find ("YPlayer") != null) 
			{
				PlayerControls YPlayerScript = GameObject.Find("YPlayer").GetComponent<PlayerControls>();
				if (YPlayerScript.gameover == false) 
				{
					if(!isPaused)
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
					else if(isPaused)
					{
						isPaused = false;
						PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
                        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        objCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        AudioListener.pause = false;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;

                    }
				}
			}

			if (GameObject.Find ("TPlayer") != null) 
			{
				PlayerControls TPlayerScript = GameObject.Find("TPlayer").GetComponent<PlayerControls>();
				if (TPlayerScript.gameover == false) 
				{
					if(!isPaused)
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
					else if(isPaused)
					{
						isPaused = false;
						PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
                        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        objCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        AudioListener.pause = false;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;

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
                    else if (isPaused)
                    {
                        isPaused = false;
                        PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
                        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        scoreCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        objCanvas.GetComponent<CanvasGroup>().alpha = 1;
                        AudioListener.pause = false;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Time.timeScale = 1;

                    }
                }
            }

        }
	
	}

    public void UnPause()
    {
        isPaused = false;
        PauseCanvas.GetComponent<CanvasGroup>().alpha = 0;
        hudCanvas.GetComponent<CanvasGroup>().alpha = 1;
        if (Application.loadedLevelName == "level2falcon" || Application.loadedLevelName == "level2xwing" || Application.loadedLevelName == "level2awing" || Application.loadedLevelName == "level2ywing" || Application.loadedLevelName == "level2tie")
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
        Time.timeScale = 1;
        if (Application.loadedLevelName == "level2xwing")
        {
            Application.LoadLevel("level2xwing");
        }

        if (Application.loadedLevelName == "level2awing")
        {
            Application.LoadLevel("level2awing");
        }

        if (Application.loadedLevelName == "level2ywing")
        {
            Application.LoadLevel("level2ywing");
        }

        if (Application.loadedLevelName == "level2falcon")
        {
            Application.LoadLevel("level2falcon");
        }

        if (Application.loadedLevelName == "level2tie")
        {
            Application.LoadLevel("level2tie");
        }

        if (Application.loadedLevelName == "level3xwing")
        {
            Application.LoadLevel("level3xwing");
        }

        if (Application.loadedLevelName == "level3awing")
        {
            Application.LoadLevel("level3awing");
        }

        if (Application.loadedLevelName == "level3ywing")
        {
            Application.LoadLevel("level3ywing");
        }

        if (Application.loadedLevelName == "level3falcon")
        {
            Application.LoadLevel("level3falcon");
        }

        if (Application.loadedLevelName == "level3tie")
        {
            Application.LoadLevel("level3tie");
        }

        if (Application.loadedLevelName == "level4awing")
        {
            Application.LoadLevel("level4awing");
        }
        if (Application.loadedLevelName == "level5awing")
        {
            Application.LoadLevel("level5awing");
        }
        if (Application.loadedLevelName == "level5xwing")
        {
            Application.LoadLevel("level5xwing");
        }
        if (Application.loadedLevelName == "level5ywing")
        {
            Application.LoadLevel("level5ywing");
        }
        if (Application.loadedLevelName == "level5falcon")
        {
            Application.LoadLevel("level5falcon");
        }
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
