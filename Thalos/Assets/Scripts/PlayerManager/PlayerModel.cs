using UnityEngine;
using System.Collections;


public class PlayerModel {


    private static PlayerModel playerModel;

    public enum DamageTypes {Standard, Fire, Magic, Silver};

    private int HealthPoints { get; set; }
    private int MaxHealthPoints { get; set; }
    private int Phial { get; set; }
    private int Damage { get; set; }
    private int Gold { get; set; }
    private int Armour { get; set; }
    private DamageTypes DamageType { get; set; }
    

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

    public bool Dead()
    {
        if(this.HealthPoints > 0)
        {
            return false;
        }
        return true;
    }

    public int TakeDamage(int Damage)
    {
        this.HealthPoints -= (Damage - this.Armour);
        return this.HealthPoints;
    
    }

    public int AddMaxHealthPoints(int HeartPiece)
    {
        this.MaxHealthPoints += HeartPiece;
        return this.MaxHealthPoints;
    }
    
    public int Heal(int HealPotion)
    {
        if(this.HealthPoints + HealPotion >= this.MaxHealthPoints)
        {
            this.HealthPoints = this.MaxHealthPoints;
        }  
        else
        {
            this.HealthPoints += HealPotion;
        }      
        return this.HealthPoints;
    }

    public int AddArmour(int PieceOfArmour)
    {
        this.Armour += PieceOfArmour;
        return this.Armour;
    }
    
    public int AddGold(int GoldCoins)
    {
        this.Gold += GoldCoins;
        return this.Gold;
    }
    
        

}
