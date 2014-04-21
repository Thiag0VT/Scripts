using UnityEngine;
using System.Collections;

public class LevelScrpit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	//comentei aqui
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
