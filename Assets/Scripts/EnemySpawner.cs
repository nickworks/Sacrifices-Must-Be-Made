using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

   static int numEnemys;
    int maxEnemys = 5;
    int minEnemys = 3;

    float spawnTimer = 0;
    

    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // print(numEnemys);
         if (numEnemys < maxEnemys && spawnTimer <= 0) {
            SpawnEnemy();
        } else if (spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
        }
	}


    void SpawnEnemy() {
        if (!PlayerController.main) return; 
        Vector3 position = PlayerController.main.transform.position;
        position.z -= Random.Range(10, 15);
        position.x += Random.Range(-10, 10);


        Instantiate(enemyPrefab,position,Quaternion.identity);
        numEnemys++;
        spawnTimer = Random.Range(numEnemys, numEnemys*2);
    }

    static public void EnemyDead() {
        numEnemys--;
    }



}
