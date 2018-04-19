using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public float maxHeight;
    public float minHeight;
    public float rateSpawn;

    private float currentRateSpawn;

    public GameObject pipePrefab;

    public int maxSpawnPipes;

    public List<GameObject> pipes;

    private GameController gameController;

    // Use this for initialization
    void Start () {
		for (int i=0; i < maxSpawnPipes; i++)
        {
            GameObject tempPipes = Instantiate(pipePrefab);
            pipes.Add(tempPipes);
            tempPipes.SetActive(false);
        }
        currentRateSpawn = rateSpawn;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetCurrentState() != GameStates.INGAME)
        {
            return;
        }

        currentRateSpawn += Time.deltaTime;
        if (currentRateSpawn > rateSpawn)
        {
            currentRateSpawn = 0;
            Spawn();
        }

        
    }

    private void Spawn()
    {
        float randHeight = Random.Range(minHeight, maxHeight);
        GameObject tempPipes = null;
        for (int i = 0; i < maxSpawnPipes; i++)
        {
            if (pipes[i].activeSelf == false)
            {
                tempPipes = pipes[i];
                break;
            }
        }

        if (tempPipes != null)
        {
            tempPipes.transform.position = new Vector3(transform.position.x, randHeight, transform.position.z);
            tempPipes.SetActive(true);
        }
    }
}
