using UnityEngine;
using System.Collections;
using iViewX;
using Controller; 
public class JumpwithGaze : MonoBehaviour {

    private float jumpdistance = 20;
    private GameObject player;
    private Vector3 destinationPoint;
    public bool isActive = true;
    
	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
	}
	



    public Vector3 getDestinationpoint()
    {
        if (isActive)
        {
            Debug.Log("JUMP");
            Vector3 gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            gazePos.y = Screen.height - gazePos.y;

            Ray screenCast = Camera.main.ScreenPointToRay(gazePos);
            LayerMask player = LayerMask.NameToLayer("Player");
            RaycastHit hitInfo;
            if (Physics.Raycast(screenCast, out hitInfo, jumpdistance,player))
            {
                
                destinationPoint = hitInfo.point;

                destinationPoint = new Vector3(destinationPoint.x, transform.position.y, destinationPoint.z);


                return destinationPoint;

            }
        }

        return Vector3.zero;
    }
}
