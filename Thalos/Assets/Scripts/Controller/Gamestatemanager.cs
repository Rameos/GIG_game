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
    
    public delegate void ChangeInGameMenu(int ID_Menu,bool status);
    public delegate void SelectNewItem(Constants.selectableItemsCircleMenu newItem);

    public delegate void RumbleEvent(float duration,float forceHeavy, float forceLight);
    public delegate void RumbleEventStop();

    public class Gamestatemanager:MonoBehaviour
    {
        [SerializeField]
        private GameObject loadScreen;
        
        public static event OpenMainMenu OpenMainMenuHandler;
        public static event OpenPlayScreen OpenPlayScreenHandler;
        public static event ClosePlayScreen ClosePlayScreenHandler;
        public static event CloseMainMenu CloseMainMenuScreenHandler;

        public static event ChangeInGameMenu ChangeInGameMenuHandler;
        public static event SelectNewItem SelectNewItemHandler;

        public static event PlayerIsDead PlayerIsDeadHandler;
        public static event PlayerGetsDamage PlayerGetsDamageHandler;

        public static event RumbleEvent RumbleEventHandler;
        public static event RumbleEventStop RumbleEventStopHandler;
        
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
            GameObject.DontDestroyOnLoad(this.gameObject);
            startApplication(actualState);
        }

        void OnLevelWasLoaded(int levelID)
        {
            FadeSceneEffect.FadeIn();

            if (levelID >= Constants.ID_FIRSTLEVEL)
            {
                OnCloseMainMenu();
               
            }
        }

        void Update()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }


        /// <summary>
        /// Starts the Intro / later maybe the Savegame
        /// </summary>
        public void startGame()
        {
        
            GameObject.DontDestroyOnLoad(this.gameObject);
            StartCoroutine(waitForFadeOutEffect(Application.loadedLevel+1));     
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

            Debug.Log("OpenPlayView");
            if (OpenPlayScreenHandler != null)
            {
                OpenPlayScreenHandler();
            }
        }

        public static void OnClosePlayView()
        {
            if (ClosePlayScreenHandler != null)
            {
                ClosePlayScreenHandler();
            }
        }

        public static void OnPlayerIsDead()
        {

            Debug.Log("DiePlayer");
            if (PlayerIsDeadHandler != null)
            {
                PlayerIsDeadHandler();
            }
        }

        public static void OnPlayerGetsDamage(int damage)
        {
            
            if (PlayerGetsDamageHandler != null)
            {
                Debug.Log("BOOM!");
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

        public static void OnRumbleEventStop()
        {
            if(RumbleEventStopHandler != null)
            {
                RumbleEventStopHandler();
            }
        }

        public static void OnChangeInGameMenu(int ID_MenuState,bool state)
        {
            if (ChangeInGameMenuHandler != null)
            {
                ChangeInGameMenuHandler(ID_MenuState,state);
            }
        }

        public static void OnSelectNewItem(Constants.selectableItemsCircleMenu selectedItem)
        {
            if(SelectNewItemHandler != null)
            {
                SelectNewItemHandler(selectedItem);
            }
        }


        IEnumerator waitForFadeOutEffect(int levelID)
        {
            OnCloseMainMenu();
            FadeSceneEffect.FadeOut();
            yield return new WaitForSeconds(FadeSceneEffect.fadeSpeed);
            Debug.Log("LoadLevelWith ID: " + levelID);
            
            
            if (levelID < 0)
            {
                Application.Quit();
            }
            else
            {
                Debug.Log("waitForFadeOut! Start levelLoading");
                loadScreen.GetComponent<LoadScene>().startLoadingProcess(levelID);
            }
        }
    }
}
