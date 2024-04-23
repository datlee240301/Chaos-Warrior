using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public float pushForce;
    private Rigidbody2D rb;
    public GameObject ligghtningEffect;
    public Transform spawnPoint;
    private int hitCount = 0;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<Knight>().animator.SetBool(AnimationStrings.isKnightHit, true);
            FindObjectOfType<Knight>().StartCoroutine(FindObjectOfType<Knight>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Genie")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            GenieController.instance.moveSpeed = 0;
            GenieHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<GenieController>().animator.SetBool("isHurt", true);
            FindObjectOfType<GenieController>().StartCoroutine(FindObjectOfType<GenieController>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Red Monster")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            RedMonsterController.instance.moveSpeed = 0;
            RedMonsterHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<RedMonsterController>().animator.SetBool("isHurt", true);
            FindObjectOfType<RedMonsterController>().StartCoroutine(FindObjectOfType<RedMonsterController>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Medusa")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            MedusaController.instance.moveSpeed = 0;
            MedusaHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<MedusaController>().animator.SetBool("isHurt", true);
            FindObjectOfType<MedusaController>().StartCoroutine(FindObjectOfType<MedusaController>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Lizard")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            LizardController.instance.moveSpeed = 0;
            LizardHealthbar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            FindObjectOfType<LizardController>().animator.SetBool("isHurt", true);
            FindObjectOfType<LizardController>().StartCoroutine(FindObjectOfType<LizardController>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Troll2")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            Troll2Controller.instance.moveSpeed = 0;
            Troll2HealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            hitCount++;
            //FindObjectOfType<Troll2Controller>().animator.SetBool("isHurt", true);
            FindObjectOfType<Troll2Controller>().StartCoroutine(FindObjectOfType<Troll2Controller>().ExitStatus());
        }else if (collision.gameObject.CompareTag("Wizard")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
            WizardController.instance.moveSpeed = 0;
            WizardHealthBar.instance.slider.value -= 100f;
            Vector2 pushDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            hitCount++;
            FindObjectOfType<WizardController>().animator.SetBool("isHurt", true);
            FindObjectOfType<WizardController>().StartCoroutine(FindObjectOfType<WizardController>().ExitStatus());
        } else if (collision.gameObject.CompareTag("Ground")) {
            GameObject light = Instantiate(ligghtningEffect, spawnPoint.position, spawnPoint.rotation);
            Destroy(light, 0.35f);
            Destroy(gameObject);
        }
    }
}