using UnityEngine;
using System.Collections;

public class TextReplaceWithNewLine : MonoBehaviour {

    TextMesh Textrenderer;
    string input; 

	void Start () {

        Textrenderer = GetComponent<TextMesh>();
        input = Textrenderer.text;

        input = input.Replace("||", "\n");
        Textrenderer.text = input;
	}

}
