using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject[] CameraObj;
    [SerializeField] private bool[] KeepBorder; //Temporarily disable borders for follow camera segments.
    public Camera currentCamera;
    private Transform topRightLimit;
    private Transform PlayerPos;
    [SerializeField]private int CurrentCamNum = 0;
    [SerializeField]private int HighestCam = 0;
    [SerializeField] private float Offset;
    public ScreenBorder screenBorder;
    private Canvas BorderCanvas;
    public int StartingCam; //Use this to manually decide what cam is used to start the scene

    void Start()
    {
        //Find Player and the limiters.
        topRightLimit = GameObject.Find("TopRight").GetComponent<Transform>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        screenBorder = GameObject.FindGameObjectWithTag("Player").GetComponent<ScreenBorder>();
        BorderCanvas = GameObject.Find("ScreenBorder").GetComponent<Canvas>();

        // Gather all the cameras.
        HighestCam = CameraObj.Length-1;
        //SetCam(HighestCam); I don't know why this exists, but I added it for some reason
        SetCam(StartingCam);
    }

    void FixedUpdate()
    {
        if(PlayerPos.position.x >= topRightLimit.position.x - Offset && CurrentCamNum < HighestCam) //Progress to next camera
        {
            NextCam();//I'm going to replace this with external triggers, but offsetting seems like it would also be useful in the future
        }
    }
    void UpdateAllBorders()
    {
        screenBorder.UpdateBorder();
        topRightLimit = GameObject.Find("TopRight").GetComponent<Transform>();
    }
    void ResetCams()
    {
        foreach (GameObject Cam in CameraObj)
        {
            Cam.SetActive(false);
        }
    }
    public void SetCam(int Num)
    {
        ResetCams();
        Debug.Log("Camera is " + Num);
        CameraObj[Num].SetActive(true);
        currentCamera = CameraObj[Num].GetComponent<Camera>();
        CurrentCamNum = Num;
        BorderCanvas.worldCamera = currentCamera;
        if(KeepBorder[Num] == true)
        {
            screenBorder.enabled = true;
            UpdateAllBorders();
        }
        else
        {
            screenBorder.enabled = false;
        }
        
    }
    public void NextCam()
    {
        StartCoroutine(WaitForFrame());
    }
    IEnumerator WaitForFrame()
    {
        yield return new WaitForEndOfFrame();
        SetCam(CurrentCamNum+1);
        Debug.Log("Cam++");
        
    }
}
