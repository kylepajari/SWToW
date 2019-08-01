using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level5introscript : MonoBehaviour
{

  public GameObject gozanti;

  public bool isRunning;

  public GameObject DialogCanvas;

  public GameObject Loading;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    gozanti = GameObject.Find("Gozanti");
    Transform ship = gozanti.transform;
    GameObject gm = GameObject.Find("GameManager");
    Loading = GameObject.Find("LoadingUI");
    DialogCanvas = GameObject.Find("DialogCanvas");
    DialogCanvas.GetComponent<CanvasGroup>().alpha = 0;
    Globals.Fade = GameObject.Find("FadeToBlack");

  }

  // Update is called once per frame
  void Update()
  {

    if (!isRunning)
    {

      StartCoroutine(introsequence());
    }

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level5");
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
    int num = 3;
    if (num == 3)
    {
      StartCoroutine(MoveOverTime(gozanti.transform, (gozanti.transform.forward * 350), 8));
      yield return new WaitForSeconds(2f);
      StartCoroutine(FadeTo(1f, 2.0f));
      yield return new WaitForSeconds(5f);
      StartCoroutine(FadeAway(0f, 2.0f));
      StartCoroutine(RotateOverTime(gozanti.transform, Quaternion.Euler(-5, -527, 0), 2f));
      num = 2;
    }
    if (num == 2)
    {

      StartCoroutine(MoveOverTime(gozanti.transform, (-gozanti.transform.up * 72), 5));
      yield return new WaitForSeconds(6f);
      StartCoroutine(Globals.FadeToBlack(1f, 1.0f));
      num = 1;
    }
    if (num == 1)
    {
      yield return new WaitForSeconds(1.5f);
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("level5");
    }
  }
}
