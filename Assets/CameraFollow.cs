using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public string playerTag = "Player"; // Tag của gameobject cần theo dõi
    public Vector3 offset; // Độ lệch giữa vị trí camera và vị trí của player

    private GameObject player; // Biến để lưu trữ gameobject player

    void Start() {
        // Tìm gameobject có tag là "Player" và lưu trữ vào biến player
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    void LateUpdate() {
        if (player != null) {
            // Tính toán vị trí mới của camera dựa trên vị trí của player và offset
            Vector3 newPosition = player.transform.position + offset;

            // Cập nhật vị trí của camera đến vị trí mới
            transform.position = newPosition;
        }
    }
}