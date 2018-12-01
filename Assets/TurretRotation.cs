using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour {
    Camera mainCamera;

    public GameObject projectile;
    public Transform spawnPosition;
    Quaternion startingRotation;

    public float rayLength;
    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(rayLength);

            if(Physics.Raycast(ray.origin, rayPoint,  out hit))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }

            Debug.DrawLine(ray.origin, rayPoint, Color.red);

            

        }

    }


  
}
