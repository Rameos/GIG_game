using UnityEngine;
using System.Collections;


public class PlayerModel {


    private static PlayerModel playerModel;

    public enum DamageTypes {Standard, Fire, Magic, Silver};

    private int HealthPoints { get; set; }
    private int Phial { get; set; }
    private int Damage { get; set; }
    private int Armour { get; set; }
    private int MachinePart { get; set; }
    private int DamageType { get; set; }
    

    public static PlayerModel Instance()
    {
        if (playerModel == null)
        {
            playerModel = new PlayerModel();
        }

        return playerModel;
    }


    private PlayerModel()
    {
        // TODO: fancy init Stuff

    }

}
