using UnityEngine;
using System.Collections;

public class level3introscript : MonoBehaviour {

    public GameObject shuttle;
    public GameObject tie1;
    public GameObject tie2;
    public bool isRunning;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        shuttle = GameObject.Find("shuttle");
        tie1 = GameObject.Find("TieParent");
        tie2 = GameObject.Find("TieParent (1)");
        GameObject gm = GameObject.Find("GameManager");

    }
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(shuttle.transform);
        if (!isRunning)
        {
            StartCoroutine(introsequence());
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

    public IEnumerator introsequence()
    {
        isRunning = true;
        int num = 5;
        if (num == 5)
        {
            StartCoroutine(MoveOverTime(shuttle.transform, (-shuttle.transform.up * 30), 4));
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveOverTime(tie1.transform, (-tie1.transform.up * 30), 4));
            StartCoroutine(MoveOverTime(tie2.transform, (-tie2.transform.up * 30), 4));
            yield return new WaitForSeconds(3.5f);
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
            yield return new WaitForSeconds(8f);
            num = 2;
        }
        if (num == 2)
        {

            Application.LoadLevel("vehicleselect");
        }
    }

}
