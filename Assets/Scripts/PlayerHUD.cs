using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    public Transform speedNeedle;
    public Image[] barrels;

    void Update()
    {
        UpdateFuelGauge();
    }

    private void UpdateFuelGauge()
    {
        float fuelPerBarrel = 10;
        float fuel = PlayerController.main.currentFuel;
        int fullBarrels = (int)(fuel / fuelPerBarrel);
        float percentOfLastBarrel = (fuel - fullBarrels * fuelPerBarrel) / fuelPerBarrel;

        for (int i = 0; i < barrels.Length; i++)
        {
            if (fullBarrels > i)
            {
                barrels[i].gameObject.SetActive(true);
                barrels[i].fillAmount = 1;
            }
            else if (fullBarrels == i)
            {
                barrels[i].gameObject.SetActive(true);
                barrels[i].fillAmount = percentOfLastBarrel;
            }
            else
            {
                barrels[i].gameObject.SetActive(false);
            }
        }
    }
}
