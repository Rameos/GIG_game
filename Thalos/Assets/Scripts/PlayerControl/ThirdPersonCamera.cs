using UnityEngine;
using System.Collections;

/// <summary>
/// Struct to hold data for aligning camera
/// </summary>
struct CameraPosition
{
    // Position to align camera to, probably somewhere behind the character
    // or position to point camera at, probably somewhere along character's axis
    private Vector3 position;
    // Transform used for any rotation
    private Transform xForm;

    public Vector3 Position { get { return position; } set { position = value; } }
    public Transform XForm { get { return xForm; } set { xForm = value; } }

   


    public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
    {
        position = pos;
        xForm = transform;
        xForm.name = camName;
        xForm.parent = parent;
        xForm.localPosition = Vector3.zero;
        xForm.localPosition = position;
    }
}

public class ThirdPersonCamera : MonoBehaviour
{
    public float invertY = 1;

    #region Variables (private)

    // Inspector serialized	
    [SerializeField]
    private Transform parentRig;
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceAwayMultipler = 1.5f;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float distanceUpMultiplier = 5f;
    [SerializeField]
    private GameObject follow;
    [SerializeField]
    private Transform followXform;
    [SerializeField]
    private float widescreen = 0.2f;
    [SerializeField]
    private float targetingTime = 0.5f;
    [SerializeField]
    private float firstPersonLookSpeed = 3.0f;
    [SerializeField]
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 90.0f);
    [SerializeField]
    private float fPSRotationDegreePerSecond = 120f;
    [SerializeField]
    private float firstPersonThreshold = 0.5f;
    [SerializeField]
    private float freeThreshold = -0.1f;
    [SerializeField]
    private Vector2 camMinDistFromChar = new Vector2(1f, -0.5f);
    [SerializeField]
    private float rightStickThreshold = 0.01f;
    [SerializeField]
    private const float freeRotationDegreePerSecond = -5f;

    public float gazeInput = 0;

    // Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;
    private Vector3 velocityLookDir = Vector3.zero;
    [SerializeField]
    private float lookDirDampTime = 0.1f;


    // Private global only
    private Vector3 lookDir;
    private Vector3 curLookDir;
   // private BarsEffect barEffect;

    private CamStates camState = CamStates.Free;
    private float xAxisRot = 0.0f;
    private CameraPosition firstPersonCamPos;
    private float lookWeight;
    private const float TARGETING_THRESHOLD = 0.01f;
    private Vector3 savedRigToGoal;
    private float distanceAwayFree;
    private float distanceUpFree;
    private Vector2 rightStickPrevFrame = Vector2.zero;

    private bool isStartScene = true;

    #endregion

    #region Properties (public)

    public Transform ParentRig
    {
        get
        {
            return this.parentRig;
        }
    }

    public Vector3 LookDir
    {
        get
        {
            return this.curLookDir;
        }
    }

    public CamStates CamState
    {
        get
        {
            return this.camState;
        }
    }

    public enum CamStates
    {
        Behind,
        FirstPerson,
        Target,
        Free
    }

    #endregion

    #region Unity event functions

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        initPosition();

        parentRig = this.transform.parent;
        if (parentRig == null)
        {
            Debug.LogError("Parent camera to empty GameObject.", this);
        }


        lookDir = followXform.forward;
        curLookDir = followXform.forward;


        camState = CamStates.Free;
        ResetCamera();
    }


    void LateUpdate()
    {
        // Pull values from controller/keyboard
        float rightX =- Input.GetAxis("RightStickX")+gazeInput;
        float rightY =- Input.GetAxis("RightStickY");
        float leftX = Input.GetAxis("Horizontal");
        float leftY = invertY * Input.GetAxis("Vertical");
        
        if(isStartScene)
        {
            rightY += 1;
        }

        Vector3 characterOffset = followXform.position + new Vector3(0f, distanceUp, 0f);
        Vector3 lookAt = characterOffset;
        Vector3 targetPosition = Vector3.zero;

        //.Log("CameraFree");
        lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime * firstPersonLookSpeed);

        // Move height and distance from character in separate parentRig transform since RotateAround has control of both position and rotation
        Vector3 rigToGoalDirection = Vector3.Normalize(characterOffset - this.transform.position);
        // Can't calculate distanceAway from a vector with Y axis rotation in it; zero it out
        rigToGoalDirection.y = 0f;

        Vector3 rigToGoal = characterOffset - parentRig.position;
        rigToGoal.y = 0;
        Debug.DrawRay(parentRig.transform.position, rigToGoal, Color.red);


        float rightXNoGaze = rightX - gazeInput;
        
        // Panning in and out
        // If statement works for positive values; don't tween if stick not increasing in either direction; also don't tween if user is rotating
        // Checked against rightStickThreshold because very small values for rightY mess up the Lerp function
        if (rightY < -1f * rightStickThreshold && rightY <= rightStickPrevFrame.y && Mathf.Abs(rightXNoGaze) < rightStickThreshold)
        {
            distanceUpFree = Mathf.Lerp(distanceUp, distanceUp * distanceUpMultiplier, Mathf.Abs(rightY));
            distanceAwayFree = Mathf.Lerp(distanceAway, distanceAway * distanceAwayMultipler, Mathf.Abs(rightY));
            targetPosition = characterOffset + followXform.up * distanceUpFree - rigToGoalDirection * distanceAwayFree;
        }
        else if (rightY > rightStickThreshold && rightY >= rightStickPrevFrame.y && Mathf.Abs(rightXNoGaze) < rightStickThreshold)
        {
            // Subtract height of camera from height of player to find Y distance
            distanceUpFree = Mathf.Lerp(Mathf.Abs(transform.position.y - characterOffset.y), camMinDistFromChar.y, rightY);
            // Use magnitude function to find X distance	
            distanceAwayFree = Mathf.Lerp(rigToGoal.magnitude, camMinDistFromChar.x, rightY);

            targetPosition = characterOffset + followXform.up * distanceUpFree - rigToGoalDirection * distanceAwayFree;
        }

        // Store direction only if right stick inactive
        if (rightX != 0 || rightY != 0)
        {
            savedRigToGoal = rigToGoalDirection;
        }


        // Rotating around character
        parentRig.RotateAround(characterOffset, followXform.up, freeRotationDegreePerSecond * (Mathf.Abs(rightX) > rightStickThreshold ? rightX : 0f));

        // Still need to track camera behind player even if they aren't using the right stick; achieve this by saving distanceAwayFree every frame
        if (targetPosition == Vector3.zero)
        {
            targetPosition = characterOffset + followXform.up * distanceUpFree - savedRigToGoal * distanceAwayFree;
        }

        SmoothPosition(parentRig.position, targetPosition);

        transform.LookAt(lookAt);

        rightStickPrevFrame = new Vector2(rightX, rightY);
    }

    #endregion


    #region Methods

    private void initPosition()
    {
        StartCoroutine(startScene());
    }

    private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
    {
        // Making a smooth transition between camera's current position and the position it wants to be in
        parentRig.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        // Compensate for walls between camera
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, wallHit.normal, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }

    /// <summary>
    /// Reset local position of camera inside of parentRig and resets character's look IK.
    /// </summary>
    private void ResetCamera()
    {
        lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime * firstPersonLookSpeed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
    }

    #endregion Methods

    IEnumerator startScene()
    {
        yield return new WaitForSeconds(2);
        isStartScene = false;
    }
}
