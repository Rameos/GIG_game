using UnityEngine;
using System.Collections;

public class Ingredients {
    

    private static Ingredients ingredients;

    public static Ingredients Instance()
    {
        if (ingredients == null)
        {
            ingredients = new Ingredients();
        }

        return ingredients;
    }


    public Ingredients()
    {
        // TODO: fancy init Stuff

        // Water, PureWater, Herb, Oil, PhoenixAsh, Snowdrop, CristalFlower

    }

}


