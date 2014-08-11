using UnityEngine;
using System.Collections;
using iViewX;
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

	// Use this for initialization
	void Start () {
	
	}
	
    void OnGUI()
    {
        if (IsGazeCursorVisible)
        {
            posGaze = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            GUI.DrawTexture(new Rect(posGaze.x, posGaze.y, gazeCursor.width * 0.5f, gazeCursor.height * 0.5f), gazeCursor);
        }
    }
	// Update is called once per frame
	void Update () {
	#region keyManager


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

            gazeModel.isEyeDetected = true;
            CancelInvoke("updateCounter");
        }
        else
        {
            countTime += timeInterval;
            UnityEngine.Debug.Log("CountTime:" + countTime);
        }
    }
}
