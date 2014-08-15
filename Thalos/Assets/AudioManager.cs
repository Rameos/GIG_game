using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	void Start () {
        GameObject.DontDestroyOnLoad(this.gameObject);
	}
}
