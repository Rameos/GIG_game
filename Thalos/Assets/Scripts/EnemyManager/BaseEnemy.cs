using UnityEngine;
using System.Collections;

public abstract class BaseEnemy {

    private int MaxLivePoints { get; set; }
    private int LivePoints { get; set; }
    private int Damage { get; set; }
    private int Armour { get; set; }
    


    public BaseEnemy(int MaxLivePoints, int Damage, int Armour)
    {
        this.MaxLivePoints = MaxLivePoints; 
        this.LivePoints = MaxLivePoints;
        this.Damage = Damage;
        this.Armour = Armour;
    }

    public int TakeDamage(int Damage)
    {
        this.LivePoints -= (Damage - Armour);
        return this.LivePoints;
    }

}