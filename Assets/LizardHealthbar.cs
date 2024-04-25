using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LizardHealthbar : MonoBehaviour
{
    public static LizardHealthbar instance;
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
        if (slider.value <= 0) {
            LizardController.instance.animator.SetBool("isDie", true);
            //LizardController.instance.Destroy(LizardController.instance.gameObject, 2f);
            LizardController.instance.moveSpeed = 0f;
        }
    }
}
