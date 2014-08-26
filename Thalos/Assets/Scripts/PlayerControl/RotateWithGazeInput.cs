﻿using UnityEngine;
using System.Collections;

public class RotateWithGazeInput : MonoBehaviour {

    private float rotationDifference;
    private AOI leftAOI;
    private AOI rightAOI;
    public float threshold = 0.1f;
    public float speedUp = 1.2f;

    public float rotationFactor;
    public Texture2D testTexture_Right;
    public Texture2D testTexture_Left;
    public Texture2D noGazeInput;
    public float sizeWidthAOI;
   
    public GameObject player;
    public ThirdPersonCamera camScript;
    public gazeActionMenu actualStateGazeAction;

    private bool isInGameMenu = false;
    public bool IsEyeDetected = true;
    public float offSetRightAOI;

    public bool isActive = false;
    
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
         GUI.Label(new Rect(25, 5, 250, 60), "Rotationspeed: " + rotationFactor);
            rotationFactor = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), rotationFactor, 0.0f, 1.0f);
            Mathf.Round( rotationFactor * 100 / 100);


        if (isVisualisationActive)
        {
            GUI.DrawTexture(leftAOI.volume, testTexture_Right);
            GUI.DrawTexture(rightAOI.volume, testTexture_Left);
        }
        
    }

    void Start()
    {

        camScript = gameObject.GetComponent<ThirdPersonCamera>();
        calculateAOI();
    }


    void Update () 
    {
        calculateAOI();
        isActive = checkInput();
        checkGazeIsInAOI();

	}

    private bool checkInput()
    {
        if(!gazeModel.isEyeTrackerRunning)
        {

            IsEyeDetected = false; 
        }
        else
        {
            Vector3 gazeInput = gazeModel.posGazeLeft;
            if (gazeInput == Vector3.zero)
            {
                IsEyeDetected = false; 
            }
            else
            {
                IsEyeDetected = true; 
            }
        }

        if (Input.GetAxis("Horizontal") > threshold || Input.GetAxis("Vertical") > threshold || isAlwaysEnable)
        {
            return true;
        }
        return false; 
    }

    public void OpenGazeMenu (bool status)
    {
        isInGameMenu = status;
    }
    
    private void checkGazeIsInAOI()
    {
        if (isActive)
        {
            Vector3 gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            float rightX = -Input.GetAxis("RightStickX");
            float rightY = -Input.GetAxis("RightStickY");
            
            float leftX = Input.GetAxis("Horizontal");
            float leftY = Input.GetAxis("Vertical");

            if (gazePos != Vector3.zero && !isInGameMenu)
            {

                #region Left
                //LeftPosition
                if (leftAOI.volume.Contains(gazePos))
                {

                    if (Mathf.Abs(rightX) < threshold)
                    {

                        float speed = Mathf.Abs((leftAOI.volume.width - gazePos.x) / leftAOI.volume.width) * rotationFactor;

                        if (speed > 1)
                            speed = 0.45f;

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
        else
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