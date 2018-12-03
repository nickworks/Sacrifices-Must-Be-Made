using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {
    private GameObject cam;
    private Vector3 pos;
    public float distanceToDeletion;
	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        pos = this.transform.position;
       // print(pos.z);
        print("Cam pos" + cam.transform.position.z);
        if (pos.z < cam.transform.position.z - distanceToDeletion)
        {
            Destroy(gameObject);
        }
	}
}
