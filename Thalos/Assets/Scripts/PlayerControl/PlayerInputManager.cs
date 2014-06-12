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
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        move(inputX, inputY);



	}

    private void move(float inputX, float inputY)
    {
        Debug.Log("MOVE: inputX:" +inputX+" inputY:"+inputY);


        gameObject.transform.position += new Vector3(inputX*speed ,0,inputY*speed);
    }
}
