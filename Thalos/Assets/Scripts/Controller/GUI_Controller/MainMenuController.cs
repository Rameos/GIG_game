using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MainMenuController : MonoBehaviour {

    private GameObject[] buttonsMainmenu;
    public Color32 colorText { get; set; }
    public Color32 colorSprites { get; set; }

    [SerializeField]
    private float delay;
    private bool isFadeActive = false;

    void Start()
    {
        buttonsMainmenu = GameObject.FindGameObjectsWithTag("MainMenu");


    }



    private void OnMouseOverItem()
    {

    }

    private void OnMouseDown()
    {

    }
}
