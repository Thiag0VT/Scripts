using UnityEngine;
using System.Collections;
//not using this script righ now
public class CameraScript : MonoBehaviour {

    public float moveVelocity = 2;
    public float xDif = 2;

    private Transform target = null;

	// Use this for initialization
	void Start () 
    {
        // get the target transform
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

	}
	
	// Update is called once per frame
    void LateUpdate ()
    {
        if (target == null)
            return;

        // get the player position and add xDif in X axis
        Vector3 fromPos = new Vector3(target.position.x+xDif    , target.position.y, transform.position.z);
        // Move the camera using Lerp effect(get the camera position and move to player position
        transform.position = Vector3.Lerp(transform.position, fromPos, moveVelocity * Time.deltaTime);
        // transform.LookAt(target);
	
	}
}
