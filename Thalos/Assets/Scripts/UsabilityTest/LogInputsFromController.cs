using UnityEngine;
using System.Collections;
using Controller;
using System.IO;
using System;




public class LogInputsFromController : MonoBehaviour {

    private string path;
    private string dataName;
    private string completePath_ButtonProtocol;
    private string completePath_RightSticProtocol;
    private string completePath_LeftSticProtocol;

    private bool isLeftSticMoving = false;
    private bool isRightSticMoving = false;

	void Update () {
        
        float Left_inputX = Input.GetAxis("Horizontal");
        float Left_inputY = Input.GetAxis("Vertical");
        float right_inputX = Input.GetAxis("RightStickX");
        float right_inputY = Input.GetAxis("RightStickY");

        checkDirectoryExists();
        checkFileExists();
        try
        {
            LogShootInput(right_inputX, right_inputY, Left_inputX, Left_inputY);
            CheckButtonInput();
            WriteInputLeft(Left_inputX, Left_inputY);
            WriteInputRight(right_inputX, right_inputY);

        }
        catch
        {

        }
        
	}
    
    void Start()
    {
        Gamestatemanager.ChangeInGameMenuHandler += logLTAndRTButtons;


        path = findPath();
        Debug.Log("Path: " + path);

        string date = System.DateTime.Now.ToString("h_mm_ss_tt");

        dataName = "T_ID" +date;
        completePath_ButtonProtocol = path + "/" + dataName+"_ButtonProtocol.txt";
        completePath_RightSticProtocol = path + "/" + dataName + "_RightSticProtocol.txt";
        completePath_LeftSticProtocol = path + "/" + dataName + "_LeftSticProtocol.txt";
        checkDirectoryExists();
        checkFileExists();

    }

    private void LogShootInput(float RightinputX, float RightinputY, float LeftinputX, float LeftinputY)
    {
        if (Mathf.Abs(RightinputX) > 0.1f && !isRightSticMoving)
        {
            isRightSticMoving = true;

            writeIntoFile("EVENT_STRIC_RIGHT_CHAMERA_START_MOVE");
        }

        else if(Mathf.Abs(RightinputX) < 0.1f && isRightSticMoving)
        {
            isRightSticMoving = false;

            writeIntoFile("EVENT_STRIC_RIGHT_CHAMERA_STOP_MOVE");
        }

        if (Mathf.Abs(RightinputY) > 0.1f && !isRightSticMoving)
        {
            isRightSticMoving = true;

            writeIntoFile("EVENT_STRIC_RIGHT_CHAMERA_START_MOVE");
        }

        else if (Mathf.Abs(RightinputX) < 0.1f && isRightSticMoving)
        {
            isRightSticMoving = false;

            writeIntoFile("EVENT_STRIC_RIGHT_CHAMERA_STOP_MOVE");
        }

        // Left Stic
        if (Mathf.Abs(LeftinputX) > 0.1f && !isLeftSticMoving)
        {
            isLeftSticMoving = true;

            writeIntoFile("EVENT_STRIC_LEFT_CHARACTER_START_MOVE");
        }

        else if (Mathf.Abs(LeftinputX) < 0.1f && isLeftSticMoving)
        {
            isLeftSticMoving = false;

            writeIntoFile("EVENT_STRIC_LEFT_CHARACTER_STOP_MOVE");
        }

        if (Mathf.Abs(LeftinputY) > 0.1f && !isLeftSticMoving)
        {
            isLeftSticMoving = true;
            writeIntoFile("EVENT_STRIC_LEFT_CHARACTER_START_MOVE");
        }

        else if (Mathf.Abs(LeftinputX) < 0.1f && isLeftSticMoving)
        {
            isLeftSticMoving = false;

            writeIntoFile("EVENT_STRIC_LEFT_CHARACTER_STOP_MOVE");
        }
    

    }


    private void WriteInputLeft(float inputX,float inputY)
    {
        using (StreamWriter file = new StreamWriter(completePath_LeftSticProtocol,true))
        {

            file.WriteLine(gazeModel.timeStamp + ": leftControlStic_X:" + inputX + "_Y:" + inputY);
        }
    }

    private void WriteInputRight(float inputX, float inputY)
    {
        using (StreamWriter file = new StreamWriter(completePath_RightSticProtocol, true))
        {
            file.WriteLine(gazeModel.timeStamp + ": rightControlStic" + inputX + "_Y:" + inputY);
        }
    }
    
    private void checkFileExists()
    {
        if(!File.Exists(completePath_ButtonProtocol))
        {
            Debug.Log("createFile");
            File.Create(completePath_ButtonProtocol).Close();
        }

        if (!File.Exists(completePath_LeftSticProtocol))
        {
            Debug.Log("createFile");
            File.Create(completePath_LeftSticProtocol).Close();
        }

        if (!File.Exists(completePath_RightSticProtocol))
        {
            Debug.Log("createFile");
            File.Create(completePath_RightSticProtocol).Close();
        }
    }

    private void writeIntoFile(string eventChar)
    {
        using (StreamWriter file = new StreamWriter(completePath_ButtonProtocol, true))
        {
            file.WriteLine(gazeModel.timeStamp+": "+eventChar);
        }
    }

    private bool checkDirectoryExists()
    {
        if(Directory.Exists(path))
        {

            Debug.Log("Repo exists");
            return true; 
        }

        else
        {
            Debug.Log("Create Repo");
            Directory.CreateDirectory(path);
        }

        return false;
    }

    private string findPath()
    {
        return Application.dataPath+"/"+Constants.USABILITY_FOLDERNAME;
    }

    private void logLTAndRTButtons(int IDMenu, bool status)
    {
        if (IDMenu == Constants.INGAMEMENU_CIRCLEMENU && status == true)
        {
            writeIntoFile("LT_EVENT_OPEN_CIRCLE_MENU");
        }
    }

    private void CheckButtonInput()
    {
        if (Input.GetButtonDown("ButtonY"))
        {
            writeIntoFile("ButtonY_Pressed_OPEN_IVENTORY");
        }

        else if (Input.GetButtonDown("ButtonX"))
        {

            writeIntoFile("ButtonX_Pressed_INTERACT");
        }

        else if (Input.GetButtonDown("ButtonA"))
        {

            writeIntoFile("ButtonA_Pressed_JUMP");
        }

        else if (Input.GetButtonDown("ButtonB"))
        {
            writeIntoFile("ButtonB_Pressed_BACKBUTTON");
        }


    }

    public void logShoot()
    {
        using (StreamWriter file = new StreamWriter(completePath_ButtonProtocol, true))
        {
            file.WriteLine(gazeModel.timeStamp + ": LT_EVENTSHOOT");
        }
    }

    public void logThrow()
    {
        using (StreamWriter file = new StreamWriter(completePath_ButtonProtocol, true))
        {
            file.WriteLine(gazeModel.timeStamp + ": LT_EVENTTHROW");
        }
    }
}
