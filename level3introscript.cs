﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level3introscript : MonoBehaviour
{

  public GameObject player;

  public bool isRunning;

  public Transform camPos1;

  public GameObject DialogCanvas;

  public bool canLook;

  public GameObject Loading;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    player = GameObject.Find("UserForCutscenes");
    GameObject gm = GameObject.Find("GameManager");
    camPos1 = GameObject.Find("camPos1").transform;
    DialogCanvas = GameObject.Find("DialogCanvas");
    DialogCanvas.GetComponent<CanvasGroup>().alpha = 0;
    canLook = false;
    Loading = GameObject.Find("LoadingUI");
    Loading.GetComponent<CanvasGroup>().alpha = 0;
    Globals.Fade = GameObject.Find("FadeToBlack");


  }

  // Update is called once per frame
  void Update()
  {

    if (!isRunning)
    {

      StartCoroutine(introsequence());
    }

    if (canLook)
    {
      transform.LookAt(player.transform);
    }

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level3");
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

  IEnumerator FadeTo(float aValue, float aTime)
  {
    float alpha = DialogCanvas.GetComponent<CanvasGroup>().alpha;
    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    {
      float newAlpha = t;
      DialogCanvas.GetComponent<CanvasGroup>().alpha = newAlpha;
      yield return null;
    }
  }

  IEnumerator FadeAway(float aValue, float aTime)
  {
    float alpha = DialogCanvas.GetComponent<CanvasGroup>().alpha;
    for (float t = 1.0f; t > 0.0f; t -= Time.deltaTime / aTime)
    {
      float newAlpha = t;
      DialogCanvas.GetComponent<CanvasGroup>().alpha = newAlpha;
      yield return null;
    }
  }



  public IEnumerator introsequence()
  {
    isRunning = true;
    int num = 4;
    if (num == 4)
    {
      StartCoroutine(RotateOverTime(Camera.main.transform, Quaternion.Euler(0, 180, 0), 5f));
      StartCoroutine(MoveOverTime(player.transform, (player.transform.forward * 3000), 180));
      yield return new WaitForSeconds(3f);
      StartCoroutine(FadeTo(1f, 2.0f));
      yield return new WaitForSeconds(3f);
      StartCoroutine(FadeAway(0f, 1.0f));
      num = 3;
    }
    if (num == 3)
    {
      canLook = true;
      yield return new WaitForSeconds(5f);
      num = 2;
    }
    if (num == 2)
    {
      transform.position = camPos1.position;
      transform.rotation = camPos1.rotation;
      canLook = false;
      var message = DialogCanvas.FindInChildren("Text").GetComponent<Text>();
      message.text = "Fuel Platform";
      StartCoroutine(FadeTo(1f, 2.0f));
      yield return new WaitForSeconds(3f);
      StartCoroutine(FadeAway(0f, 1.0f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(transform, Quaternion.Euler(-12, 370, 0), 1f));
      yield return new WaitForSeconds(1f);
      for (float f = Camera.main.fieldOfView; f > 15; f -= Time.deltaTime * 32f)
      {
        float fov = f;
        Camera.main.fieldOfView = fov;
        yield return null;
      }
      yield return new WaitForSeconds(3f);
      StartCoroutine(Globals.FadeToBlack(1f, 1.0f));
      yield return new WaitForSeconds(1f);
      num = 1;
    }
    if (num == 1)
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level3");
    }
  }
}
