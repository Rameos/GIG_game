using UnityEngine;
using System.Collections;
using Controller;
public class GUI_CloseCredits : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetAxis("ButtonB") > 0.75)
        {
            gameObject.SetActive(false);
            Gamestatemanager.OnOpenMainMenu();
        }
    }
}
