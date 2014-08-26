using UnityEngine;
using System.Collections;

public class TextReplaceWithNewLine : MonoBehaviour {

    TextMesh renderer;
    string input; 
	// Use this for initialization
	void Start () {

        renderer = GetComponent<TextMesh>();
        input = renderer.text;

        input = input.Replace("||", "\n");
        renderer.text = input;
	}

}
