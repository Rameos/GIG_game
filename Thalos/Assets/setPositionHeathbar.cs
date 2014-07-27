using UnityEngine;
using System.Collections;
using Backend;
public class setPositionHeathbar : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        int hp = PlayerModel.Instance().HealthPoints;
        float maxHp = PlayerModel.Instance().MaxHealthPoints*0.1f;
        Debug.Log("HealthPoints:" + hp);

        Vector3 position = transform.position;

        float percent = PlayerModel.Instance().HealthPoints * 0.1f;
        position.y = percent-maxHp;
        this.transform.position = position; 
	}
}
