using UnityEngine;

public class CameraShake : MonoBehaviour {
    public Transform camTransform; // Tham chiếu đến transform của camera
    public float shakeDuration = 0f; // Thời gian rung
    public float shakeMagnitude = 0.7f; // Độ lớn của rung
    public float dampingSpeed = 1.0f; // Tốc độ giảm của rung

    Vector3 initialPosition; // Vị trí ban đầu của camera
    Quaternion initialRotation; // Hướng ban đầu của camera

    void Start() {
        if (camTransform == null) {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        initialPosition = camTransform.localPosition;
        initialRotation = camTransform.localRotation;
    }

    void Update() {
        //Shake();
        if (shakeDuration > 0) {
            // Rung
            camTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else {
            // Dừng rung và trở lại vị trí ban đầu
            shakeDuration = 0f;
            camTransform.localPosition = initialPosition;
            camTransform.localRotation = initialRotation;
        }
    }

    // Hàm để bắt đầu rung
    public void Shake() {
        shakeDuration = 0.5f;
    }
}
