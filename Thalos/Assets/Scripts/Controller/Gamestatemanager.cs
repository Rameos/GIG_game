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
    public delegate void PlayerIsDead();
    public delegate void PlayerGetsDamage(int damagepoints);
    public class Gamestatemanager:MonoBehaviour
    {
        public event OpenMainMenu OpenMainMenuHandler;
        public event OpenPlayScreen OpenPlayScreenHandler;
        public event ClosePlayScreen ClosePlayScreenHandler;
        public event CloseMainMenu CloseMainMenuScreenHandler;
        
        public static event PlayerIsDead PlayerIsDeadHandler;
        public static event PlayerGetsDamage PlayerGetsDamageHandler; 

        public enum Gamestate
        {
            Mainmenu,
            Play,
            Sequence
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

        void OnLevelWasLoaded(int levelID)
        {
            FadeSceneEffect.FadeIn();
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

        /// <summary>
        /// End the Application
        /// </summary>
        public void endGame()
        {
            StartCoroutine(waitForFadeOutEffect(-1));
        }

        /// <summary>
        /// Load the Level
        /// </summary>
        /// <param name="levelID"></param>
        public void loadLevel(int levelID)
        {
            StartCoroutine(waitForFadeOutEffect(levelID));
        }

        /// <summary>
        /// Load the Level
        /// </summary>
        /// <param name="levelName"></param>
        public void loadLevel(string levelName)
        {
            StartCoroutine(waitForFadeOutEffect(levelName));
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

        /// <summary>
        /// init all Components of the Game
        /// </summary>
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
        
        /// <summary>
        /// OptionEvent
        /// </summary>
        private void OnOpenMainMenu()
        {
            if (OpenMainMenuHandler != null)
                OpenMainMenuHandler(this, EventArgs.Empty);
        }

        /// <summary>
        /// CloseMenu
        /// </summary>
        private void OnCloseMainMenu()
        {
            if (CloseMainMenuScreenHandler != null)
                CloseMainMenuScreenHandler(this, EventArgs.Empty);
        }

        public static void OnPlayerIsDead()
        {
            if (PlayerIsDeadHandler != null)
            {
                PlayerIsDeadHandler();
            }
        }

        public static void OnPlayerGetsDamage(int damage)
        {
            if (PlayerGetsDamageHandler != null)
            {
                PlayerGetsDamageHandler(damage);
            }
        }

        IEnumerator waitForFadeOutEffect(int levelID)
        {
            FadeSceneEffect.FadeOut();
            yield return new WaitForSeconds(FadeSceneEffect.fadeSpeed);
            Debug.Log("LoadLevelWith ID: " + levelID);
            
            if (levelID < 0)
            {
                Application.Quit();

            }
            else
            {
                Application.LoadLevel(levelID);
            }
        }

        IEnumerator waitForFadeOutEffect(string levelName)
        {
            FadeSceneEffect.FadeOut();
            yield return new WaitForSeconds(FadeSceneEffect.fadeSpeed);
            Application.LoadLevel(levelName);
        }
    }
}
