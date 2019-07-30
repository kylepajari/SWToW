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

  public Transform cannon;
  public Transform cannon2;

  public AudioSource[] sounds;
  public AudioSource shoot;

  public GameObject greenlaser;

  public GameObject Loading;

  // Use this for initialization
  void Start()
  {
    Time.timeScale = 1;
    destroyer = GameObject.Find("StarDestroyer");
    Transform ship = destroyer.transform;
    cannon = ship.Find("cannon");
    cannon2 = ship.Find("cannon2");
    sounds = GetComponents<AudioSource>();
    shoot = sounds[1];
    moncal = GameObject.Find("MonCalCruiser");
    GameObject gm = GameObject.Find("GameManager");
    camPos1 = GameObject.Find("camPos1").transform;
    camPos2 = GameObject.Find("camPos2").transform;
    DialogCanvas = GameObject.Find("DialogCanvas");
    DialogCanvas.GetComponent<CanvasGroup>().alpha = 0;
    canLook = false;
    Loading = GameObject.Find("LoadingUI");

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

  void ShootLeft()
  {
    GameObject laser1 = Instantiate(greenlaser, cannon.transform.position, cannon.transform.rotation) as GameObject;
    laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (20 * 10f), ForceMode.Impulse);
    shoot.Play();
  }

  void ShootRight()
  {
    GameObject laser1 = Instantiate(greenlaser, cannon2.transform.position, cannon2.transform.rotation) as GameObject;
    laser1.GetComponent<Rigidbody>().AddForce(transform.forward * (20 * 10f), ForceMode.Impulse);
    shoot.Play();
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
      StartCoroutine(MoveOverTime(moncal.transform, (moncal.transform.right * 500), 10));
      yield return new WaitForSeconds(10f);
      num = 2;
    }
    if (num == 2)
    {
      transform.position = camPos2.position;
      StartCoroutine(MoveOverTime(destroyer.transform, (-destroyer.transform.up * 600), 30));
      yield return new WaitForSeconds(8f);
      ShootLeft();
      yield return new WaitForSeconds(1f);
      ShootRight();
      yield return new WaitForSeconds(2f);
      ShootLeft();
      yield return new WaitForSeconds(1f);
      ShootRight();
      yield return new WaitForSeconds(4f);
      ShootLeft();
      yield return new WaitForSeconds(1f);
      ShootRight();
      yield return new WaitForSeconds(13f);
      num = 1;
    }
    if (num == 1)
    {
      Loading.GetComponent<CanvasGroup>().alpha = 1;
      Application.LoadLevel("vehicleselect");
    }
  }
}
