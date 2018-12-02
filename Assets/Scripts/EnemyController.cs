using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody body;
    public Transform target;
    public Transform suspension;
    public Transform model;

    public float throttleMin = 800;
    public float throttleMax = 2000;
    public float turnMultiplier = 100;

    /// <summary>
    /// The target distance from the player along the z axis
    /// </summary>
    float offset = 10;
    

    bool isGrounded = false;

    void Start() {
        body = GetComponent<Rigidbody>();
    }

    void Update() {
        RaycastHit hit;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f));

        if (isGrounded) UpdateGrounded(hit);
        else UpdateAir();

    }

    void UpdateGrounded(RaycastHit hit) {
        Vector3 forward = Vector3.Cross(Vector3.right, hit.normal);
        float yaw = Drive(forward);
        SetModelPosAndRot(Quaternion.FromToRotation(Vector3.up, hit.normal), yaw);
    }

    void UpdateAir() {
        float yaw = Drive(Vector3.forward);
        float pitch = -body.velocity.y * 2;

        SetModelPosAndRot(Quaternion.Euler(pitch, 0, 0), yaw);
    }

    
    //cut player off?
    //set player into tailspin?
    //knock player of edge or into wall?
    

    float Drive(Vector3 forward) {
        if (!target) return 0;
        float dist = target.position.z - transform.position.z; //how far away is the player

        float v = dist / offset; // divide that by the desierd offset

        float throttle = Mathf.Lerp(throttleMin, throttleMax, v);

        Vector3 force = forward * throttle;//foreward speed

        Debug.DrawRay(transform.position, forward);

        body.AddForce(force * Time.deltaTime);

        float turnAngle = Mathf.Atan2(body.velocity.x, body.velocity.z) * 180 / Mathf.PI;
        return turnAngle;
       
    }

    void SetModelPosAndRot(Quaternion rot, float turn) {
        float rotateSpeed = isGrounded ? 120 : 40; // the maximum number of degrees to rotate per second

        suspension.position = transform.position; // make the model follow the hamster wheel! ////////////////////// NOTE: If suspension is a child of the veichle do we need thi? 
        suspension.rotation = Quaternion.RotateTowards(suspension.rotation, rot, rotateSpeed * Time.deltaTime);
        if (model) model.localEulerAngles = new Vector3(0, turn, 0);
    }
}
