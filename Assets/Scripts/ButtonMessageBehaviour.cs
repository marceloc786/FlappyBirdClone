using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMessageBehaviour : MonoBehaviour {
    public GameObject target;
    public string message;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toquei()
    {
        target.SendMessage(message, SendMessageOptions.RequireReceiver);
    }
}
