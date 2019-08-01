using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

  public static int pitchInverted = 0;

  public static GameObject Fade;

  public static int levelCount;

  public static string ShipName = "";

  public static IEnumerator FadeToBlack(float aValue, float aTime)
  {
    float alpha = Fade.GetComponent<CanvasGroup>().alpha;
    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    {
      float newAlpha = t;
      Fade.GetComponent<CanvasGroup>().alpha = newAlpha;
      yield return null;
    }
  }

  public static IEnumerator MoveOverTime(Transform theTransform, Vector3 d, float t)
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

  public static IEnumerator RotateOverTime(Transform theTransform, Quaternion d, float t)
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
  public static GameObject FindObject(GameObject parent, string name)
  {
    Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
    foreach (Transform t in trs)
    {
      if (t.name == name)
      {
        return t.gameObject;
      }
    }
    return null;
  }
}
