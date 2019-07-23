using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class level3tieGood : MonoBehaviour {

    public GameObject shuttle;
    public GameObject follower1;
    public GameObject follower2;
    public Transform maincam;
    public GameObject wincan;
    public bool isRunning;

    // Use this for initialization
    void Start()
    {
        shuttle = GameObject.Find("shuttle");
        follower1 = GameObject.Find("TieParent");
        follower2 = GameObject.Find("TieParent (1)");
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
            StartCoroutine(MoveOverTime(shuttle.transform, (shuttle.transform.forward * 240), 30));
            StartCoroutine(MoveOverTime(follower1.transform, (follower1.transform.forward * 240), 30));
            StartCoroutine(MoveOverTime(follower2.transform, (follower2.transform.forward * 240), 30));
            yield return new WaitForSeconds(6f);
            num = 4;
        }
        if (num == 4)
        {
            maincam.position = new Vector3(135,107,66);
            maincam.rotation = Quaternion.Euler(0,0,0);
            yield return new WaitForSeconds(4.2f);
            wincan.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(4f);
            num = 1;
        }
        if (num == 1)
        {
            maincam.LookAt(shuttle.transform);
            wincan.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(8f);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Application.LoadLevel("levelselect");
        }
    }
}
