using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitScript : MonoBehaviour
{

  public Transform sparkEffect;
  public Transform electricEffect;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter(Collider col)
  {
    if (col.tag == "Empire" || col.tag == "Rebel")
    {
      if (transform.name.Contains("laser") && !col.name.Contains("Player"))
      {
        Instantiate(sparkEffect, transform.position, transform.rotation);
      }
      if (transform.name.Contains("ion"))
      {
        Instantiate(electricEffect, transform.position, transform.rotation);
      }
    }
    else
    {
      if (!col.name.Contains("laser"))
      {
        if (transform.name.Contains("laser"))
        {
          Instantiate(sparkEffect, transform.position, transform.rotation);
        }
        if (transform.name.Contains("ion"))
        {
          Instantiate(electricEffect, transform.position, transform.rotation);
        }
      }
    }
  }
}
