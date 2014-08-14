﻿using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using iViewX;

namespace iViewX
{
    public class GazeController
    {
        private int sampleCount = 3;
        private int actualCount = 0;

        private List<Vector2> samplesLeftGaze = new List<Vector2>();
        private List<Vector2> samplesRightGaze = new List<Vector2>();

        private bool isFilterEnable = false;

        //Sample Data
        private EyeTrackingController ET_Device;
        private EyeTrackingController.AccuracyStruct m_AccuracyData;
        private EyeTrackingController.SampleStruct m_SampleData;
        private EyeTrackingController.SystemInfoStruct m_SystemData;
        private EyeTrackingController.CalibrationStruct m_CalibrationData;
        //private EyeTrackingController.Trackm_TrackingStatusData; 
        
        //Sample callBack 
        private GetSampleCallBack m_samplecallBack;
        private delegate void GetSampleCallBack(EyeTrackingController.SampleStruct sampleData);

        private GetSystemInfoCallBack m_systemInfoCallBack;
        private delegate void GetSystemInfoCallBack(EyeTrackingController.SystemInfoStruct systemInfoData);

        /// <summary>
        /// Constructor: Init the Controller_MyGaze and set the SampleDataCallback.
        /// </summary>
        public GazeController()
        {
            ET_Device = new EyeTrackingController();

            //CallbackDefinition
            m_samplecallBack = new GetSampleCallBack(GetSampleCallbackFunction);
            ET_Device.iV_SetSampleCallback(m_samplecallBack);

        }

        /// <summary>
        /// init the Eye Tracking thread.
        /// </summary>
        public void initEyeThread()
        {
            int errorID = connectToETDevice();


            //ErrorMessage
            getLogdata(errorID, errorIDContainer.STATE_CONNECT);
        }



        public void getCalibrationStatus()
        {/*/
            int errorID = ET_Device.iV_GetCalibrationStatus(ref m_calibrationStatusData);
            ET_Device.iV_GetTrackingStatus(ref m_TrackingStatusData);

            Debug.Log("Enum:" + m_calibrationStatusData.ToString());

            getLogdata(errorID, errorIDContainer.STATE_CALIBRATE);
          */
        }

        /// <summary>
        /// Disconnect from the Eye Tracker Server
        /// </summary>
        public void finish()
        {
            ET_Device.iV_SetCalibrationCallback(null);

            int errorID = disconnetFromETDevice();

            //ErrorMessage
            getLogdata(errorID, errorIDContainer.STATE_DISCONNET);
        }

        /// <summary>
        /// Load calibrationData from the Server
        /// </summary>
        /// <param name="id_Calibration"></param>
        public void loadCalibration(string id_Calibration)
        {
            int errorID = ET_Device.iV_LoadCalibration(new System.Text.StringBuilder(id_Calibration));
            
            //ErrorMessage
            getLogdata(errorID, errorIDContainer.STATE_CALIBRATE);
        }

        /// <summary>
        /// Start a Calibration from the Server
        /// </summary>
        public void startCalibration()
        {
            int errorID = 0;

            //Load CalibrationSetup From the Eye Tracking Server
            //errorID = ET_Device.iV_GetCalibrationParameter(ref m_CalibrationData);

            int displayDevice = 0;
            int calibrationPoints = gazeModel.calibrationPoints;
            int targetSize = 20;


            m_CalibrationData.displayDevice = displayDevice;
            m_CalibrationData.autoAccept = 2;
            m_CalibrationData.method = calibrationPoints;
            m_CalibrationData.visualization = 1;
            m_CalibrationData.speed = 0;
            m_CalibrationData.targetShape = 2;
            m_CalibrationData.backgroundColor = 0;
            m_CalibrationData.foregroundColor = 250;
            m_CalibrationData.targetSize = targetSize;
            m_CalibrationData.targetFilename = "";


            errorID = ET_Device.iV_SetupCalibration(ref m_CalibrationData);
            Debug.Log("CalibrationSetup:/n DisplayDevice:" + m_CalibrationData.displayDevice+" Method: "+m_CalibrationData.method+"Calib To string: " +m_CalibrationData.ToString());
            //ErrorMessage
            getLogdata(errorID, errorIDContainer.STATE_CALIBRATE);


            //Start the calibration
           errorID = ET_Device.iV_Calibrate();

            //ErrorMessage
           getLogdata(errorID, errorIDContainer.STATE_CALIBRATE);

        }

