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

}
