using UnityEngine;
using System.Collections;

public class ShotAndThrowWithGaze : MonoBehaviour {

    [SerializeField]
    private Transform position_GazeAim;

    [SerializeField]
    private Transform startPoint_Bullet;

    [SerializeField]
    private Transform startPoint_Poison;

    public Vector3 directionShoot { private set; get; }
    public Vector3 directionPoison { private set; get; }

    public Vector3 destinationPoint_Shoot { private set; get; }
    public Vector3 destinationPoint_Poison { private set; get; }

    void Update()
    {
        int playerLayerMask = LayerMask.NameToLayer("Player");
       
        Vector3 gazePosition = (gazeModel.posGazeLeft+gazeModel.posGazeRight)*0.5f;
        gazePosition.y = Screen.height - gazePosition.y;


        Ray ray = Camera.main.ScreenPointToRay(gazePosition);
        //Raycast
        RaycastHit hitInformation;
        Physics.Raycast(ray, out hitInformation, 1000f, playerLayerMask);

        if (hitInformation.Equals(null) == false)
        {
            position_GazeAim.transform.position = hitInformation.point;

            destinationPoint_Shoot = position_GazeAim.transform.position;
            destinationPoint_Poison = position_GazeAim.transform.position;
        
        //update to better Stuff
        }
        
        //Debug.DrawLine(position_GazeAim.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position,Color.red);

        directionPoison = (hitInformation.point - startPoint_Poison.position).normalized;
        directionShoot = (hitInformation.point - startPoint_Bullet.position).normalized;
        

        Debug.DrawRay(startPoint_Bullet.position, directionShoot, Color.red);
        Debug.DrawRay(startPoint_Poison.position, directionShoot, Color.red);
    }
    
    

}
