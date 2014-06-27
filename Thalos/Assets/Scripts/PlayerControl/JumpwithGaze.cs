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
	
    void Update()
    {
        if (Vector3.Distance(destinationPoint, player.transform.position)>0.01)
        {
            player.transform.position = Vector3.Lerp(transform.position, destinationPoint, 0.1f);
        }
    }


    public void jumpWithGaze()
    {
        if (isActive)
        {
            Debug.Log("JUMP");
            Vector3 gazePos = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;

            Ray screenCast = Camera.main.ScreenPointToRay(gazePos);

            RaycastHit hitInfo;
            if (Physics.Raycast(screenCast, out hitInfo, jumpdistance))
            {
                
                destinationPoint = hitInfo.point;
                destinationPoint = new Vector3(destinationPoint.x, transform.position.y, destinationPoint.z);
            }
        }
    }
}
