using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour {

    public PlayerHUD prefabHUD;
    static PlayerHUD hud;
    Rigidbody body;

    void Start()
    {
        if (!hud) hud = Instantiate(prefabHUD);
        body = GetComponent<Rigidbody>();
    }
    void OnDestroy()
    {
        if (hud) Destroy(hud.gameObject);
    }
}
