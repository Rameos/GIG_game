using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controller;
public class HideWhenCloseMenu : MonoBehaviour {


    List<GameObject> children;
	
    void Start () {
        
        getChildrenInList();
        Deactivateitems();

        StartCoroutine(renderInputLater());
        Gamestatemanager.CloseMainMenuScreenHandler += Deactivateitems;
        
	}


    void getChildrenInList()
    {
        children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }
    void Deactivateitems()
    {
        try
        {
            foreach (GameObject child in children)
            {
                if(child!=null)
                {
                    child.SetActive(false);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Why Children Empty`?");
            Debug.LogError(e);
        }
        
        
    }

    void Activateitems()
    {

        foreach (GameObject child in children)
        {
            child.SetActive(true);
        }
    }
    IEnumerator renderInputLater()
    {
        yield return new WaitForSeconds(2.1f);
        Activateitems();
    }
}
