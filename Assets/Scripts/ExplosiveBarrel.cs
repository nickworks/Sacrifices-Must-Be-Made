using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

    
    public float age = 0;
    public float lifespan = 1;

    Rigidbody body;
    Vector3 prevVelocity = Vector3.zero;

    float threshold = 1000;
    public GameObject prefabExplosion;

    void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountdownTimer();
    }

    private void CountdownTimer()
    {
        age += Time.deltaTime;
        if (age >= lifespan)
        {
            Explode();
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        GameObject particles = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Destroy(particles, 2);
    }
}