        /// <summary>
        /// Start a Validation from the Server
        /// </summary>
        public void startValidation()
        {
            int errorID = 0;

            //Start the validation
            errorID = ET_Device.iV_Validate();
            getLogdata(errorID, errorIDContainer.STATE_VALIDATE);

            //Show the ValidationScreen
            errorID = ET_Device.iV_ShowAccuracyMonitor();
            getLogdata(errorID, errorIDContainer.STATE_VALIDATE);

        }

        /// <summary>
        /// Pause Eye Tracking
        /// </summary>
        public void pauseEyeTracking()
        {
            int errorID = 0;
            errorID = ET_Device.iV_PauseEyetracking();
            getLogdata(errorID, errorIDContainer.STATE_PAUSE);
        }

        /// <summary>
        /// Continue Eye Tracking
        /// </summary>
        public void continueEyeTracking()
        {
            int errorID = 0;
            errorID = ET_Device.iV_ContinueEyetracking();
            getLogdata(errorID, errorIDContainer.STATE_CONTINUE);
        }

        public void enableGazeFilter()
        {
            int errorID = ET_Device.iV_EnableGazeDataFilter();
            getLogdata(errorID, errorIDContainer.STATE_GAZEFILTER);
        }

        public void disableGazeFilter()
        {
            int errorID = ET_Device.iV_DisableGazeDataFilter();
            getLogdata(errorID, errorIDContainer.STATE_GAZEFILTER);
        }

        /// <summary>
        /// Connect to the Eye Tracking Server.
        /// </summary>
        private int connectToETDevice()
        {
            int errorID = 0;
            try
            {
                errorID = ET_Device.iV_ConnectLocal();
            }
            catch (System.Exception e)
            {
                errorID = errorIDContainer.ACTION_CRITICALERROR;
                Debug.Log(e.Message);
            }

            if (errorID == errorIDContainer.ACTION_COMPLETE)
                gazeModel.isEyeTrackerRunning = true;

            return errorID;
        }

        /// <summary>
        /// Disconnet from the Eye Tracking Server
        /// (Important Note: You must disconnect your Application from the server to avoid critical crashed and problems with the Portsettings)
        /// </summary>
        private int disconnetFromETDevice()
        {
            int errorID = 0;
            try
            {
                errorID = ET_Device.iV_Disconnect();

            }

            catch (System.Exception e)
            {
                errorID = errorIDContainer.ACTION_CRITICALERROR;
                Debug.LogError(e.Message);
            }

            if (errorID == errorIDContainer.ACTION_COMPLETE)
                gazeModel.isEyeTrackerRunning = false;

            return errorID;
        }

