using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level4OutroScript : MonoBehaviour
{

  public GameObject shuttle;
  public GameObject follower;
  public Transform maincam;
  public GameObject wincan;
  public bool isRunning;

  // Use this for initialization
  void Start()
  {
    shuttle = GameObject.Find("corvette");
    follower = GameObject.Find("AWingAlly");
    maincam = GameObject.Find("Main Camera").transform;
    wincan = GameObject.Find("Canvas");
    wincan.GetComponent<CanvasGroup>().alpha = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isRunning)
    {
      StartCoroutine(outrosequence());
    }
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
    {
      Application.LoadLevel("levelselect");
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

  public IEnumerator outrosequence()
  {
    isRunning = true;
    int num = 5;
    if (num == 5)
    {
      StartCoroutine(MoveOverTime(follower.transform, (follower.transform.forward * 800), 80));
      StartCoroutine(MoveOverTime(maincam.transform, (maincam.transform.up * 20), 10));
      StartCoroutine(MoveOverTime(shuttle.transform, (shuttle.transform.forward * 800), 70));
      yield return new WaitForSeconds(13f);
      num = 4;
    }
    if (num == 4)
    {
      maincam.position = new Vector3(1174, 62, -1018);
      maincam.LookAt(shuttle.transform);
      StartCoroutine(MoveOverTime(maincam.transform, (-maincam.transform.up * 20), 60));
      yield return new WaitForSeconds(4f);
      follower.transform.LookAt(maincam.transform);
      StartCoroutine(MoveOverTime(follower.transform, (follower.transform.forward * 900), 40));
      yield return new WaitForSeconds(7f);
      maincam.LookAt(follower.transform);
      num = 3;
    }
    if (num == 3)
    {
      yield return new WaitForSeconds(1f);

      num = 1;
    }
    if (num == 1)
    {
      wincan.GetComponent<CanvasGroup>().alpha = 1;
      yield return new WaitForSeconds(5f);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      Application.LoadLevel("levelselect");
    }
  }
}
