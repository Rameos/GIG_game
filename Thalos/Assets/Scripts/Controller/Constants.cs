using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Controller
{
    public static class Constants
    {
        public const int ID_FIRSTLEVEL = 1;
        public const int ID_INTRO = 1;
        public const int ID_MAINMENU = 0; 

        public const int DEADMUSIC_CHANNEL = 0;
        public const int NORMALMUSIC_CHANNEL = 1;
        public const int BATTLEMUSIC_CHANNEL = 2;
        public const int MAINMENUMUSIC_CHANNEL = 3;

        public const string TAG_PLAYER = "Player";
        public const string TAG_MAINCAMERA = "MainCamera";
        public const string TAG_GUI = "GazeGui";
        public const string TAG_ENEMY = "Enemy";

        public const int INGAMEMENU_PAUSE = 0;
        public const int INGAMEMENU_CIRCLEMENU = 1;
        public const int INGAMEMENU_INVENTORY = 2;
        public const int INGAMEMENU_INGAME2DVIEW = 3; 

        public const int LAYERMASK_GUI = 9;

        public const int ID_PLAYER = 0;
        public const int ID_ENEMY = 1;

        public static float BULLETSPEED_STANDARD = 1;
        public static float BULLETSPEED_FIRE = 1;
        public static float BULLETSPEED_ICE = 1;

        public const int healPower = 25;
        
        //Damage
        public const int damageStandardBolt = 15;
        public const int damageFireBolt = 20;
        public const int damageIceBolt = 50;

        public const int damageFirePoison = 30;
        public const int damageIcePoison = 50;

        public const float COOLDOWN_BOLT = 0.5f;
        public const float COOLDOWN_POISON = 0.1f;

        //Popups

        public const int POPUP_ID_FIREHELP = 0;
        public const int POPUP_ID_HEALHELP = 1;
        public const int POPUP_ID_ICE = 2;
        public const int POPUP_ID_NORESOURCES = 3; 


        public enum selectableItemsCircleMenu
        {
            NormalBolt,
            FireBolt,
            IceBolt,
            HealPoison,
            FirePoison,
            IcePoison
        }

        // UsabilityLogs
        public const string USABILITY_FOLDERNAME = "UsabilityReports";
    }
}
