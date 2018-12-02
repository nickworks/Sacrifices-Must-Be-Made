using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Rigidbody body;
    Rigidbody targetBody;
    public Transform target;
    public Transform suspension;
    public Transform model;

    public float throttleMin = 800;
    public float throttleMax = 2000;
    public float turnMultiplier = 100;

    //Vector3 vector3 = new Vector3(0, 0, 1);

    /// <summary>
    /// The target distance from the player along the z axis
    /// </summary>
    float offset = 10;

    bool isGrounded = false;


    enum AIStates {
        chase,
        coast,
        cutoff
        //cut player off?
        //set player into tailspin?
        //knock player of edge or into wall?
        // Cut Off: If we are in front of the player, move in their direction along the X-Axis
        // Suicide Dash/Push POC: If we are close to the player and moveing at a similar velocity, move twoard them along the X-Axis 


    }

    AIStates state = AIStates.chase;


    void Start() {
        body = GetComponent<Rigidbody>();

        if (!target) return;
        targetBody = target.GetComponent<Rigidbody>();
    }

    void Update() {
        if (!target) return;

        RaycastHit hit;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f));

        Vector3 forward;

        if (isGrounded) {
            forward = Vector3.Cross(Vector3.right, hit.normal);
        } else {
            forward = Vector3.forward;
        }


        switch (state) {
            case AIStates.chase:
                Chase(forward);
                break;
            case AIStates.coast:

                break;
            default:
                print("Error: AI Statemachine in EnemyController.cs is out of bounds");
                break;

        }

        HandleGroundedRotBehavior(hit, CalcYaw());
    }


    /// <summary>
    /// This function sets diffred rotation veluus depending on whether or not we are grounded 
    /// </summary>
    /// <param name="hit">The raycast thet detectrs the ground, used to get the normals</param>
    /// <param name="yaw">our yaw baised on the forward velocity</param>
    void HandleGroundedRotBehavior(RaycastHit hit, float yaw) {

        Quaternion rot;

        if (isGrounded) {
            rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        } else {
           float pitch = -body.velocity.y * 2;
           rot = Quaternion.Euler(pitch, 0, 0);

        }

        SetModelPosAndRot(rot,yaw);
    }


   
    void Chase(Vector3 forward) {
        float dist = target.position.z - transform.position.z; //how far away is the player

        float v = dist / offset;// divide that by the desierd offset

        float throttle = Mathf.Lerp(throttleMin, throttleMax, v);

        Vector3 force = forward * throttle;//forward speed

        body.AddForce(force * Time.deltaTime);
        print(body.velocity);
        
    }

    void Coast(Vector3 forward) {

    }


    /// <summary>
    /// This function calculates the direction we are moving and converts that to a rotation value
    /// </summary>
    /// <returns>Our yaw baised on our forward velocity</returns>
    float CalcYaw() {
        float turnAngle = Mathf.Atan2(body.velocity.x, body.velocity.z) * 180 / Mathf.PI;
        return turnAngle;
    }


    /// <summary>
    /// This function positions and rotates the rendered mesh, it is called in HandleGroundedRotBehavior()
    /// </summary>
    /// <param name="rot">The rotation we want to apply to the mesh</param>
    /// <param name="turn">The curent yaw according to our velocity</param>
    void SetModelPosAndRot(Quaternion rot, float turn) {
        float rotateSpeed = isGrounded ? 120 : 40; // the maximum number of degrees to rotate per second

        suspension.position = transform.position; // make the model follow the hamster wheel! 
        suspension.rotation = Quaternion.RotateTowards(suspension.rotation, rot, rotateSpeed * Time.deltaTime);
        if (model) model.localEulerAngles = new Vector3(0, turn, 0);
    }
}
