using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    static public PlayerController main;
    static public float score = 0;

    public float attackCost = 5;
    public float maximumFuel = 100;
    public float currentFuel { get; private set; }


    public Rigidbody body { get; private set; }
    public Transform suspension;
    public Transform model;

    public float throttleMin = 800;
    public float throttleMax = 2000;
    public float throttleMaxAir = 1500;
    public float turnMultiplier = 1;

    bool isGrounded = false;

    Vector3 forward = Vector3.forward;
    Vector3 up = Vector3.up;

    public void AddFuel(float delta)
    {
        currentFuel += delta;
        currentFuel = Mathf.Clamp(currentFuel, 0, maximumFuel);
    }

    void Start () {
        score = 0;
        main = this;
        body = GetComponent<Rigidbody>();
        currentFuel = maximumFuel;
	}

	void Update ()
    {
        AddFuel(-1 * Time.deltaTime); // lose 1 fuel per second
        
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
        if (currentFuel <= 0) return 0;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        forward.x += h * turnMultiplier;

        float throttle = Mathf.Lerp(throttleMin, isGrounded ? throttleMax : throttleMaxAir, v);
        Vector3 force = forward * throttle;
        
        //Debug.DrawRay(transform.position, forward);

        body.AddForce(force * Time.deltaTime);

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
