using UnityEngine;

public class NoticeManager : MonoBehaviour {
    public static NoticeManager instance;
    public GameObject noticePanel ,skill1NoticeText, bowNoticeText;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        if(skill1NoticeText.activeSelf) bowNoticeText.SetActive(false);
        else bowNoticeText.SetActive(true);
    }

    public void ShowSkill1NoticeText() {
        noticePanel.SetActive(true);
        skill1NoticeText.SetActive(true);
    }
    
    public void ShowBowNoticeText() {
        noticePanel.SetActive(true);
        bowNoticeText.SetActive(true);
    }
}
