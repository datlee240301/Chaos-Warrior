using UnityEngine;

public class SkillEffect1 : MonoBehaviour {
    public GameObject explosion;
    public Transform spawnPos;
    private float speed = 40f;

    private void Update() {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void SpawnExplosion() {
        GameObject explodingEffect = Instantiate(explosion, spawnPos.position,spawnPos.rotation);
        Destroy(explodingEffect, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Enemy")) {
            speed = 0;
            SpawnExplosion();
        }
    }
}
