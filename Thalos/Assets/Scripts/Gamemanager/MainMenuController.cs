using UnityEngine;
using System.Collections;
using iViewX;
using Controller;

public class MainMenuController : MonoBehaviour {
    public Gamestatemanager.Gamestate stateToStart;

	// Use this for initialization
	void Start () {

        GazeControlComponent control = gameObject.AddComponent < GazeControlComponent>();

        if (gameObject.GetComponent<InputManagerMainMenu>() == null)
        {
            InputManagerMainMenu inputMainMenu = gameObject.AddComponent<InputManagerMainMenu>();
        }

        else if (gameObject.GetComponent<InputManagerMainMenu>() == null)
        {
            //TODO
        }

        if (gameObject.GetComponent<FadeSceneEffect>() == null)
        {
            FadeSceneEffect fadeEffect = gameObject.AddComponent <FadeSceneEffect>();
        }

        Gamestatemanager.Instance.startGame(stateToStart);

	}
	
	void Update () {
	
	}
}
