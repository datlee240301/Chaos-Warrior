using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyBar : MonoBehaviour {
    public static PlayerEnergyBar instance;
    public Slider slider;
    private float elapsedTime = 0f;
    private float timeInterval = 3f;
    private float increaseAmount = 100f;

    private void Awake() {
        instance = this;    
    }
    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        slider.value = 1000;
        slider.maxValue = 1000;
    }

    // Update is called once per frame
    void Update() {
        // Tăng giá trị slider mỗi 3 giây
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeInterval) {
            slider.value += increaseAmount;
            elapsedTime = 0f;
        }

        // Đảm bảo giá trị slider không vượt quá giá trị tối đa
        slider.value = Mathf.Clamp(slider.value, 0, slider.maxValue);
    }
}
