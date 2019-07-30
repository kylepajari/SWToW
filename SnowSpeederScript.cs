using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowSpeederScript : MonoBehaviour
{

  public AudioSource[] sounds;
  public AudioSource TowCableSound;
  public AudioSource TowCableOutOfRange;

  public Camera m_MainCamera;

  public GameObject TowCable;

  void Start()
  {
    m_MainCamera = Camera.main;
    sounds = GetComponents<AudioSource>();
    TowCableOutOfRange = sounds[2];
    // TowCable = sounds[5];

    TowCable = GameObject.Find("TowCableGun");
  }

  // Update is called once per frame
  void Update()
  {
    RaycastHit hit;
    Vector3 right = transform.TransformDirection(Vector3.right) * 20;
    Vector3 left = transform.TransformDirection(Vector3.left) * 20;
    Debug.DrawRay(transform.position, right, Color.green);
    Debug.DrawRay(transform.position, left, Color.green);

    if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Mouse1))
    {
      //check to see if ship is near atat
      if (Physics.Raycast(transform.position, left, out hit, 20) || Physics.Raycast(transform.position, right, out hit, 20) && hit.collider.tag == "ATAT")
      {
        //create cable
        RopeScript cableScript = TowCable.GetComponent<RopeScript>();
        cableScript.target = hit.transform;
        cableScript.Attach();
      }
      else
      {
        TowCableOutOfRange.Play();
      }
    }
  }
}
