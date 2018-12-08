using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour {

    public GameObject prefabBarrel;
    public Transform spawnPoint;
    public float fuelPerBarrelTossed = 10;
    public Transform cursor;
    public AnimationCurve curve;

    // Update is called once per frame
    void LateUpdate()
    {
        if(PlayerInput.mode == InputMode.Gamepad)
        {
            cursor.parent = PlayerController.main.suspension;
            AimWithAnalog();
        }
        if (PlayerInput.mode == InputMode.MouseKeyboard)
        {
            cursor.parent = null;
            AimWithMouse();
        }
        DrawAimPath();
    }

    private void DrawAimPath()
    {
        Vector3[] pts = new Vector3[PlayerController.main.line.positionCount];
        int centerIndex = pts.Length / 2;

        float height = 2 * (transform.position - cursor.position).sqrMagnitude / 100;

        for (int i = 0; i < pts.Length; i++)
        {
            int max = pts.Length - 1;
            float p = i / (float)max;
            Vector3 pt = Vector3.Lerp(transform.position, cursor.position, p);
            pt.y += curve.Evaluate(p) * height;
            pts[i] = pt;
        }

        PlayerController.main.line.SetPositions(pts);
    }

    private void AimWithAnalog()
    {
        float aimAxisH = Input.GetAxis("Horizontal2");
        float aimAxisV = Input.GetAxis("Vertical2");

        Vector3 target = new Vector3(aimAxisH, 0, aimAxisV);
        if (target.sqrMagnitude > 1) target.Normalize();
        target *= 10;
        bool deadZone = (target.sqrMagnitude < .1f); // deadZone
        if (aimAxisV > 0) target.z *= 2;
        if (deadZone) return;

        bool aimFurtherOut = (target.sqrMagnitude >= cursor.localPosition.sqrMagnitude);
        float inputAlignAmount = Vector3.Dot(target, cursor.localPosition);
        bool letsAimThisThing = (aimFurtherOut || inputAlignAmount < .5f || target.sqrMagnitude > .8f);
        if (letsAimThisThing)
        {
            cursor.localPosition += (target - cursor.localPosition) * Time.deltaTime * 4;
        }
        cursor.rotation = Quaternion.identity;

    }

    private void AimWithMouse()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //if (mx == 0 || my == 0) return;
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

