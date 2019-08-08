using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level3OutroScript : MonoBehaviour
{

  public GameObject corvette;
  public GameObject player;
  public Transform maincam;
  public GameObject wincan;
  public bool isRunning;

  // Use this for initialization
  void Start()
  {
    corvette = GameObject.Find("corvette");
    player = GameObject.Find("UserForCutscenes");
    maincam = GameObject.Find("Main Camera").transform;
    wincan = GameObject.Find("Canvas");
    wincan.GetComponent<CanvasGroup>().alpha = 0;
    Globals.Fade = GameObject.Find("FadeToBlack");
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
      StartCoroutine(MoveOverTime(player.transform, (player.transform.forward * 800), 80));
      StartCoroutine(MoveOverTime(maincam.transform, (maincam.transform.up * 20), 10));
      StartCoroutine(MoveOverTime(corvette.transform, (corvette.transform.forward * 800), 70));
      yield return new WaitForSeconds(13f);
      num = 4;
    }
    if (num == 4)
    {
      maincam.position = new Vector3(1174, 62, -1018);
      maincam.LookAt(corvette.transform);
      StartCoroutine(MoveOverTime(maincam.transform, (-maincam.transform.up * 20), 60));
      yield return new WaitForSeconds(4f);
      player.transform.LookAt(maincam.transform);
      StartCoroutine(MoveOverTime(player.transform, (player.transform.forward * 900), 40));
      yield return new WaitForSeconds(7f);
      maincam.LookAt(player.transform);
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
      yield return new WaitForSeconds(4f);
      StartCoroutine(Globals.FadeToBlack(1f, 1.0f));
      yield return new WaitForSeconds(1f);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      Application.LoadLevel("levelselect");
    }
  }
}
