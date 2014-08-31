using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    /// <summary>
    /// Dont Destroy AudioManager
    /// </summary>
	void Start () {
        GameObject.DontDestroyOnLoad(this.gameObject);
	}
}
