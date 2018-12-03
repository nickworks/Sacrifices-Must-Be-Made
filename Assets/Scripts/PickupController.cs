using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public float fuelAmount = 10;

	void Start () {
        
	}
	void Update () {
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController.main.AddFuel(10);
        }
    }
}
