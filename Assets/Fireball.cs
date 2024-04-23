using UnityEngine;

public class Fireball : MonoBehaviour {
    Animator animator;
    public float speed = 5f;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            animator.SetTrigger("isExplode");
            speed = 0f;
        }   
    }

    public void DestroyObj() {
        Destroy(gameObject);
    }
}
