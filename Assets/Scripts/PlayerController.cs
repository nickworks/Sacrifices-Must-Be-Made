using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    static public PlayerController main;

    public float attackCost = 5;
    public float totalFuel = 100;
    private static float currentFuel = 100;
    

    public Rigidbody body;
    public Transform suspension;
    public Transform model;

    public float throttleMin = 800;
    public float throttleMax = 2000;
    public float throttleMaxAir = 1500;
    public float turnMultiplier = 1;

    bool isGrounded = false;
    bool isDead = false;

    Vector3 forward = Vector3.forward;
    Vector3 up = Vector3.up;

    public static float getFuel()
    {
        return currentFuel;
    }
    public static void setFuel(float newFuel)
    {
       if(currentFuel + newFuel > 100)
        {
            currentFuel = 100;
        }
            currentFuel += newFuel;
    }

    void Start () {
        main = this;
        body = GetComponent<Rigidbody>();	
	}

	void Update ()
    {
        setFuel(-1 * Time.deltaTime);
        print(getFuel());
        if (isDead) return;
        DetectGround();

        if (isGrounded) UpdateGrounded();
        else UpdateAir();

    }
  
    private void DetectGround()
    {
        RaycastHit hit;
        isGrounded = (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f));

        // Set the forward and up vectors for the vehicle:
        forward = isGrounded ? Vector3.Cross(Vector3.right, up) : Vector3.forward;
        up = isGrounded ? hit.normal : Vector3.up;
    }

    void UpdateGrounded()
    {
        float yaw = Drive();
        SetModelPosAndRot(Quaternion.FromToRotation(Vector3.up, up), yaw);
    }

    void UpdateAir()
    {
        float yaw = Drive();
        float pitch = -body.velocity.y * 2;

        SetModelPosAndRot(Quaternion.Euler(pitch, 0, 0), yaw);
    }

    float Drive()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        forward.x += h * turnMultiplier;

        float throttle = Mathf.Lerp(throttleMin, isGrounded ? throttleMaxAir : throttleMax, v);
        Vector3 force = forward * throttle;
        
        Debug.DrawRay(transform.position, forward);

        body.AddForce(force * Time.deltaTime);
        //print("Player: " + body.velocity);


        float turnAngle = Mathf.Atan2(body.velocity.x, body.velocity.z) * 180 / Mathf.PI;
        return turnAngle;
    }

    void SetModelPosAndRot(Quaternion rot, float turn)
    {
        float rotateSpeed = isGrounded ? 180 : 40; // the maximum number of degrees to rotate per second

        suspension.position = transform.position; // make the model follow the hamster wheel! ////////////////////// NOTE: If suspension is a child of the veichle do we need thi? 
        suspension.rotation = Quaternion.RotateTowards(suspension.rotation, rot, rotateSpeed * Time.deltaTime);
        if(model) model.localEulerAngles = new Vector3(0, turn, 0);
    }
}
