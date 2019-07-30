using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hothScript : MonoBehaviour
{

  public GameObject levelCanvas;
  public float atatsDestroyed;
  public Text lvlText;

  public bool atat1Down;
  public bool atat2Down;
  public bool atat3Down;
  public bool havenotRun;

  public bool shieldDown;
  public GameObject Fade;

  // Use this for initialization
  void Start()
  {
    levelCanvas = GameObject.Find("LevelManager");
    levelCanvas.GetComponent<CanvasGroup>().alpha = 0;
    lvlText = levelCanvas.GetComponent<Text>();
    atatsDestroyed = 0;
    atat1Down = false;
    atat2Down = false;
    atat3Down = false;
    havenotRun = true;
    shieldDown = false;
    Fade = GameObject.Find("FadeToBlack");
  }

  // Update is called once per frame
  void Update()
  {

    if (atatsDestroyed == 1)
    {
      if (havenotRun)
      {
        havenotRun = false;
        atat1Down = true;
        lvlText.text = "Two more AT-ATs remaining!";
        StartCoroutine(goodjob());
      }

    }
    if (atatsDestroyed == 2)
    {
      if (!havenotRun)
      {
        havenotRun = true;
        atat2Down = true;
        lvlText.text = "One more AT-AT remaining!";
        StartCoroutine(goodjob());
      }
    }
    if (atatsDestroyed == 3)
    {
      if (havenotRun)
      {
        havenotRun = false;
        lvlText.text = "All AT-ATs Down!";
        atat3Down = true;
        StartCoroutine(goodjob());
      }
    }

    if (shieldDown)
    {
      StopCoroutine(goodjob());
      lvlText.text = "";
      StartCoroutine(missionFailed());
    }

  }

  public IEnumerator goodjob()
  {
    if (atatsDestroyed == 3)
    {
      yield return new WaitForSeconds(2f);
      StartCoroutine(FadeToBlack(1f, 1.0f));
      yield return new WaitForSeconds(2f);
      Application.LoadLevel("levelselect");
      //   Application.LoadLevel("level6GoodOutro");
    }
    else
    {
      levelCanvas.GetComponent<CanvasGroup>().alpha = 1;
      yield return new WaitForSeconds(2f);
      levelCanvas.GetComponent<CanvasGroup>().alpha = 0;
    }



  }
  public IEnumerator missionFailed()
  {
    lvlText.text = "Shield Generator has been Destroyed!";
    // Application.LoadLevel("level6BadOutro");
    levelCanvas.GetComponent<CanvasGroup>().alpha = 1;
    yield return new WaitForSeconds(2f);
    StartCoroutine(FadeToBlack(1f, 1.0f));
    yield return new WaitForSeconds(2f);
    Application.LoadLevel("levelselect");
  }

  IEnumerator FadeToBlack(float aValue, float aTime)
  {
    float alpha = Fade.GetComponent<CanvasGroup>().alpha;
    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
    {
      float newAlpha = t;
      Fade.GetComponent<CanvasGroup>().alpha = newAlpha;
      yield return null;
    }
  }

}
