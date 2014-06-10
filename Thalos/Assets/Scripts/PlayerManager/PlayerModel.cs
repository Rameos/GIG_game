using UnityEngine;
using System.Collections;


public class PlayerModel {


    private static PlayerModel playerModel;

    public enum DamageTypes {Standard, Fire, Magic, Silver};

    public int HealthPoints { get; set; }
    public int MaxHealthPoints { get; set; }
    public int Phial { get; set; }
    public int Damage { get; set; }
    public int Gold { get; set; }
    public int Armour { get; set; }
    public DamageTypes DamageType { get; set; }
    

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
        this.MaxHealthPoints = 100;
        this.Phial = 0;
        this.Damage = 42;
        this.Gold = 10;
        this.Armour = 5;
        this.DamageType = DamageTypes.Standard;
        


    }       

}
