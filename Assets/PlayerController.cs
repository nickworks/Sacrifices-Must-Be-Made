using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody body;
    public Transform suspension;
    public Transform model;

    public float throttleMin = 10;
    public float throttleMax = 100;
    public float turnMultiplier = 100;

    bool isGrounded = false;

    void Start () {
        body = GetComponent<Rigidbody>();	
	}
	void Update () {
        RaycastHit hit;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f));

        if (isGrounded) UpdateGrounded(hit);
        else UpdateAir();
        
    }
    void UpdateGrounded(RaycastHit hit)
    {
        Vector3 forward = Vector3.Cross(Vector3.right, hit.normal);
        float yaw = Drive(forward);
        SetModelPosAndRot(Quaternion.FromToRotation(Vector3.up, hit.normal), yaw);
    }
    void UpdateAir()
    {
        float yaw = Drive(Vector3.forward);
        float pitch = -body.velocity.y * 2;

        SetModelPosAndRot(Quaternion.Euler(pitch, 0, 0), yaw);
    }
    float Drive(Vector3 forward)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        forward.x += h * turnMultiplier;

        float throttle = Mathf.Lerp(throttleMin, throttleMax, v);
        Vector3 force = forward * throttle;
        
        Debug.DrawRay(transform.position, forward);

        body.AddForce(force * Time.deltaTime);

        float turnAngle = Mathf.Atan2(body.velocity.x, body.velocity.z) * 180 / Mathf.PI;
        return turnAngle;
    }

    void SetModelPosAndRot(Quaternion rot, float turn)
    {
        float rotateSpeed = isGrounded ? 120 : 40; // the maximum number of degrees to rotate per second

        suspension.position = transform.position; // make the model follow the hamster wheel!
        suspension.rotation = Quaternion.RotateTowards(suspension.rotation, rot, rotateSpeed * Time.deltaTime);
        if(model) model.localEulerAngles = new Vector3(0, turn, 0);
    }
}
