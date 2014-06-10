using UnityEngine;
using System.Collections;

public class PlayerController {


    private static PlayerController playerController;

    private PlayerModel playerModel;


    public static PlayerController Instance()
    {
        if (playerController == null)
        {
            playerController = new PlayerController();
        }

        return playerController;
    }


    private PlayerController()
    {
        playerModel = PlayerModel.Instance();
    }

    public bool Dead()
    {
        if (playerModel.HealthPoints > 0)
        {
            return false;
        }
        return true;
    }

    public int TakeDamage(int Damage)
    {
        playerModel.HealthPoints -= (Damage - playerModel.Armour);
        return playerModel.HealthPoints;

    }

    public int AddMaxHealthPoints(int HeartPiece)
    {
        playerModel.MaxHealthPoints += HeartPiece;
        return playerModel.MaxHealthPoints;
    }

    public int Heal(int HealPotion)
    {
        if (playerModel.HealthPoints + HealPotion >= playerModel.MaxHealthPoints)
        {
            playerModel.HealthPoints = playerModel.MaxHealthPoints;
        }
        else
        {
            playerModel.HealthPoints += HealPotion;
        }
        return playerModel.HealthPoints;
    }

    public int AddArmour(int PieceOfArmour)
    {
        playerModel.Armour += PieceOfArmour;
        return playerModel.Armour;
    }

    public int AddGold(int GoldCoins)
    {
        playerModel.Gold += GoldCoins;
        return playerModel.Gold;
    }
}
