using UnityEngine;
using System.Collections;
using Controller;
using Backend;

namespace Enemy
{
    public class Policeman : BaseEnemy
    {

        public Policeman(int MaxLivePoints, int Damage, int Armour): base(MaxLivePoints, Damage, Armour)
        {

        }
    }
}
