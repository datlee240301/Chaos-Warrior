using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Transform playerTransform;
    public float followSpeed = 3f;
    public float stoppingDistance = 5f; // Khoảng cách mà nhân vật sẽ ngừng di chuyển khi gần Player

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (playerTransform != null) {
            // Tính toán khoảng cách giữa nhân vật và Player trên trục X
            float distanceX = playerTransform.position.x - transform.position.x;

            // Nếu khoảng cách vượt quá khoảng cách dừng, di chuyển nhân vật
            if (Mathf.Abs(distanceX) > stoppingDistance) {
                // Xác định hướng di chuyển
                float directionX = Mathf.Sign(distanceX);

                // Di chuyển nhân vật
                transform.Translate(Vector3.right * directionX * followSpeed * Time.deltaTime);
            }
        }
    }
}