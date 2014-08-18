using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    private GameObject loadingscreenItems;


    private bool isRunning = false;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        loadingscreenItems.SetActive(false);
    }

    public void startLoadingProcess(int levelID)
    {
        if(!isRunning)
        {
            StartCoroutine(DisplayLoadingScreen(levelID));
        }
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator DisplayLoadingScreen(int levelID)
    {

        isRunning = true; 
        loadingscreenItems.SetActive(true);
        AsyncOperation async = Application.LoadLevelAsync(levelID);
        
        while (!async.isDone)
        {
            Debug.Log("LoadDone: " + async.progress * 100);
            yield return null;

        }

        yield return new WaitForEndOfFrame();
        isRunning = false; 
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.LogError("levelWas loaded!");
        loadingscreenItems.SetActive(false);
    }
    
    
}
