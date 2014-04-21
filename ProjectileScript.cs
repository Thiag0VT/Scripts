using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	private Transform playerReferenceTag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerReferenceTag = GameObject.FindWithTag("Player").transform;
		if(Vector3.Distance(this.transform.position, playerReferenceTag.position) >15) {
			Destroy(this.gameObject);
		}


	}

}
