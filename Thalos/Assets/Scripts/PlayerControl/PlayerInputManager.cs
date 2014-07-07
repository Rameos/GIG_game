﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using Controller; 

public class PlayerInputManager : MonoBehaviour {

    private float speed = 0.1f;
    private Camera gameCam;
    private float rotationDegreePerSecon = 120f;
    private float directionSpeed = 1.5f;
    private float directionDampTime = 0.25f;
    private float speedDampTime = 0.05f;
    private float focDampTime = 3f;
    private float jumpMultiplier = 1f;
    private CapsuleCollider capCollider;

    private Vector3 moveDirection;
    private Vector3 jumpDirection;

    private float jumpPower; 

    [SerializeField]
    private float distanceMax =1f;  
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speedFactor = 0.25f;
    [SerializeField]
    private float thresholdTriggers = 0.1f;
    [SerializeField]
    private float thresholdStics = 0.1f; 

    [SerializeField]
    private float jumpForcePower = 5f;
    [SerializeField]
    private bool isJumping = false;
    [SerializeField]
    private bool isInAir = false;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private Transform centerOfMass;
    [SerializeField]
    private float stepRotation = 5;


    float inputX;
    float inputY;


    //Test: 
    private float jumpVelocity = 2f;
    private float gravity = 9.81f;
    private float distance = 2.4f;



    //ControllerInput
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    //ExternalScripts
    JumpwithGaze jumpScript;
    private int y; 


	void Start () {
        centerOfMass = transform.FindChild("CenterOfMass");
        animator = GetComponent<Animator>();
        findGameController();
        Gamestatemanager.RumbleEventHandler += startRumbleForTime;
        jumpScript = gameObject.GetComponent<JumpwithGaze>();
        capCollider = gameObject.GetComponent<CapsuleCollider>();
	}

    void Update()
    {
        connectController();
    }

	void FixedUpdate () {

       inputX = Input.GetAxis("Horizontal");
       inputY = Input.GetAxis("Vertical");

       checkIsGrounded();
       checkButtonInput();
       checkGazeMenuStatus();
       checkshootInput();
       move(inputX, inputY);
	}

    public void startRumbleForTime(float rumbleHeavy, float rumbleWeak,float time)
    {
        StartCoroutine(rumbleOverTime(Time.deltaTime, 1, 1));
            
    }

    private void connectController()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);

                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
    
    private bool findGameController()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex testIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testIndex);
                if(testState.IsConnected)
                {
                    playerIndex = testIndex;
                    playerIndexSet = true;
                    prevState = state;
                    state = GamePad.GetState(playerIndex);
                    return true; 
                }
            }
        }
        return false;
    }

    private void checkIsGrounded()
    {
        Debug.Log(capCollider.height);
        Ray rayinput = new Ray(centerOfMass.position, -transform.up);
        Debug.DrawRay(transform.position, -transform.up,Color.red,1);
        if (Physics.Raycast(rayinput, capCollider.height / 2))
        {
            isGrounded = true;
            isJumping = false;
            isInAir = false;
        }
        else if (!isInAir)
        {
            isGrounded = false;
            isInAir = true;
            //Set JumpDirection
        }
    }

    private void jump()
    {
        if(isGrounded)
        {
            if (!jumpScript.isActive)
            {
                rigidbody.velocity = new Vector3(0, jumpForcePower, 0);
                if (Mathf.Abs(inputX) > thresholdStics || Mathf.Abs(inputY) > thresholdStics)
                    rigidbody.velocity += transform.forward * jumpForcePower;
            }

            else
            {

                Vector3 destinationPoint = jumpScript.getDestinationpoint();
                Debug.Log("DestinationPoint: " + destinationPoint);
                Debug.DrawRay(transform.position, destinationPoint, Color.magenta,2f);

                destinationPoint = destinationPoint - transform.position;

                destinationPoint.y = 0;

                Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
                transform.rotation = lookAtRotation;//Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);
                
                rigidbody.velocity = new Vector3(0, jumpForcePower, 0);
                rigidbody.velocity += transform.forward * jumpForcePower;

                //if (Mathf.Abs(inputX) > thresholdStics || Mathf.Abs(inputY) > thresholdStics)
                   
            
            }
            //animator.SetTrigger("Jump");
            //rigidbody.velocity.y = 20;
            //rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Force);
        }
    }

    private void move(float inputX, float inputY)
    {

        float angle = 0f;
        float speedOut = 0f;
        StickInputToWorld(inputX, inputY, ref angle, ref speedOut);

        Debug.DrawLine(transform.position, transform.position + transform.forward * 2, Color.green);

        if (Mathf.Abs(speedOut) > 0.1f)
        {

            if (isGrounded)
            {
                moveDirection = (transform.forward * speedFactor * speedOut) * Time.deltaTime;

                transform.position += moveDirection;
                transform.Rotate(0, angle, 0);

            }
            else
            {
                /*
                Debug.Log("Movement: " + jumpDirection);

                moveDirection = (transform.forward * jumpSpeed) * Time.deltaTime;

                transform.position += moveDirection;*/
            }

        }

        // Setup the Animator Parameters
        animator.SetFloat("Speed", speedOut);
        
    }
    
    private void checkshootInput()
    {
        if (Input.GetAxis("Triggers") < 0-thresholdTriggers)
        {
            animator.SetBool("Throw",true);
            Debug.Log("Throw");
        }

        else if (Input.GetAxis("Triggers") > thresholdTriggers)
        {

            animator.SetBool("Shoot",true);
            Debug.Log("Shoot");
            GamePad.SetVibration(playerIndex, Input.GetAxis("Triggers"), 0);
        }

        else
        {
            float time = 0.4f;

            animator.SetBool("Throw", false);
            animator.SetBool("Shoot", false);
            GamePad.SetVibration(playerIndex, 0, 0);
        }
    }

    private void checkButtonInput()
    {
        if (Input.GetAxis("ButtonY") > 0)
        {
            Debug.Log("ButtonY");
        }

        else if (Input.GetAxis("ButtonX") > 0)
        {
            Debug.Log("ButtonX");
        }

        else if (Input.GetAxis("ButtonA") > 0)
        {
            Debug.Log("ButtonA");

            jump();
        }

        else if (Input.GetAxis("ButtonB") > 0)
        {
            Debug.Log("ButtonB");
        }

    }

    private void checkGazeMenuStatus()
    {

        if (Input.GetAxis("ButtonLB") > 0)
        {
            Debug.Log("ButtonLB");
            
        }

        else if (Input.GetAxis("ButtonRB") > 0)
        {
            GamePad.SetVibration(playerIndex, 0, 1);
        }


    }

    private void StickInputToWorld(float inputX, float inputY,ref float angleOut,ref float speedOut)
    {
        Vector3 rootDirection = transform.forward;
        Vector3 stickDirection = new Vector3(inputX, 0, inputY);

        speedOut = stickDirection.sqrMagnitude;
        Vector3 CameraDirection = Camera.main.transform.forward;
        CameraDirection.y = 0;

        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(CameraDirection));

        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        angleOut = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1 : 1);


        //transform.Rotate(0, angleOut, 0);

    }

    IEnumerator rumbleOverTime(float time, float powerHeavy, float powerSoft)
    {
        GamePad.SetVibration(playerIndex, powerHeavy, powerSoft);
        yield return new WaitForSeconds(time);

        GamePad.SetVibration(playerIndex, 0, 0);
    }

}
