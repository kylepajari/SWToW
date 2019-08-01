using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
  public GameObject XPlayer;
  public GameObject APlayer;
  public GameObject YPlayer;
  public GameObject SPlayer;
  public GameObject FPlayer;

  public GameObject TPlayer;


  void Awake()
  {
    XPlayer = Globals.FindObject(gameObject, "XPlayer");
    APlayer = Globals.FindObject(gameObject, "APlayer");
    YPlayer = Globals.FindObject(gameObject, "YPlayer");
    SPlayer = Globals.FindObject(gameObject, "SPlayer");
    FPlayer = Globals.FindObject(gameObject, "FPlayer");
    TPlayer = Globals.FindObject(gameObject, "TPlayer");
    if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
    {
      Destroy(GameObject.Find("Main Camera"));
    }



    switch (Globals.ShipName)
    {
      case "XPlayer":
        XPlayer.SetActive(true);
        APlayer.SetActive(false);
        YPlayer.SetActive(false);
        SPlayer.SetActive(false);
        FPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(XPlayer);
        }
        break;
      case "APlayer":
        APlayer.SetActive(true);
        XPlayer.SetActive(false);
        YPlayer.SetActive(false);
        SPlayer.SetActive(false);
        FPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(APlayer);
        }
        break;
      case "YPlayer":
        YPlayer.SetActive(true);
        APlayer.SetActive(false);
        XPlayer.SetActive(false);
        SPlayer.SetActive(false);
        FPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(YPlayer);
        }
        break;
      case "SPlayer":
        SPlayer.SetActive(true);
        APlayer.SetActive(false);
        YPlayer.SetActive(false);
        XPlayer.SetActive(false);
        FPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(SPlayer);
        }
        break;
      case "FPlayer":
        FPlayer.SetActive(true);
        APlayer.SetActive(false);
        YPlayer.SetActive(false);
        SPlayer.SetActive(false);
        XPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(FPlayer);
        }
        break;
      case "TPlayer":
        TPlayer.SetActive(true);
        APlayer.SetActive(false);
        YPlayer.SetActive(false);
        SPlayer.SetActive(false);
        FPlayer.SetActive(false);
        XPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(TPlayer);
        }
        break;
      default:
        XPlayer.SetActive(true);
        APlayer.SetActive(false);
        YPlayer.SetActive(false);
        SPlayer.SetActive(false);
        FPlayer.SetActive(false);
        TPlayer.SetActive(false);
        if (!Application.loadedLevelName.Contains("Intro") && !Application.loadedLevelName.Contains("Outro"))
        {
          CreateCamera(XPlayer);
        }
        break;
    }
  }

  void CreateCamera(GameObject ship)
  {
    GameObject MainCamera = new GameObject();
    MainCamera.name = "Main Camera";
    MainCamera.tag = "MainCamera";
    MainCamera.AddComponent<Camera>();
    MainCamera.GetComponent<Camera>().farClipPlane = 3500;
    MainCamera.AddComponent<GUILayer>();
    MainCamera.AddComponent<FlareLayer>();
    MainCamera.AddComponent<AudioListener>();
    MainCamera.transform.parent = ship.transform;
    MainCamera.transform.rotation = ship.transform.rotation;
  }
}
