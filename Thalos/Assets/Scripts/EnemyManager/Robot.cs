using UnityEngine;
using System.Collections;
using Backend;

namespace Enemy
{
    public class Robot : BaseEnemy
    {

        public Robot(int MaxLivePoints, int Damage, int Armour)
            : base(MaxLivePoints, Damage, Armour)
        {

        }
    }
}
