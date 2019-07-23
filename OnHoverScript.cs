using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour {

	public AudioSource[] buttonSounds;
  public AudioSource buttonHover;
  public AudioSource buttonConfirm;

	// Use this for initialization
	void Start () {
		buttonSounds = GetComponents<AudioSource>();
        buttonHover = buttonSounds[0];
        buttonConfirm = buttonSounds[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnHover() {
        if(buttonHover.isActiveAndEnabled)
        {
            buttonHover.Play();
        }
        else
        {
            print("cannot play hover");
        }
        
    }

    public void OnConfirm() {
        if(buttonConfirm.isActiveAndEnabled)
        {
            buttonConfirm.Play();
        }
        else
        {
            print("cannot play confirm");
        }
        
    }
}
