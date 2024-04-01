using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public float pushForce;
    private Rigidbody2D rb;
    public GameObject ligghtningEffect;
    public Transform spawnPoint;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            GameObject light =  Instantiate(ligghtningEffect, spawnPoint.position,spawnPoint.rotation);
            Destroy(light,0.35f);
            Destroy(gameObject);
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<Knight>().animator.SetBool(AnimationStrings.isKnightHit, true);
            FindObjectOfType<Knight>().StartCoroutine(FindObjectOfType<Knight>().ExitStatus());
        }
        else if (collision.gameObject.CompareTag("Genie")) {
            GameObject light =  Instantiate(ligghtningEffect, spawnPoint.position,spawnPoint.rotation);
            Destroy(light,0.35f);
            Destroy(gameObject);
            GenieController.instance.moveSpeed = 0;
            GenieHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<GenieController>().animator.SetBool("isHurt", true);
            FindObjectOfType<GenieController>().StartCoroutine(FindObjectOfType<GenieController>().ExitStatus());
        }
        else if (collision.gameObject.CompareTag("Red Monster")) {
            GameObject light =  Instantiate(ligghtningEffect, spawnPoint.position,spawnPoint.rotation);
            Destroy(light,0.35f);
            Destroy(gameObject);
            RedMonsterControler.instance.moveSpeed = 0;
            RedMonsterHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<RedMonsterControler>().animator.SetBool("isHurt", true);
            FindObjectOfType<RedMonsterControler>().StartCoroutine(FindObjectOfType<RedMonsterControler>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Ground")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
        }
    }
}