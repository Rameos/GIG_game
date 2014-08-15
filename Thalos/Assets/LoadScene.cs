using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    private GameObject loadingscreenItems; 

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        loadingscreenItems.SetActive(false);
    }

    public void startLoadingProcess(int levelID)
    {
        
        StartCoroutine(DisplayLoadingScreen(levelID));
    }


    IEnumerator DisplayLoadingScreen(int levelID)
    {
        loadingscreenItems.SetActive(true);
        AsyncOperation async = Application.LoadLevelAsync(levelID);
        
        while (!async.isDone)
        {
            Debug.Log("LoadDone: " + async.progress * 100);
            yield return null;
        }        
    }

    void OnLevelWasLoaded(int level)
    {
        loadingscreenItems.SetActive(false);
    }
    
    
}
