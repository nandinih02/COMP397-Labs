using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class UIControls : MonoBehaviour
{
    public Slider Healthbar;
    public TMP_Text HealthBarValue;
    public void Start()
    {
        Healthbar.value = 100;
    }
    public void OnHealthBar_Changed()
    {

        Debug.Log("Health Bar Value Changed");
        HealthBarValue.text = Healthbar.value.ToString();
    }

    public void TakeDamage(int damage)
    {
        int health = Int32.Parse(Healthbar.value.ToString());
        health -= damage;
        Healthbar.value = health;
    }
}
