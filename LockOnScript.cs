using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnScript : MonoBehaviour {

    //================================================
    //Lock On Script
    //================================================

    public GameObject lockOn;
    public GameObject EnemyUI;
    public bool locked;
    public GameObject closest;
    public Text eName;
    public Text eHealth;
    public Text eDis;
    public Transform cannon5;
    public float lockX;
    public float lockY;
    public float lockZ;
    public Text notLocked;
    public AudioSource missileFire;
    public AudioSource outofAmmo;
    public GameObject missile1;
    public Text missiles;
    public bool noMissiles;
    public GameObject missile;
    public float missileammo;
    public bool hasAmmo;
    public AudioSource[] sounds;
    public float playerSpeed;
    public Color flashcolor1 = new Color(1f, 0f, 0f);

    public Texture reticle;

    public float reticle_size;

    public Camera m_MainCamera;

    void Start()
    {
        m_MainCamera = Camera.main;
        playerSpeed = GetComponent<PlayerControls>().playerSpeed;
        lockOn = GameObject.Find("LockOn");
        EnemyUI = GameObject.Find("EnemyUI");
        eName = GameObject.Find("eName").GetComponent<Text>();
        eHealth = GameObject.Find("eHealth").GetComponent<Text>();
        eDis = GameObject.Find("eDis").GetComponent<Text>();
        notLocked = GameObject.Find("CantSecFire").GetComponent<Text>();
        missiles = GameObject.Find("missileCount").GetComponent<Text>();
        sounds = GetComponents<AudioSource>();
        locked = false;
        notLocked.GetComponent<CanvasGroup>().alpha = 0;
        EnemyUI.GetComponent<CanvasGroup>().alpha = 0;
        lockOn.GetComponent<CanvasGroup>().alpha = 0;
        if (transform.name == "XPlayer" || transform.name == "APlayer" || transform.name == "FPlayer" || transform.name == "YPlayer")
        {
            cannon5 = transform.Find("protonCannon");
        }
        if(transform.name == "XPlayer" || transform.name == "APlayer")
        {
            missileammo = 3;
        }
        if (transform.name == "FPlayer" || transform.name == "YPlayer")
        {
            missileammo = 6;
        }
        if (transform.name == "YPlayer" || transform.name == "FPlayer")
        {
            outofAmmo = sounds[2];
            missileFire = sounds[3];
        }
        if (transform.name == "APlayer")
        {
            outofAmmo = sounds[3];
            missileFire = sounds[2];
        }
        if (transform.name == "XPlayer")
        {
            outofAmmo = sounds[3];
            missileFire = sounds[4];
        }
        hasAmmo = true;  
        missiles.text = missileammo.ToString();



    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Mouse2))
        {
            //If lock is engaged
            if (locked)
            {
                //Turn off lock related items on screen
                notLocked.GetComponent<CanvasGroup>().alpha = 0;
                EnemyUI.GetComponent<CanvasGroup>().alpha = 0;
                //Turn Locked to false
                locked = false;
                //Remove any Locked enemy if found
                closest = null;
            }
            //check if not currently Locked onto enemy
            else
            {
                //Look for closest enemy to player
                FindClosestEnemy();
                locked = !locked;
            }
        }


        //Secondary Fire(Proton Torpedo)
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Mouse1))
        {

            if (hasAmmo)
            {
                if (missile1 == null)
                {

                    //Create a missile object
                    missile1 = Instantiate(missile, cannon5.transform.position, cannon5.transform.rotation) as GameObject;
                    missile1.tag = "PlayersMissile";
                    //Make missile move forward
                    missile1.GetComponent<Rigidbody>().velocity = missile1.transform.forward * (playerSpeed * 5f);


                    //Play missile fire sound
                    missileFire.Play();

                    //Reduce missile count by 1
                    missileammo -= 1;

                    //Display new count on HUD
                    missiles.text = missileammo.ToString();

                    //Check if ran out of missiles
                    if (missileammo <= 0)
                    {
                        //Set player has no missiles left to TRUE
                        hasAmmo = false;
                    }
                }
            }

            //If PLayer has no missiles left
            else
            {
                //Set no missiles to TRUE
                noMissiles = true;

                //Set has missile ammo to FALSE
                hasAmmo = false;

                //Play out of ammo sound
                outofAmmo.Play();
            }
        }

        if (missile1 != null)
        {
            if (locked)
            {
                if(closest)
                {
                    missile1.transform.LookAt(closest.transform);
                }
                missile1.GetComponent<Rigidbody>().velocity = missile1.transform.forward * (playerSpeed * 5f);
            }

        }



        //If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
        //If closest is false or null
        if (!closest)
        {
            EnemyUI.GetComponent<CanvasGroup>().alpha = 0;
            locked = false;
            closest = null;
        }
        //Or if have a current target lock
        else
        {
            //Grab name, health, and distance to player from current locked enemy and display them on screen in lower right corner
            if (closest.name.Contains("Tie_Interceptor"))
            {
                eName.text = "Tie Interceptor";
            }
            if (closest.name.Contains("TieParent"))
            {
                eName.text = "Tie Fighter";
            }
            if (closest.name.Contains("Tie_Bomber"))
            {
                eName.text = "Tie Bomber";
            }
            if (closest.name == "shuttle")
            {
                eName.text = "Imperial Shuttle";
            }
            if (closest.name.Contains("atst"))
            {
                eName.text = "AT-ST";
            }
            if (closest.name.Contains("turbobottom"))
            {
                eName.text = "Turbolaser Turret";
            }
            
            if (closest.name == "shuttle")
            {
                eHealth.text =  closest.GetComponent<VeersShuttleScript>().enemyHealth.ToString();
            }
            else if (closest.name.Contains("atst"))
            {
                eHealth.text = closest.GetComponent<ATSTscript>().enemyHealth.ToString();
            }
            else if (closest.name.Contains("turbobottom"))
            {
                eHealth.text = closest.GetComponent<turboscript>().enemyHealth.ToString();
            }
            else 
            {
                eHealth.text = closest.GetComponent<enemyControls>().enemyHealth.ToString();
            }
            
            eDis.text = Convert.ToInt32(Vector3.Distance(closest.transform.position, transform.position)).ToString();

            //Set Lock to true
            locked = true;

            //Turn on enemy info display
            EnemyUI.GetComponent<CanvasGroup>().alpha = 1;
            
        }




        if (noMissiles)
        {
            missiles.color = flashcolor1;
        }
        else
        {
            // ... transition the colour back to clear.
            missiles.color = Color.Lerp(missiles.color, Color.white, 3 * Time.deltaTime);
        }
        noMissiles = false;
        

    }


    // Find the name of the closest enemy
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Empire");
        float distance = Mathf.Infinity;
        Vector3 currentposition = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentposition;
            float curDistance = directionToTarget.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void OnGUI(){
        
         if(locked && closest)
         {
             lockOn.transform.position = m_MainCamera.WorldToScreenPoint(closest.transform.position);
             GUI.DrawTexture(new Rect((lockOn.transform.position.x-(reticle_size/2f)),(Screen.height-(lockOn.transform.position.y+(reticle_size/2f))), reticle_size,reticle_size), reticle, ScaleMode.ScaleToFit);
         }
    }

}
