using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedusaHealthBar : MonoBehaviour
{
    public static MedusaHealthBar instance;
    public Slider slider;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update() {

    }
}
