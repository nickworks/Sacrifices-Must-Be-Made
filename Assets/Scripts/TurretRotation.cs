using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour {

    public GameObject prefabBarrel;
    public Transform spawnPoint;
    public float fuelPerBarrelTossed = 10;
    public Transform cursor;
   
    // Update is called once per frame
    void FixedUpdate ()
    {
        AimWithAnalog();

        AimWithMouse();
    }

    private void AimWithAnalog()
    {
        float aimAxisH = Input.GetAxis("Horizontal2");
        float aimAxisV = Input.GetAxis("Vertical2");
        print(aimAxisH);
        transform.localEulerAngles += new Vector3(0, aimAxisH, 0) * Time.deltaTime * 1000;
    }

    private void AimWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // make a ray
        Plane aimPlane = new Plane(Vector3.up, transform.position); // make a plane

        float rayLength = 0;
        if (aimPlane.Raycast(ray, out rayLength)) // detect if the ray intersects the plane
        {
            Vector3 hit = ray.GetPoint(rayLength); // detect where the intersection is

            Vector3 dis = transform.position - hit;
            float yaw = -Mathf.Atan2(dis.z, dis.x) * 180 / Mathf.PI;
            float parentYaw = transform.parent.eulerAngles.y;
            transform.localEulerAngles = new Vector3(0, yaw - parentYaw, 0);
            cursor.position = hit;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerController.main.currentFuel > fuelPerBarrelTossed)
            {

                GameObject obj = Instantiate(prefabBarrel, spawnPoint.position, Quaternion.identity);
                Vector3 dir = spawnPoint.position - transform.position;

                Rigidbody barrel = obj.GetComponent<Rigidbody>();
                barrel.velocity += PlayerController.main.ballBody.velocity; // inherit the car's velocity

                barrel.AddForce(dir * 20, ForceMode.Impulse); // push the barrel
                barrel.AddTorque(Random.onUnitSphere * 10); // random spin

                PlayerController.main.AddFuel(-fuelPerBarrelTossed); // lose fuel
            }
        }
    }
}

