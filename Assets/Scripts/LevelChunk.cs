using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour {

    public Transform endOfChunk;

    public GameObject prefabFuel;

    [Range(0,1)]
    public float chanceOfFuel = .5f;

    void Start () {
        SpawnFuel();
	}
    void SpawnFuel()
    {
        List<PickupController> barrels = new List<PickupController>(GetComponentsInChildren<PickupController>());
        int numberOfBarrels = (int)(chanceOfFuel * barrels.Count);

        while(barrels.Count > numberOfBarrels)
        {
            int index = Random.Range(0, barrels.Count);
            Destroy(barrels[index].gameObject);
            barrels.RemoveAt(index);
        }
    }
	
	void Update () {
		
	}
}
