using UnityEngine;
using System.Collections;

public class level3introscript : MonoBehaviour
{

  public GameObject shuttle;
  public GameObject leftwing;
  public GameObject rightwing;
  public GameObject tie1;
  public GameObject tie2;
  public bool isRunning;

  public GameObject DialogCanvas;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    shuttle = GameObject.Find("ImpShuttle");
    leftwing = GameObject.Find("LeftWing");
    rightwing = GameObject.Find("RightWing");
    tie1 = GameObject.Find("TieParent");
    tie2 = GameObject.Find("TieParent (1)");
    GameObject gm = GameObject.Find("GameManager");
    DialogCanvas = GameObject.Find("DialogCanvas");

  }

  // Update is called once per frame
  void Update()
  {

    transform.LookAt(shuttle.transform);
    if (!isRunning)
    {
      StartCoroutine(introsequence());
    }

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
    {
      Application.LoadLevel("vehicleselect");
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

  public IEnumerator introsequence()
  {
    isRunning = true;
    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(shuttle.transform, (-shuttle.transform.up * 30), 6));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(leftwing.transform, Quaternion.Euler(0, 145, 0), 5));
      StartCoroutine(RotateOverTime(rightwing.transform, Quaternion.Euler(0, -145, 0), 5));
      yield return new WaitForSeconds(3f);
      StartCoroutine(MoveOverTime(tie1.transform, (-tie1.transform.up * 30), 3));
      StartCoroutine(MoveOverTime(tie2.transform, (-tie2.transform.up * 30), 3));
      yield return new WaitForSeconds(1f);
      num = 4;
    }
    if (num == 4)
    {
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(3, 180, 0), 1));
      yield return new WaitForSeconds(0.5f);
      num = 3;
    }
    if (num == 3)
    {
      StartCoroutine(MoveOverTime(shuttle.transform, (shuttle.transform.forward * 800), 20f));
      yield return new WaitForSeconds(0.5f);
      StartCoroutine(MoveOverTime(tie1.transform, (tie1.transform.forward * 800), 20f));
      StartCoroutine(MoveOverTime(tie2.transform, (tie2.transform.forward * 800), 20f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(FadeTo(1f, 2.0f));
      yield return new WaitForSeconds(8f);
      num = 2;
    }
    if (num == 2)
    {

      Application.LoadLevel("vehicleselect");
    }
  }

}
