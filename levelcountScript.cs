using UnityEngine;
using System.Collections;

public class levelcountScript : MonoBehaviour {

    private static levelcountScript instance;
    public int levelcount;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
