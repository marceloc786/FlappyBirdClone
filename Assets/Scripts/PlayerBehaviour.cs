using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public Transform mesh;
    public float forceFly;
    public Rigidbody2D rigidBird;
    private Animator animatorPlayer;

    private float currentTimeToAnim;
    private bool inAnim = true;
    private GameController gameController;
    public SoundController soundController;
    private PauseController pauseController;

	// Use this for initialization
	void Start () {
        animatorPlayer = mesh.GetComponent<Animator>();
        rigidBird = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        pauseController = FindObjectOfType(typeof(PauseController)) as PauseController;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.GAMEOVER &&
            !pauseController.IsPaused()) {
            inAnim = true;
            rigidBird.velocity = Vector2.zero;
           // rigidbody2D.AddForce(new Vector2(0, 1) * forceFly); //nao funciona mais precisa criar a referencia e estanciar
            rigidBird.AddForce(new Vector2(0, 1) * forceFly);
            soundController.PlaySound(soundsGame.wing);
        }
        
        else if (Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.TUTORIAL)
        {
            Restart();
        }

        animatorPlayer.SetBool("callFly", inAnim);

        Vector3 playerPosition = transform.position;
        if (playerPosition.y > 4.8f)
        {
            playerPosition.y = 4.8f;
            transform.position = playerPosition;
        }

        else if (gameController.GetCurrentState() == GameStates.TUTORIAL)
        {
            inAnim = true;
        }


        if (gameController.GetCurrentState() != GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.GAMEOVER)
        {
            rigidBird.gravityScale = 0;
            return;
        }
        else
        {
            rigidBird.gravityScale = 1;
        }

        if (inAnim && gameController.GetCurrentState() != GameStates.TUTORIAL) {
            currentTimeToAnim += Time.deltaTime;

            if(currentTimeToAnim > 0.333f) {
                currentTimeToAnim = 0;
                inAnim = false;
            }
        }

        

        if (gameController.GetCurrentState() == GameStates.INGAME)
        {

            if (rigidBird.velocity.y < 0)
            {
                mesh.eulerAngles -= new Vector3(0, 0, 5f);
                if (mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
                {
                    mesh.eulerAngles = new Vector3(0, 0, 270);
                }
            }
            else if (rigidBird.velocity.y > 0)
            {
                mesh.eulerAngles += new Vector3(0, 0, 2f);
                if (mesh.eulerAngles.z > 30)
                {
                    mesh.eulerAngles = new Vector3(0, 0, 30);
                }
            }
        }
        else if (gameController.GetCurrentState() == GameStates.GAMEOVER)
        {
            mesh.eulerAngles -= new Vector3(0, 0, 5f);
            if (mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
            {
                mesh.eulerAngles = new Vector3(0, 0, 270);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameController.CallGameOver();
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Perdeu playboy");
        gameController.CallGameOver();
    }

    public void RestartRotation ()
    {
        mesh.eulerAngles = new Vector3(0, 0, 0);
    }

    public void Restart ()
    {
        if (gameController.GetCurrentState() != GameStates.GAMEOVER)
        {
            gameController.ResetGame();
            gameController.StartGame();
        }
    }
}