        /// <summary>
        /// Write the SampleData into the gazeModel
        /// </summary>
        /// <param name="sampleData"></param>
        private void GetSampleCallbackFunction(EyeTrackingController.SampleStruct sampleData)
        {
            gazeModel.diamLeftEye = (float)sampleData.leftEye.diam;
            gazeModel.diamRightEye = (float)sampleData.leftEye.diam;
            
            gazeModel.posLeftEye = new Vector3((float)sampleData.leftEye.eyePositionX, (float)sampleData.leftEye.eyePositionY, (float)sampleData.leftEye.eyePositionZ);
            gazeModel.posRightEye = new Vector3((float)sampleData.rightEye.eyePositionX, (float)sampleData.rightEye.eyePositionY, (float)sampleData.rightEye.eyePositionZ);

            //GazePosition
            if (isFilterEnable)
            {
                if (actualCount < sampleCount)
                {
                    samplesLeftGaze.Add(new Vector2((float)sampleData.leftEye.gazeX, (float)sampleData.leftEye.gazeY));
                    samplesRightGaze.Add(new Vector2((float)sampleData.rightEye.gazeX, (float)sampleData.rightEye.gazeY));
                    actualCount++;
                }

                else
                {
                    Vector2 finalLeftGaze = Vector3.zero;
                    Vector2 finalRightGaze = Vector3.zero;

                    foreach (Vector2 sample in samplesLeftGaze)
                    {
                        finalLeftGaze += sample;
                    }

                    foreach (Vector2 sample in samplesRightGaze)
                    {
                        finalRightGaze += sample;
                    }

                    finalLeftGaze /= 3;
                    finalRightGaze /= 3;

                    gazeModel.posGazeLeft = finalLeftGaze;
                    gazeModel.posGazeRight = finalRightGaze;

                    samplesLeftGaze.Clear();
                    samplesRightGaze.Clear();
                    actualCount = 0;


#if UNITY_EDITOR
                    Rect gameView = getOffsetFromGameView();
                    Vector2 posGazeLeft = new Vector3(gazeModel.posGazeLeft.x, gazeModel.posGazeLeft.y - gameView.y);
                    Vector2 posGazeRight = new Vector3(gazeModel.posGazeRight.x, gazeModel.posGazeRight.y - gameView.y);

                    gazeModel.posGazeLeft = posGazeLeft;
                    gazeModel.posGazeRight = posGazeRight;

#else
            Vector2 offSet = Win32HelperClass.GetGameViewPosition();
            if (offSet.x >= 0)
            {
                gazeModel.posGazeLeft = gazeModel.posGazeLeft - offSet;
                gazeModel.posGazeRight = gazeModel.posGazeRight - offSet;
            }
            else
            {
                gazeModel.posGazeLeft = offSet -gazeModel.posGazeLeft;
                gazeModel.posGazeRight = offSet - gazeModel.posGazeRight;
            }

#endif
                }
            }

            else
            {

                Vector2 sampleLeft = new Vector2((float)sampleData.leftEye.gazeX, (float)sampleData.leftEye.gazeY);
                Vector2 sampleRight = new Vector2((float)sampleData.rightEye.gazeX, (float)sampleData.rightEye.gazeY);
                
                Debug.Log("Pos left: " + gazeModel.posGazeLeft);
                Debug.Log("Pos right: " + gazeModel.posGazeRight);

                //LEFT == ZERO
                if (sampleLeft == Vector2.zero && sampleRight != Vector2.zero)
                {
                    Debug.Log("Left is Zero!");
                    gazeModel.posGazeLeft = sampleRight;
                    gazeModel.posGazeRight = sampleRight;
                }
                
                //RIGHT == ZERO
                else if (sampleRight == Vector2.zero && sampleLeft != Vector2.zero)
                {
                    Debug.Log("Right is Zero!");
                    gazeModel.posGazeRight = sampleLeft;
                    gazeModel.posGazeLeft = sampleLeft;
                }

                else
                {
                    Debug.Log("Both Eyes Detected!");
                    gazeModel.posGazeLeft = sampleLeft;
                    gazeModel.posGazeRight = sampleRight;

                }
            }

            /*Left Eye
            gazeModel.posGazeLeft = new Vector2((float)sampleData.leftEye.gazeX, (float)sampleData.leftEye.gazeY);
            
            //Right Eye
            gazeModel.posGazeRight = new Vector2((float)sampleData.rightEye.gazeX, (float)sampleData.rightEye.gazeY);

            //PupilData

            */

            gazeModel.gameScreenPosition = Win32HelperClass.GetGameViewPosition();

            //Head Position
            gazeModel.posHead = (gazeModel.posLeftEye + gazeModel.posRightEye) * 0.5f;

            //TimeStamp of the Sample
            gazeModel.timeStamp = sampleData.timestamp;

        }

        /// <summary>
        /// Write the SystemInfoDataStruct into the gazeModel
        /// </summary>
        /// <param name="systemInfoData"></param>
        private void GetSystemInfoCallbackFuntion(EyeTrackingController.SystemInfoStruct systemInfoData)
        {
            gazeModel.systemInfo = systemInfoData;
        }

        /// <summary>
        /// Print Logoutput from the Functions in the GazeController_MyGaze
        /// </summary>
        /// <param name="errorID"></param>
        /// <param name="state"></param>
        private void getLogdata(int errorID, int state)
        {
            if (errorID >errorIDContainer.ACTION_COMPLETE)
                Debug.LogError("Error by " + errorIDContainer.getState(state) + ": " + errorIDContainer.getErrorMessage(errorID));
            else
                Debug.LogError(errorIDContainer.getState(state) + " finished");
        }

#if UNITY_EDITOR
        private Rect getOffsetFromGameView()
        {
            var unityEditorType = Type.GetType("UnityEditor.GameView,UnityEditor");
            var getMainGameViewMethod = unityEditorType.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            UnityEditor.EditorWindow result = getMainGameViewMethod.Invoke(null, null) as UnityEditor.EditorWindow;
            return result.position;
        }
#endif
    }
}