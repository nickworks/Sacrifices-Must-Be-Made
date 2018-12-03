using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    public GameObject prefabBarrel;
    public Transform cam;
    public float timer;

    public int spawnAmount;
	// Use this for initialization
	void Start () {
        SpawnPickups(spawnAmount = Random.Range(1, 10));

	}
	
	// Update is called once per frame
	void Update () {
        timer -= 1* Time.deltaTime;
        if(timer <= 0)
        {
            SpawnPickups(spawnAmount = Random.Range(1, 10));
            ResetTimer();
        }
    }
    void ResetTimer()
    {
        timer = Random.Range(1, 15);
    }
    void SpawnPickups(int spawnAmount)
    {
        //print("WHAT");
        for (int i = 0; i < spawnAmount; i ++)
        {
            
            Vector3 pos = Vector3.zero;
            pos.x += Random.Range(-20, 20);
            pos.z = cam.position.z + Random.Range(50,100);
            pos.y = cam.position.y;
            GameObject.Instantiate(prefabBarrel, pos, Quaternion.identity);
        }
    }
}
