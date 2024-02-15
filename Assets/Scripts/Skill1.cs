using UnityEngine;
using UnityEngine.UI;

public class Skill1 : MonoBehaviour
{
    public static Skill1 Instance;
    public Slider slider;

    private void Awake() {
        Instance = this;
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 5;
        slider.minValue = 0;
        slider.value = slider.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= Time.deltaTime;
    }
}
