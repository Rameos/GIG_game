using UnityEngine;
using System.Collections;
using Backend;
public class DeathManager : MonoBehaviour {

    public bool isCleaningAllItems = true;

    public Vector3 newStartPosition; 
	void Start () {

        //PlayerModel.Instance().revivePosition = transform.position;
	}
	
	void Update () {

        newStartPosition = PlayerModel.Instance().revivePosition;

        if(PlayerModel.Instance().HealthPoints<=0)
        {
            this.gameObject.transform.position = newStartPosition;
            PlayerModel.Instance().HealthPoints = PlayerModel.Instance().MaxHealthPoints-10;
        }


	}
}
