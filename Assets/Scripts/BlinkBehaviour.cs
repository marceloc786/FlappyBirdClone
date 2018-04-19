using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBehaviour : MonoBehaviour {

    public float rateBlink;
    private float currentRateBlink;
    public Renderer rend;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentRateBlink += Time.deltaTime;

        if(currentRateBlink > rateBlink)
        {
            rend.enabled = !rend.enabled;
            currentRateBlink = 0;
        }
		
	}
}
