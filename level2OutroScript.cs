using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level2OutroScript : MonoBehaviour
{

  public GameObject shuttle;
  public GameObject player;
  public Transform maincam;
  public GameObject wincan;
  public bool isRunning;

  // Use this for initialization
  void Start()
  {
    shuttle = GameObject.Find("ImpShuttle");
    player = GameObject.Find("UserForCutscenes");
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
      StartCoroutine(MoveOverTime(shuttle.transform, (shuttle.transform.forward * 240), 32));
      StartCoroutine(MoveOverTime(player.transform, (player.transform.forward * 240), 32));
      yield return new WaitForSeconds(3f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 0), 1f));
      yield return new WaitForSeconds(1f);
      num = 4;
    }
    if (num == 4)
    {
      maincam.position = new Vector3(135, 107, 66);
      maincam.rotation = Quaternion.Euler(0, 0, 0);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 0), 1f));
      yield return new WaitForSeconds(1f);
      num = 3;
    }
    if (num == 3)
    {
      maincam.LookAt(shuttle.transform);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 5), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, -5), 1f));
      yield return new WaitForSeconds(1);
      StartCoroutine(RotateOverTime(shuttle.transform, Quaternion.Euler(0, 180, 0), 1f));
      num = 2;
    }
    if (num == 2)
    {
      maincam.position = new Vector3(135, 107, 60);
      maincam.LookAt(shuttle.transform);
      yield return new WaitForSeconds(1f);
      num = 1;
    }
    if (num == 1)
    {
      wincan.GetComponent<CanvasGroup>().alpha = 1;
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(player.transform, Quaternion.Euler(0, 180, 180), 1f));
      yield return new WaitForSeconds(1f);
      StartCoroutine(RotateOverTime(player.transform, Quaternion.Euler(0, 180, 1), 1f));
      yield return new WaitForSeconds(8f);
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      Application.LoadLevel("levelselect");
    }
  }
}
