using UnityEngine;
using System.Collections;
using Controller;
using System.Collections.Generic;

public class Winning : MonoBehaviour {

    public int neededDeadRobots = 5;

    private int minutes = 0;
    private int seconds = 0;

    private List <GameObject> enemyList;
    private List<GameObject> spriteList;
    [SerializeField]
    private TextMesh textOutPut;

    [SerializeField]
    private TextMesh enemyOutPut;

    

    
	// Use this for initialization
	void Start () {
        enemyList = new List<GameObject>();
        spriteList = new List<GameObject>();
        getEnemyList();

        neededDeadRobots = enemyList.Count;

	}
	
	// Update is called once per frame
	void Update () {


        UpdateEnemyDisplay();
        UpdateTimerDisplay();

        if (neededDeadRobots <= 0)
        {
            StopTimer();
            //FadeSceneEffect.FadeOut();

        }
	}
    private void UpdateEnemyDisplay()
    {
        enemyOutPut.text = ":" + neededDeadRobots;
    }

    private void UpdateTimerDisplay()
    {
        // Update Timer
        if (minutes < 10 && seconds < 10)
        {
            textOutPut.text = "0" + minutes + ":0" + seconds;
        }
        else if (minutes < 10 && seconds > 10)
        {
            textOutPut.text = "0" + minutes + ":" + seconds;
        }
        else
        {
            textOutPut.text = minutes + ":" + seconds;

        }
    }

    public void StartTimer()
    {
        InvokeRepeating("updateTimer", 1, 1);
    }

    public void StopTimer()
    {
        CancelInvoke("updateTimer");
    }

    public void RemoveHead()
    {
        neededDeadRobots--;
    }
    
    private void updateTimer()
    {
        seconds++;
        if(seconds>=60)
        {
            seconds = 0;
            minutes++;
        }

    }

    private void getEnemyList()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < items.Length;i++)
        {
            if(items[i].name == "Enemy_VS2")
            {
                enemyList.Add(items[i]);
            }
        }
            Debug.Log("EnemyCount: " + enemyList.Count);
    }
    
    IEnumerator finishScene()
    {
        FadeSceneEffect.FadeOut();
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
