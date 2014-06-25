using UnityEngine;
using System.Collections;
using Backend;

public class Endboss : BaseEnemy
{

    public Endboss()
        : base(100, 30, 20)
    {

    }

    public int TakeDamage(int Damage, PlayerModel.DamageTypes DamageType)
    {
        if (DamageType == PlayerModel.DamageTypes.Fire)
        {
            return this.LivePoints;
        }
        if (DamageType == PlayerModel.DamageTypes.Standard)
        {
            return this.LivePoints;
        }
        return base.TakeDamage(Damage, DamageType);
    }
}
