using UnityEngine;
using System.Collections;
using Backend;
public class setPositionHeathbar : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        int hp = PlayerModel.Instance().HealthPoints;
        float maxHp = PlayerModel.Instance().MaxHealthPoints*0.1f;
        Vector3 position = transform.position;

        float percent = PlayerModel.Instance().HealthPoints * 0.1f;
        position.y = percent-maxHp;
        this.transform.position =Vector3.Lerp(transform.position,position,0.2f); 
	}
}
