using UnityEngine;
using System.Collections;

public class menuMusic : MonoBehaviour {

    private static menuMusic instance = null;
    public static menuMusic Instance
    {
        get { return instance; }
    }
    public AudioSource[] sounds;
    public AudioSource music;

    static bool AudioBegin = false;
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

    sounds = GetComponents<AudioSource>();
        music = sounds[0];
        //if (!AudioBegin)
        //{
            music.Play();
            DontDestroyOnLoad(gameObject);
        //    AudioBegin = true;
        //}
    }
    void Update()
    {
        if (Application.loadedLevelName == "vehicleselect" || Application.loadedLevelName == "level3Intro")
        {
            music.Stop();
            //AudioBegin = false;
        }
        else if (Application.loadedLevelName == "MainMenu")
        {
            if (!music.isPlaying)
            {
                music.Play();
            }          
        }
    }
}
