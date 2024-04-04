using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Troll2HealthBar : MonoBehaviour {
    public static Troll2HealthBar instance;
    public Slider slider;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        slider.maxValue = 8000;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update() {

    }
}
