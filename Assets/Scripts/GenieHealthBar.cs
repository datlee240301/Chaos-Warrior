using UnityEngine;
using UnityEngine.UI;

public class GenieHealthBar : MonoBehaviour
{
    public static GenieHealthBar instance;
    public Slider slider;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 1000;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
