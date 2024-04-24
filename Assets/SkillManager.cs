using UnityEngine;

public class SkillManager : MonoBehaviour {
    public GameObject bowIcon;
    public GameObject skill1Icon;  
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (PlayerPrefs.GetInt("EnableBow") ==1) {
            bowIcon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Skill1") ==1) {
            skill1Icon.SetActive(true);
        }
    }
}
