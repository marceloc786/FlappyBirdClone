using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour {

    public Material currentMaterial;
    public float speed;
    private float offset;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        // currentMaterial = renderer.material;
        currentMaterial = GetComponent<Renderer>().material;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }
	
	// Update is called once per frame
	void Update () {

        if ((gameController.GetCurrentState() != GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.MAINMENU &&
            gameController.GetCurrentState() != GameStates.TUTORIAL) || (Time.timeScale != 1))
        {
            return;
        }

        offset += 0.001f;
        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset*speed, 0));

        
    }
}
