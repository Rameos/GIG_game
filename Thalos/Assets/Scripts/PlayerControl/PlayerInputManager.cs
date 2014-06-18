using UnityEngine;
using System.Collections;

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

    [SerializeField]
    private float distanceMax =1f;  
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float speedFactor = 0.25f;
    [SerializeField]
    private float thresholdTriggers = 0.1f;



	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        checkGroundDistance();
        checkButtonInput();
        checkGazeMenuStatus();
        checkshotInput();
        move(inputX, inputY);


	}


    private bool checkGroundDistance()
    {
        RaycastHit hit;
        if(Physics.Raycast(new Ray(transform.position,transform.up*-1),out hit))
        {
            Debug.Log("Distance:" + hit.distance);

            if (hit.distance < distanceMax)
            {
                rigidbody.isKinematic = true;
                //rigidbody.isKinematic = false;
                return false; 
            }

            else
            {
                rigidbody.isKinematic = false;
                return true;
            }
        }

        return false;
    }

    private void move(float inputX, float inputY)
    {
        animator.SetFloat("Runspeed", inputY);


        float angle =0f;
        float speedOut = 0f;
        StickInputToWorld(inputX, inputY, ref angle, ref speedOut);

        Debug.DrawLine(transform.position, transform.position + transform.forward*2);

        if (Mathf.Abs(speedOut) > 0.1f)
        {
            Debug.Log("SpeedOut:" + speedOut);
            transform.position += transform.forward * (speedOut*speedFactor);
            transform.Rotate(0, angle, 0);
        }
        
    }
    
    private void checkshotInput()
    {
        if (Input.GetAxis("Triggers") < 0-thresholdTriggers)
        {
            Debug.Log("Throw");
        }

        else if (Input.GetAxis("Triggers") > thresholdTriggers)
        {
            Debug.Log("Shot");
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
            Debug.Log("ButtonRB");
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
}
