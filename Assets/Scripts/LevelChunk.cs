using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour {

    public Transform endOfChunk;

    [Range(0,1)]
    public float chanceOfFuel = .5f;

    void Start () {
        Limit<PickupController>(chanceOfFuel);
        Limit<ObstacleSpawner>(.5f);
	}
    void Limit<T>(float percent)
    {
        List<T> objs = new List<T>(GetComponentsInChildren<T>());
        int numberOfBarrels = (int)(percent * objs.Count);

        while (objs.Count > numberOfBarrels)
        {
            int index = Random.Range(0, objs.Count);
            Destroy((objs[index] as Behaviour).gameObject);
            objs.RemoveAt(index);
        }
    }

	void Update () {
		
	}
}
