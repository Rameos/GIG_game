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

    public class Gamestatemanager
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


        public static Gamestatemanager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Gamestatemanager();
                }
                return instance;
            }
        }

        public Gamestatemanager()
        {

        }

        public void startGame(Gamestate startState)
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
            OnOpenMainMenu();

            //TODO:  init InputManager
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
    }
}
