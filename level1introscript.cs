using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1introscript : MonoBehaviour
{

  public GameObject destroyer;

  public GameObject moncal;
  public bool isRunning;

  public Transform camPos1;
  public Transform camPos2;

  public GameObject DialogCanvas;

  public bool canLook;

  public GameObject Loading;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    destroyer = GameObject.Find("StarDestroyer");
    Transform ship = destroyer.transform;
    moncal = GameObject.Find("MonCalCruiser");
    GameObject gm = GameObject.Find("GameManager");
    camPos1 = GameObject.Find("camPos1").transform;
    camPos2 = GameObject.Find("camPos2").transform;
    DialogCanvas = GameObject.Find("DialogCanvas");
    DialogCanvas.GetComponent<CanvasGroup>().alpha = 0;
    canLook = false;
    Loading = GameObject.Find("LoadingUI");
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
      transform.LookAt(moncal.transform);
    }

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level1");
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
    Quaternion startPosition = theTransform.rotation;
    Quaternion endPosition = d;
    while (index < 1)
    {

      theTransform.rotation = Quaternion.Lerp(startPosition, endPosition, index);
      index += rate * Time.deltaTime;
      yield return index;
    }
    theTransform.rotation = endPosition;
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
      //pan down over Yavin
      StartCoroutine(RotateOverTime(transform, Quaternion.Euler(-3, 260, 0), 8));
      yield return new WaitForSeconds(4f);
      // DialogCanvas.GetComponent<CanvasGroup>().alpha = 1;
      StartCoroutine(FadeTo(1f, 2.0f));
      yield return new WaitForSeconds(5f);
      num = 3;
    }
    if (num == 3)
    {
      //snap to behind the moncal
      StartCoroutine(FadeAway(0f, 2.0f));
      transform.position = camPos1.position;
      canLook = true;
      StartCoroutine(MoveOverTime(moncal.transform, (-moncal.transform.up * 500), 10));
      yield return new WaitForSeconds(10f);
      num = 2;
    }
    if (num == 2)
    {
      transform.position = camPos2.position;
      StartCoroutine(MoveOverTime(destroyer.transform, (-destroyer.transform.up * 600), 24));
      yield return new WaitForSeconds(23f);
      StartCoroutine(Globals.FadeToBlack(1f, 1.0f));
      yield return new WaitForSeconds(2f);
      num = 1;
    }
    if (num == 1)
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level1");
    }
  }
}
