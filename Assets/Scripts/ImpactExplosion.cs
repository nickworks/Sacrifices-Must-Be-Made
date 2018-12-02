using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactExplosion : MonoBehaviour {

    Rigidbody body;
    Vector3 prevVelocity;
    public float threshold = 0;
    public GameObject prefabExplosion;
    public bool ignoreVertical = true;
    public float spawnSafetyTimer = .1f;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
	void Update () {
        if(spawnSafetyTimer > 0)
            spawnSafetyTimer -= Time.deltaTime;
        else
            CheckForCrash();
	}
    private void CheckForCrash()
    {
        Vector3 deltaVelocity = body.velocity - prevVelocity;
        prevVelocity = body.velocity;
        if(ignoreVertical) deltaVelocity.y = 0;
    }
    void Explode()
    {
        Destroy(gameObject);
        GameObject particles = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Destroy(particles, 2);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.impulse.sqrMagnitude > threshold * threshold)
        {
            // dot product of impulse & back
            float multiplier = Vector3.Dot(col.impulse.normalized, Vector3.back);
            multiplier *= multiplier; // bend the curve

            if (col.impulse.sqrMagnitude * multiplier > threshold * threshold)
            {
                Explode();
            }
        }
    }
}
