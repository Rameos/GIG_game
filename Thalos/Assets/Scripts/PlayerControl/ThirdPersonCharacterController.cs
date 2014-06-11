using UnityEngine;
using System.Collections;

public class ThirdPersonCharacterController : MonoBehaviour
{


    #region Variables
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Camera gameCam;
    [SerializeField]
    private float rotationDegreePerSecond = 120f;
    [SerializeField]
    private float directionSpeed = 1.5f;
    [SerializeField]
    private float directionDampTime = 0.25f;
    [SerializeField]
    private float speedDampTime = 0.05f;
    [SerializeField]
    private float focDampTime = 3f;
    [SerializeField]
    private float jumpMultiplier = 1f;
    [SerializeField]
    private CapsuleCollider capCollider;
    [SerializeField]
    private float jumpDist = 1f;
    #endregion

    #region privateVariables
    private float leftX = 0f;
    private float leftY = 0f;
    private AnimatorStateInfo stateInfo;
    private AnimatorTransitionInfo transInfo; 
    private float speed =0f;
    private float direction = 0f;
    private float charAngle = 0f;
    private const float SPRINT_SPEED = 2.0f;
    private const float SPRINT_FIC = 75f;
    private const float NORMAL_FOV = 60f;
    private float capsuleHeight;

    // Hashes
    private int m_LocomotionId = 0;
    private int m_LocomotionPivotLId = 0;
    private int m_LocomotionPivotRId = 0;
    private int m_LocomotionPivotLTransId = 0;
    private int m_LocomotionPivotRTransId = 0;
    #endregion

    #region Properties

    public Animator Animator
    {
        get
        {
            return this.animator;
        }
    }

    public float Speed
    {
        get
        {
            return this.speed;
        }
    }

    public float LocomotionThreshold { get { return 0.2f; } }
    
    #endregion

    void Start()
    {
        animator = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider>();
        capsuleHeight = capCollider.height;
        if (animator.layerCount >= 2)
        {
            animator.SetLayerWeight(1, 1);
        }

        // Hash all animation names for performance
        m_LocomotionId = Animator.StringToHash("Base Layer.Locomotion");
        m_LocomotionPivotLId = Animator.StringToHash("Base Layer.LocomotionPivotL");
        m_LocomotionPivotRId = Animator.StringToHash("Base Layer.LocomotionPivotR");
        m_LocomotionPivotLTransId = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.LocomotionPivotL");
        m_LocomotionPivotRTransId = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.LocomotionPivotR");
    }

    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        transInfo = animator.GetAnimatorTransitionInfo(0);

        if (Input.GetButton("Jump"))
        {
            animator.SetFloat("Jump", 1);
        }
        else
        {
            animator.SetFloat("Jump", 1);
        }

        leftX = Input.GetAxis("Horizontal");
        leftY = Input.GetAxis("Vertical");

        charAngle = 0f;
        direction = 0f;

        float charSpeed = 0f;
        StrickToWorldspace(this.transform, gameCam.transform, ref direction, ref charSpeed, ref charAngle, IsInPivot());

        speed = charSpeed;

        animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        animator.SetFloat("Forward", direction, directionDampTime, Time.deltaTime);

        if (speed > LocomotionThreshold)
        {
            if (!IsInPivot())
            {
                Animator.SetFloat("Turn", charAngle);
            }
        }
        if (speed < LocomotionThreshold && Mathf.Abs(leftX) < 0.05)
        {
            animator.SetFloat("Forward", 0f);
            animator.SetFloat("Turn", 0f);
        }

    }



    private void StrickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, ref float angleOut, bool isPivoting)
    {
        Vector3 rootDirection = root.forward;
        Vector3 strickDirection = new Vector3(leftX, 0, leftY);

        speedOut = strickDirection.sqrMagnitude;

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(CameraDirection));

        Vector3 moveDirection = referentialShift * strickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);


        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
        if(!isPivoting)
        {
            angleOut = angleRootToMove;
        }
        angleRootToMove /= angleRootToMove * directionSpeed;

    }

    public bool IsInPivot()
    {
        return stateInfo.nameHash == m_LocomotionPivotLId ||
            stateInfo.nameHash == m_LocomotionPivotRId ||
            transInfo.nameHash == m_LocomotionPivotLTransId ||
            transInfo.nameHash == m_LocomotionPivotRTransId;
    }
}
