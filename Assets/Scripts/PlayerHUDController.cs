using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour {

    public PlayerHUD prefabHUD;
    static PlayerHUD hud;

    public float speedometerMaxVelocity = 80;

    Rigidbody body;
    float previousAngle = 0;

    void Start()
    {
        if (!hud) hud = Instantiate(prefabHUD);
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hud) return;

        float p = body.velocity.z / speedometerMaxVelocity;
        p = Mathf.Clamp(p, 0, 1);
        float angle = Mathf.Lerp(120, -118, p);
        float finalAngle = (angle + previousAngle) / 2;
        hud.speedNeedle.eulerAngles = new Vector3(0, 0, finalAngle);
        previousAngle = finalAngle;
    }
}
