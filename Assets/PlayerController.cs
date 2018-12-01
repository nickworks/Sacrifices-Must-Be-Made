using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody body;

    public float throttleMin = 10;
    public float throttleMax = 100;
    public float turnMultiplier = 100;

    void Start () {
        body = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 force = new Vector3();
        force.x += h * turnMultiplier;
        force.z += Mathf.Lerp(throttleMin, throttleMax, v);
        
        body.AddForce(force * Time.deltaTime);
        float yaw = Mathf.Atan2(body.velocity.x, body.velocity.z);
        float pitch = -body.velocity.y / 15;
        transform.eulerAngles = new Vector3(pitch, yaw, 0) * 180 / Mathf.PI;
    }
}
