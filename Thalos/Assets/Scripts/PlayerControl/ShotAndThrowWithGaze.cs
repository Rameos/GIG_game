using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShotAndThrowWithGaze : MonoBehaviour {

    [SerializeField]
    private Transform position_GazeAim;

    [SerializeField]
    private Transform startPoint_Bullet;

    [SerializeField]
    private Transform startPoint_Poison;

    [SerializeField]
    private bool isGazeActive = false;

    public Vector2 crossfadePosition;
    public Texture crossFadeTexture;

    public Vector3 directionShoot { private set; get; }
    public Vector3 directionPoison { private set; get; }

    public Vector3 destinationPoint_Shoot { private set; get; }
    public Vector3 destinationPoint_Poison { private set; get; }

    void OnGUI()
    {
        float offSet = crossFadeTexture.width * 0.5f;
        Vector2 GUIdrawItem = crossfadePosition;
        GUIdrawItem.y = Screen.height - GUIdrawItem.y;
        GUI.DrawTexture(new Rect(GUIdrawItem.x - offSet, GUIdrawItem.y - offSet, crossFadeTexture.width, crossFadeTexture.height), crossFadeTexture);
    }

    void Start()
    {
        crossfadePosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.15f);
        crossfadePosition.y = Screen.height - crossfadePosition.y; 
    }
    void Update()
    {

        crossfadePosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.15f);
        crossfadePosition.y = Screen.height - crossfadePosition.y; 

        int playerLayerMask = LayerMask.NameToLayer("Player");
        Vector3 gazePosition = crossfadePosition;


        if (gazeModel.isEyeDetected && isGazeActive)
        {
          
            gazePosition = (gazeModel.posGazeLeft + gazeModel.posGazeRight) * 0.5f;
            gazePosition.y = Screen.height - gazePosition.y;
        }

            Ray ray = Camera.main.ScreenPointToRay(gazePosition);
            //Raycast
            RaycastHit hitInformation;
            Physics.Raycast(ray, out hitInformation, 1000f, playerLayerMask);

            Debug.DrawRay(ray.origin, ray.direction, Color.cyan);

            if (hitInformation.Equals(null) == false && hitInformation.collider!= null)
            {
                if (hitInformation.collider.tag != "Bullet")
                {
                    Debug.Log("hitInformation: " + hitInformation.collider.name);
                    position_GazeAim.transform.position = hitInformation.point;

                    destinationPoint_Shoot = position_GazeAim.transform.position;
                    destinationPoint_Poison = position_GazeAim.transform.position;

                }
            }

            //Debug.DrawLine(position_GazeAim.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position,Color.red);

            directionPoison = (hitInformation.point - startPoint_Poison.position).normalized;
            directionShoot = (hitInformation.point - startPoint_Bullet.position).normalized;


            Debug.DrawRay(startPoint_Bullet.position, directionShoot, Color.red);
            Debug.DrawRay(startPoint_Poison.position, directionShoot, Color.red);
        



    }
    
    

}
