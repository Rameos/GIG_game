using System.Collections;
using iViewX;
using UnityEngine;
using System;

namespace Controller
{
    public delegate void OpenMainMenu(object s, EventArgs e);
    public delegate void OpenPlayScreen(object s, EventArgs e);
    public delegate void ClosePlayScreen(object s, EventArgs e);
    public delegate void CloseMainMenu(object s, EventArgs e);

    public class Gamestatemanager:MonoBehaviour
    {
        private static Gamestatemanager instance;
        public event OpenMainMenu OpenMainMenuHandler;
        public event OpenPlayScreen OpenPlayScreenHandler;
        public event ClosePlayScreen ClosePlayScreenHandler;
        public event CloseMainMenu CloseMainMenuScreenHandler;

        public enum Gamestate
        {
            Mainmenu,
            Play
        }
        
        public Gamestate actualState { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        void Start()
        {
            initComponents();
            startApplication(actualState);
        }

        /// <summary>
        /// Starts the Intro / later maybe the Savegame
        /// </summary>
        public void startGame()
        {
            Debug.Log("StartGame!");
            OnCloseMainMenu();


            int DEBUG_Level = 1;
            StartCoroutine(waitForFadeOutEffect(DEBUG_Level));
           
        }

        public void loadLevel(int levelID)
        {
            waitForFadeOutEffect(levelID);
        }

        /// <summary>
        /// Init all Controllers of the GameStateManager
        /// </summary>
        /// <param name="startState"></param>
        public void startApplication(Gamestate startState)
        {

            actualState = startState;

            switch (actualState)
            {
                case Gamestate.Mainmenu:

                    Debug.Log("MainMenu!");
                    OnOpenMainMenu();
                    break;

                case Gamestate.Play:
                    Debug.Log("Play!");
                    break;

            }

            //TODO:  init InputManager
        }

        private void initComponents()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            GazeControlComponent control = gameObject.AddComponent<GazeControlComponent>();

            if (gameObject.GetComponent<InputManagerMainMenu>() == null)
            {
                InputManagerMainMenu inputMainMenu = gameObject.AddComponent<InputManagerMainMenu>();
            }

            else if (gameObject.GetComponent<InputManagerMainMenu>() == null)
            {
                //TODO
            }
        }
        
        private void OnOpenMainMenu()
        {
            if (OpenMainMenuHandler != null)
                OpenMainMenuHandler(this, EventArgs.Empty);
        }

        private void OnCloseMainMenu()
        {
            if (CloseMainMenuScreenHandler != null)
                CloseMainMenuScreenHandler(this, EventArgs.Empty);
        }

        IEnumerator waitForFadeOutEffect(int levelID)
        {
            FadeSceneEffect.FadeOut();
            yield return new WaitForSeconds(FadeSceneEffect.fadeSpeed);
            Application.LoadLevel(levelID);
        }

    }
}
