using System.Collections;

public class Gamestatemanager {


    private static Gamestatemanager instance;

    public static Gamestatemanager Instance()
    {
        if (instance == null)
        {
            instance = new Gamestatemanager();
        }

        return instance;
    }


    public Gamestatemanager()
    {
        // TODO: fancy init Stuff

    }

}
