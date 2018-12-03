using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour {

    public Transform endOfChunk;

    public GameObject prefabFuel;

    [Range(0,1)]
    public float chanceOfFuel = .5f;
    public Transform[] fuelSpawnPoints;
    public Transform[] moneySpawnPoints;

    void Start () {
        SpawnFuel();
	}
    void SpawnFuel()
    {
        int numberOfBarrels = (int)(chanceOfFuel * fuelSpawnPoints.Length);
        List<Transform> spots = new List<Transform>(fuelSpawnPoints);
        for(int i = 0; i < numberOfBarrels; i++)
        {
            int index = Random.Range(0, spots.Count);
            Transform winner = spots[index];
            spots.RemoveAt(index);
            Instantiate(prefabFuel, winner.position, winner.rotation, transform);
        }


    }
	
	void Update () {
		
	}
}
