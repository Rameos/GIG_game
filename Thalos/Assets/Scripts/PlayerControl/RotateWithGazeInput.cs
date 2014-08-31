﻿using UnityEngine;
using System.Collections;
using Controller;

public class RotateWithGazeInput : MonoBehaviour {

    private float rotationDifference;
    private AOI leftAOI;
    private AOI rightAOI;
    public float threshold = 0.1f;
    public float speedUp = 1.2f;

    public float rotationFactor;
    public Texture2D testTexture_Right;
    public Texture2D testTexture_Left;
   
    public float sizeWidthAOI;
   
    public GameObject player;
    public ThirdPersonCamera camScript;
    public gazeActionMenu actualStateGazeAction;

    public bool isInGameMenu = false;
    public bool IsEyeDetected = true;
    public float offSetRightAOI;


    [SerializeField]
    private bool isVisualisationActive = false;

    [SerializeField]
    private bool isAlwaysEnable = true;
    public enum gazeActionMenu
    {
        onlyCam, 
        onlyPlayer,
        camAndPlayer,
        NONE
    }

    void OnGUI()
    {

        if (isVisualisationActive)
        {
            GUI.DrawTexture(leftAOI.volume, testTexture_Right);
            GUI.DrawTexture(rightAOI.volume, testTexture_Left);
        }
        
    }

    void Start()
    {
        Gamestatemanager.ChangeInGameMenuHandler +=Gamestatemanager_ChangeInGameMenuHandler;
        camScript = gameObject.GetComponent<ThirdPersonCamera>();
        
        calculateAOI();
    }

    private void Gamestatemanager_ChangeInGameMenuHandler(int ID_Menu, bool status)
    {
        isInGameMenu = status;
    }


    void Update () 
    {
        if(!isInGameMenu)
        {
            calculateAOI();
            checkGazeIsInAOI();
        }
        else
        {
            camScript.gazeInput = 0;
        }
	}
    
    private void checkGazeIsInAOI()
    {

            Vector3 gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            float rightX = -Input.GetAxis("RightStickX");
            float rightY = -Input.GetAxis("RightStickY");
            

            if (gazePos != Vector3.zero && !isInGameMenu)
            {

                #region Left
                //LeftPosition
                if (leftAOI.volume.Contains(gazePos))
                {

                    if (Mathf.Abs(rightX) < threshold)
                    {

                        float speed = Mathf.Abs((leftAOI.volume.width - gazePos.x) / leftAOI.volume.width) * rotationFactor;

                        camScript.gazeInput = speed * speedUp;
                    }
                }
                #endregion

                #region right
                //RightPosition
                else if (rightAOI.volume.Contains(gazePos))
                {
                    float speed = Mathf.Abs(((rightAOI.volume.width - gazePos.x) + offSetRightAOI) / rightAOI.volume.width) * rotationFactor;

                    camScript.gazeInput = -speed * speedUp;
                }
                #endregion
                
                else
                {
                    camScript.gazeInput = 0;
                }
            }
            else if(isInGameMenu)
            {
                camScript.gazeInput = 0;
            }
    }
    
  
    private void calculateAOI()
    {
        leftAOI.volume = new Rect(0,0,Screen.width*sizeWidthAOI,Screen.height);
        leftAOI.startPoint = Vector3.zero;
        leftAOI.endPoint = new Vector3(Screen.width * sizeWidthAOI, 0, 0);

        rightAOI.volume = new Rect(Screen.width-Screen.width*sizeWidthAOI, 0, Screen.width * sizeWidthAOI, Screen.height);
        rightAOI.endPoint = new Vector3(Screen.width, 0, 0);
        rightAOI.startPoint = new Vector3(Screen.width - Screen.width * sizeWidthAOI, 0, 0);


        offSetRightAOI = rightAOI.startPoint.x - leftAOI.endPoint.x;

    }
}
public struct AOI
{
    public Rect volume;
    public Vector3 startPoint;
    public Vector3 endPoint;
}