using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	private Transform playerReferenceTag;
	public int timeToDestroy = 3;
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,timeToDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		//playerReferenceTag = GameObject.FindWithTag("Player").transform;
		//if(Vector3.Distance(this.transform.position, playerReferenceTag.position) >15) {

		//}


	}

}
