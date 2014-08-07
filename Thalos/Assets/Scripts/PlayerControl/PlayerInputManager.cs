using UnityEngine;
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
    private bool circleMenuIsOpen = false;

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

    //private bool jumpClimax = false;
    [SerializeField]
    private Transform centerOfMass;

    
    float inputX;
    float inputY;
    private int y;

    static int idle = Animator.StringToHash("Idle");
    static int walk = Animator.StringToHash("Locomotion");
    static int shoot_Idle = Animator.StringToHash("Shoot_Idle");
    static int shoot_Walking = Animator.StringToHash("Shoot_Walk"); 
    
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
    private JumpwithGaze jumpScript;
    private ShotManager_Player shotManager;

    //RumbleAttributes
    private float forceHeavy =0f;
    private float forceWeak =0f;
    private bool isRumbleActive = false;

    private Vector3 destinationPoint; 

    //Bullshit
    private bool isGlitchJumpingOn = false;

	void Start () {
        connectController();
        centerOfMass = transform.FindChild("CenterOfMass");
        animator = GetComponent<Animator>();
        findGameController();
        Gamestatemanager.RumbleEventHandler += startRumbleForTime;
        Gamestatemanager.RumbleEventStopHandler += stopRumbleEvent;
        jumpScript = gameObject.GetComponent<JumpwithGaze>();
        capCollider = gameObject.GetComponent<CapsuleCollider>();
        shotManager = gameObject.GetComponent<ShotManager_Player>();

	}

    void Update() 
    {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


        checkIsGrounded();

        move(inputX, inputY);


        checkshootInput();
        ManageRumbleEvents();

        checkButtonInput();
        checkGazeMenuStatus();
        
        // !!!bullshitTime!!!
        if(destinationPoint!= transform.position && isGlitchJumpingOn)
        {
            transform.position = Vector3.Lerp(transform.position, destinationPoint, 0.2f);
        }
        else 
        {
            isGlitchJumpingOn = false;
        }
    }

    public void stopRumbleEvent() 
    {
        StopCoroutine(rumbleOverTime(0f));
        isRumbleActive = false;
    }

    public void startRumbleForTime(float rumbleHeavy, float rumbleWeak,float time) 
    {
        forceHeavy = rumbleHeavy;
        forceWeak = rumbleWeak;
        isRumbleActive = true; 
        StartCoroutine(rumbleOverTime(time));
            
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
        float distance = capCollider.height / 2;
        Ray rayinput = new Ray(centerOfMass.position, -transform.up);

        if (Physics.Raycast(rayinput, distance))
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
        Debug.Log("Jump!");
        animator.SetTrigger("Jump");
    }

    private void createJumpCurve()
    {
        if (isGrounded)
        {
            if (!jumpScript.isActive)
            {
                Debug.Log("JumpScript Not Active");
                rigidbody.velocity = new Vector3(0, jumpForcePower, 0);
                if (Mathf.Abs(inputX) > thresholdStics || Mathf.Abs(inputY) > thresholdStics)
                    rigidbody.velocity += transform.forward * jumpForcePower;
            }

            else
            {
                destinationPoint = jumpScript.getDestinationpoint();
                Debug.DrawRay(transform.position, destinationPoint, Color.magenta, 2f);

                destinationPoint = destinationPoint - transform.position;

                destinationPoint.y = 0;

                Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
                transform.rotation = lookAtRotation;
                //Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);
                
                //And heeeere is the biggest bullshit
                isGlitchJumpingOn = true;
                
                // Old and Good Stuff
                //rigidbody.velocity = new Vector3(0, jumpForcePower, 0);
                //rigidbody.velocity += transform.forward * jumpForcePower;

                //if (Mathf.Abs(inputX) > thresholdStics || Mathf.Abs(inputY) > thresholdStics)


            }

            //rigidbody.velocity.y = 20;
            //rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Force);
        }
        else
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
            transform.rotation = lookAtRotation;
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

            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            
            if (currentState.IsName("Shoot_Walk"))
            {
                Debug.Log("SHOT AND RUN!");
                //speedOut = 0.5f;
            }

            if (isGrounded)
            {
                moveDirection = (transform.forward * speedFactor * speedOut) * Time.deltaTime;

                transform.position += moveDirection;
                transform.Rotate(0, angle, 0);

            }
            else
            {
                //BUG!
                Vector3 destinationPoint = jumpScript.getDestinationpoint();
                Debug.DrawRay(transform.position, destinationPoint, Color.magenta, 2f);

                destinationPoint = destinationPoint - transform.position;

                destinationPoint.y = 0;

                Quaternion lookAtRotation = Quaternion.LookRotation(destinationPoint);
                transform.rotation = lookAtRotation;//Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);
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
            //shotManager.Shot(); 
            if(isInAir||isJumping)
            {
                Debug.Log("OH MY GAWD IM  A FUCKING BIRD!!! LETS SHOOT SOME STUPID STUFF");
                //this.rigidbody.isKinematic = true;
                //this.rigidbody.useGravity = false;
            }
            
            animator.SetBool("Shoot",true);
            Debug.Log("Shoot");
        }

        else
        {
            animator.SetBool("Throw", false);
            animator.SetBool("Shoot", false);
        }

    }

    private void checkButtonInput()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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

            Camera.main.GetComponent<RotateWithGazeInput>().OpenGazeMenu(true);
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_CIRCLEMENU,true);
            circleMenuIsOpen = true;
        }

        //else if (Input.GetAxis("ButtonRB") > 0)
        //{
        //    GamePad.SetVibration(playerIndex, 0, 1);
        //}

        else if(circleMenuIsOpen== true && Input.GetAxis("ButtonLB")<=0)
        {

            Camera.main.GetComponent<RotateWithGazeInput>().OpenGazeMenu(false);
            circleMenuIsOpen = false;
            Gamestatemanager.OnChangeInGameMenu(Constants.INGAMEMENU_CIRCLEMENU, false);

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

    private void ManageRumbleEvents()
    {
        if(isRumbleActive)
        {
            GamePad.SetVibration(playerIndex, forceHeavy, forceWeak);
        }
        else
        {
            GamePad.SetVibration(playerIndex, 0, 0);
            
        }
    }
    
    void OnCollisionStay(Collision colInfo)
    {
       
        try
        {
            Vector3 contactPoint = colInfo.contacts[0].point;
            Vector3 contactPointNormal = colInfo.contacts[0].normal;

            if (isInAir || isJumping)
            {
                Debug.LogError("ColStay");
                Debug.DrawRay(contactPoint, contactPointNormal,Color.green);
                rigidbody.AddForceAtPosition(contactPointNormal * 10, contactPoint);
            }
        }
        catch
        {
            rigidbody.AddForce(Vector3.up*2f);            
        }
        
    }

    IEnumerator rumbleOverTime(float time)
    {
        Debug.Log("Rumble!!!");
        yield return new WaitForSeconds(2);
        Debug.Log("Rumble OVER!!!");
        
        isRumbleActive = false; 
    }

}
