using UnityEngine;
using System.Collections;
using Backend;

public class Robot : BaseEnemy {

	public Robot() : base(100, 10, 5)
    {
    
    }

    public int TakeDamage(int Damage, PlayerModel.DamageTypes DamageType)
    {
        if (DamageType == PlayerModel.DamageTypes.Fire)
        {
            return this.LivePoints;
        }
        return base.TakeDamage(Damage, DamageType);
    }
}
