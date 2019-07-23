using UnityEngine;
using System.Collections;

public class LaserDestoryScript : MonoBehaviour {


	void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.name == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}
