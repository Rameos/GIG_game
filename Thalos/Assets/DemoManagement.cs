using UnityEngine;
using System.Collections;
using iViewX;
using Controller;
public class DemoManagement : MonoBehaviour {



    [SerializeField]
    private Texture2D gazeCursor;
    private Vector3 posGaze;


    public bool IsEyeDetected { get; set; }
    public bool IsGazeCursorVisible { get; set; }
    [SerializeField]
    private float maxTime = 2f;

    private bool isDwellTimeRunning = false;
    private float countTime = 0f;
    private float timeInterval = 0.1f;
    public bool isRotationSpeedVisible = false;
    public bool isKillingProcessActive = false;

    //private bool isCheatConsoleOpen = false;
    //private string cheat = "";


    //private const string cheat_AllFast = "AllFast";
    //private const string cheat_IceFast = "IceFast";
    //private const string cheat_FireFast = "FireFast";
    //private const string cheat_StandardFast = "StandardFast";

    //private const string cheat_AllSlow = "AllSlow";
    //private const string cheat_IceSlow = "IceSlow";
    //private const string cheat_FireSlow = "FireSlow";
    //private const string cheat_StandardSlow = "StandardSlow";

    //private const int TYPESTANDARD = 0;
    //private const int TYPEFIRE = 1;
    //private const int TYPEICE = 2;

    private float SpeedSlow;
    private float SpeedFast = 3;
	// Use this for initialization
	void Start () {

        SpeedSlow = Constants.BULLETSPEED_STANDARD;
        //setSpeed(TYPESTANDARD, SpeedFast);
        //cheat = cheat_AllFast;
	}
	
    void OnGUI()
    {
        if (IsGazeCursorVisible)
        {
            float offset = gazeCursor.width * 0.25f;
            posGaze = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            GUI.DrawTexture(new Rect(posGaze.x-offset, posGaze.y - offset, gazeCursor.width * 0.5f, gazeCursor.height * 0.5f), gazeCursor);
        }

        //if(isCheatConsoleOpen)
        //{
        //    cheat = GUI.TextArea(new Rect(200, 20, 500, 20),cheat);
        //    if(GUI.Button( new Rect(200,50,100,100),"Activate Cheat"))
        //    {

        //        switch (cheat)
        //        {
        //            case cheat_AllFast:

        //                Debug.Log("ALLFAST!!");
        //                setSpeed(TYPESTANDARD, SpeedFast);
        //                setSpeed(TYPEFIRE, SpeedFast);
        //                setSpeed(TYPEICE, SpeedFast);
        //                break;

        //            case cheat_AllSlow:
        //                setSpeed(TYPESTANDARD, SpeedSlow);
        //                setSpeed(TYPEFIRE, SpeedSlow);
        //                setSpeed(TYPEICE, SpeedSlow);
        //                break;

        //            case cheat_FireFast:
        //                setSpeed(TYPEFIRE, SpeedFast);
        //                break;

        //            case cheat_FireSlow:
        //                setSpeed(TYPEFIRE, SpeedSlow);
        //                break;

        //            case cheat_IceFast:
        //                setSpeed(TYPEICE, SpeedFast);
        //                break;

        //            case cheat_IceSlow:
        //                setSpeed(TYPEICE, SpeedSlow);
        //                break;

        //            case cheat_StandardFast:
        //                setSpeed(TYPESTANDARD, SpeedFast);
        //                break;

        //            case cheat_StandardSlow:
        //                setSpeed(TYPESTANDARD, SpeedSlow);
        //                break;
        //        }
        //    }
        //}
    }


    //private void setSpeed(int type,float speed)
    //{
    //    switch (type)
    //    {
    //        case TYPESTANDARD:
    //            Constants.BULLETSPEED_STANDARD = speed;
    //            break;

    //        case TYPEFIRE:
    //            Constants.BULLETSPEED_FIRE = speed;
    //            break;
            
    //        case TYPEICE:
    //            Constants.BULLETSPEED_ICE= speed;
    //            break;
    //    }

    //}
	
    void Update () {
	#region keyManager

        //if(Input.GetKeyDown(KeyCode.Return))
        //{
        //    if (isCheatConsoleOpen)
        //    {
        //        isCheatConsoleOpen = false;
        //    }
        //    else
        //    {
        //        isCheatConsoleOpen = true;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isRotationSpeedVisible)
            {
                UnityEngine.Debug.Log("Hide isRotationSpeedVisible");
                isRotationSpeedVisible = false;
            }
            else
            {
                UnityEngine.Debug.Log("SetVisible isRotationSpeedVisible");
                isRotationSpeedVisible = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.Debug.Log("Start 2 point Calibration");
            GazeControlComponent.Instance.StartCalibration(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UnityEngine.Debug.Log("Start 5 Point Calibration");
            GazeControlComponent.Instance.StartCalibration(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            UnityEngine.Debug.Log("Start 9 Point Calibration");
            GazeControlComponent.Instance.StartCalibration(9);
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {

            UnityEngine.Debug.Log("Restart Game");
            //ToDo: fadeOut Animation
            Application.LoadLevel(Application.loadedLevel);
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            if (IsGazeCursorVisible)
            {
                UnityEngine.Debug.Log("Hide GazeCursor");
                IsGazeCursorVisible = false;
            }
            else
            {
                UnityEngine.Debug.Log("SetVisible GazeCursor");
                IsGazeCursorVisible = true;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GazeControlComponent.Instance.Disconnect();
            Application.Quit();
        }
        #endregion


        #region EyeDetection

        if (gazeModel.diamRightEye == 0 || gazeModel.diamLeftEye == 0)
        {
            if (!isDwellTimeRunning)
            {
                InvokeRepeating("updateCounter", 0f, timeInterval);
                isDwellTimeRunning = true;
            }
        }
        else
        {
            gazeModel.isEyeDetected = true;
            isDwellTimeRunning = false;
            CancelInvoke("updateCounter");
            countTime = 0;
        }

        #endregion
    }

    private void updateCounter()
    {
        if (countTime >= maxTime)
        {

            gazeModel.isEyeDetected = false;
            CancelInvoke("updateCounter");
        }
        else
        {
            countTime += timeInterval;
            UnityEngine.Debug.Log("CountTime:" + countTime);
        }
    }
}
