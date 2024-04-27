using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Singleton<HealthBar> {

   // public static HealthBar Instance { get; private set; }

    private Slider healthSlider;

    protected override void Awake() {
        base.Awake();
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHeatlh) {
        healthSlider.maxValue = maxHeatlh;
        healthSlider.value = maxHeatlh;
    }

    public void SetHealth(int healthAmount) {
        healthSlider.value = healthAmount;
    }
}
