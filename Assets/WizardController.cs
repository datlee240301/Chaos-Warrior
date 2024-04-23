using System.Collections;
using UnityEngine;

public class WizardController : MonoBehaviour {
    public static WizardController instance;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance;

    public GameObject magic;
    public Transform shootingPoint;
    public float speed = 10f;

    private void Awake() {
        instance = this;
    }

    void Start() {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
    }

    private void Update() {
        if (WizardHealthBar.instance.slider.value <= 0) {
            animator.SetBool("isDie", true);
            //animator.SetBool("isWalk", false);
            //animator.SetBool("isAttack1", false);
            Destroy(gameObject, 2f);
            moveSpeed = 0f;
        }
        if (WizardHealthBar.instance.slider.value <= 500) {
            StopAllCoroutines();
            SpawnFireball();
            //SpawnFireball();
            
        }
    }

    IEnumerator MoveRoutine() {
        while (true) {
            if (movingRight) {
                while (transform.position.x < startPos.x + 3f) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack2", true);
                        animator.SetBool("isWalk", false);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = false;
                        break;
                    }
                    animator.SetBool("isAttack2", false);
                    animator.SetBool("isWalk", true);
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    transform.localScale = new Vector2(1, 1);
                    yield return null;
                }
                animator.SetBool("isWalk", false);
                yield return new WaitForSeconds(waitTime);
                animator.SetBool("isWalk", true);
                movingRight = false;
            } else {
                while (transform.position.x > startPos.x - 3f) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack2", true);
                        animator.SetBool("isWalk", false);
                        //animator.SetBool("isAttack", true);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = true;
                        break;
                    }
                    animator.SetBool("isAttack2", false);
                    animator.SetBool("isWalk", true);
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    transform.localScale = new Vector2(-1, 1);
                    yield return null;
                }
                animator.SetBool("isWalk", false);
                yield return new WaitForSeconds(waitTime);
                animator.SetBool("isWalk", true);
                movingRight = true;
            }
        }
    }
    public void SpawnFireball() {
        animator.SetBool("isAttack2", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isAttack1", true);
    }
    //IEnumerator MoveRoutine2() {
    //    while (true) {
    //        if (movingRight) {
    //            while (transform.position.x < startPos.x + 3f) {
    //                if (IsPlayerNearby()) {
    //                    animator.SetBool("isAttack", false);
    //                    animator.SetBool("isWalk", false);
    //                    TurnTowardsPlayer();
    //                    yield return new WaitForSeconds(waitTime);
    //                    movingRight = false;
    //                    break;
    //                }
    //                animator.SetBool("isAttack", false);
    //                animator.SetBool("isWalk", true);
    //                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    //                transform.localScale = new Vector2(1, 1);
    //                yield return null;
    //            }
    //            animator.SetBool("isWalk", false);
    //            yield return new WaitForSeconds(waitTime);
    //            animator.SetBool("isWalk", true);
    //            movingRight = false;
    //        } else {
    //            while (transform.position.x > startPos.x - 3f) {
    //                if (IsPlayerNearby()) {
    //                    animator.SetBool("isAttack", false);
    //                    animator.SetBool("isWalk", false);
    //                    //animator.SetBool("isAttack", true);
    //                    TurnTowardsPlayer();
    //                    yield return new WaitForSeconds(waitTime);
    //                    movingRight = true;
    //                    break;
    //                }
    //                animator.SetBool("isAttack", false);
    //                animator.SetBool("isWalk", true);
    //                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    //                transform.localScale = new Vector2(-1, 1);
    //                yield return null;
    //            }
    //            animator.SetBool("isWalk", false);
    //            yield return new WaitForSeconds(waitTime);
    //            animator.SetBool("isWalk", true);
    //            movingRight = true;
    //        }
    //    }
    //}

    bool IsPlayerNearby() {
        if (playerTransform != null) {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            return distance <= stoppingDistance;
        }
        return false;
    }

    void TurnTowardsPlayer() {
        Vector3 direction = playerTransform.position - transform.position;
        if (direction.x > 0) {
            transform.localScale = new Vector2(1, 1);
        } else {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public IEnumerator ExitStatus() {
        yield return new WaitForSeconds(1);
        animator.SetBool("isHurt", false);
        moveSpeed = 3;
    }

    public void MakeFireball() {
        PlayerShoot.instance.SpawnFireball();
    }

    public IEnumerator Shoot() {
        GameObject arrow = GameObject.Instantiate(magic, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D arroRb = arrow.GetComponent<Rigidbody2D>();
        Vector2 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector2(originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y);
        arroRb.velocity = new Vector2(speed * arrow.transform.localScale.x, arrow.transform.localScale.y);
        yield return new WaitForSeconds(2);
        Destroy(arrow);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerHitbox")) {
            WizardHealthBar.instance.slider.value -= 200f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            WizardHealthBar.instance.slider.value -= 600f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        }
    }
}
