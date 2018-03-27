using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Tester : MonoBehaviour {

    // Use this for initialization
    TextAsset testtext = Resources.Load("Level") as TextAsset;

    void Start () {
        Debug.Log(testtext);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
