using System.Collections;
using iViewX;
using UnityEngine;
using System;

namespace Controller
{

    public delegate void OpenMainMenu();
    public delegate void CloseMainMenu();

    public delegate void OpenPlayScreen();
    public delegate void ClosePlayScreen();

    public delegate void PlayerIsDead();
    public delegate void PlayerGetsDamage(int damagepoints);
    
    public delegate void OpenInGameMenu();
    public delegate void CloseInGameMenu();

    public delegate void RumbleEvent(float duration,float forceHeavy, float forceLight);

    public class Gamestatemanager:MonoBehaviour
    {
        public static event OpenMainMenu OpenMainMenuHandler;
        public static event OpenPlayScreen OpenPlayScreenHandler;
        public static event ClosePlayScreen ClosePlayScreenHandler;
        public static event CloseMainMenu CloseMainMenuScreenHandler;
        
        public static event PlayerIsDead PlayerIsDeadHandler;
        public static event PlayerGetsDamage PlayerGetsDamageHandler;

        public static event RumbleEvent RumbleEventHandler;

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

            if (levelID >= Constants.ID_FIRSTLEVEL)
            {
                OnCloseMainMenu();
                OnOpenPlayView();
            }
            else if (levelID == Constants.ID_INTRO)
            {
                OnCloseMainMenu();
            }
            else if (levelID == Constants.ID_MAINMENU)
            {
                OnOpenMainMenu();
                OnClodePlayView();
            }
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

        public static void OnOpenMainMenu()
        {
            Debug.Log("OpenMainMenu");
            if (OpenMainMenuHandler != null)
                OpenMainMenuHandler();

        }

        /// <summary>
        /// CloseMenu
        /// </summary>

        public static void OnCloseMainMenu()
        {
            if (CloseMainMenuScreenHandler != null)
            {
                CloseMainMenuScreenHandler();
            }
        }

        public static void OnOpenPlayView()
        {
            if (OpenPlayScreenHandler != null)
            {
                OpenPlayScreenHandler();
            }
        }

        public static void OnClodePlayView()
        {
            if (ClosePlayScreenHandler != null)
            {
                ClosePlayScreenHandler();
            }
        }

        public static void OnPlayerIsDead()
        {

            Debug.Log("OpenPlayer");
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

        public static void OnRumbleEvent(float duration,float forceHeavy, float forceLight)
        {
            if (RumbleEventHandler != null)
            {
                RumbleEventHandler(duration,forceHeavy,forceLight);
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
