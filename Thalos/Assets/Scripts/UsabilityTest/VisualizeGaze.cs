using UnityEngine;
using System.Collections;
using iViewX;

public class VisualizeGaze : MonoBehaviour {


    [SerializeField]
    private Texture2D gazeCursor;
    float offsetTexture;

    void Start()
    {
        offsetTexture = gazeCursor.width * 0.5f;
    }



    void OnGUI()
    {
        Vector2 gazePos = (gazeModel.posGazeLeft+gazeModel.posGazeRight)*0.5f;
        GUI.DrawTexture(new Rect(gazePos.x - offsetTexture, gazePos.y - offsetTexture, gazeCursor.width, gazeCursor.height), gazeCursor);
    }
}
