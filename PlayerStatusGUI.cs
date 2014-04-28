using UnityEngine;
using System.Collections;

public class PlayerStatusGUI : MonoBehaviour {

    public TextMesh healthLabel;
    public TextMesh shieldLabel;
    public TextMesh statusLabel;

	// Use this for initialization
	void Start () 
    {
        healthLabel.text = PlayerScript.health + "/" + PlayerScript.maxHealth;
        shieldLabel.text = PlayerScript.shield + "/" + PlayerScript.maxShield;
        statusLabel.text = "Status";
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthLabel.text =  System.String.Format( "{0:f0}",PlayerScript.health)+ "/" + PlayerScript.maxHealth;
        shieldLabel.text =  System.String.Format( "{0:f0}",PlayerScript.shield) + "/" + PlayerScript.maxShield;
        statusLabel.text = PlayerScript.states.ToString();
	}
}
