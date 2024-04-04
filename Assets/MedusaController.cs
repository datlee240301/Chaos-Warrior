using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class MedusaController : MonoBehaviour {
    public static MedusaController instance;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    private GameObject[] rockObjects;  // Thêm biến để lưu trữ vật thể Rock
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance;
    public float distanceToHit;

    private void Awake() {
        instance = this;
    }

    void Start() {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rockObjects = GameObject.FindGameObjectsWithTag("MedusaRock"); // Tìm vật thể Rock trong scene
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
    }

    private void Update() {
        if (MedusaHealthBar.instance.slider.value <= 0) {
            animator.SetBool("isDie", true);
            Destroy(gameObject, 2f);
            moveSpeed = 0f;
        }
    }

    public IEnumerator MoveRoutine() {
        while (true) {
            if (movingRight) {
                while (transform.position.x < startPos.x + distanceToHit) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack", true);
                        animator.SetBool("isWalk", false);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = false;
                        break;
                    }
                    animator.SetBool("isAttack", false);
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
                while (transform.position.x > startPos.x - distanceToHit) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack", true);
                        animator.SetBool("isWalk", false);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = true;
                        break;
                    }
                    animator.SetBool("isAttack", false);
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
    public IEnumerator MoveRoutine2() {
        while (true) {
            if (movingRight) {
                while (transform.position.x < startPos.x + distanceToHit) {
                    //if (IsPlayerNearby()) {
                    //    animator.SetBool("isAttack", true);
                    //    animator.SetBool("isWalk", false);
                    //    TurnTowardsPlayer();
                    //    yield return new WaitForSeconds(waitTime);
                    //    movingRight = false;
                    //    break;
                    //}
                    animator.SetBool("isAttack", false);
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
                while (transform.position.x > startPos.x - distanceToHit) {
                    //if (IsPlayerNearby()) {
                    //    animator.SetBool("isAttack", true);
                    //    animator.SetBool("isWalk", false);
                    //    TurnTowardsPlayer();
                    //    yield return new WaitForSeconds(waitTime);
                    //    movingRight = true;
                    //    break;
                    //}
                    animator.SetBool("isAttack", false);
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

    bool IsPlayerNearby() {
        if (playerTransform != null ) {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerHitbox")) {
            MedusaHealthBar.instance.slider.value -= 200f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            MedusaHealthBar.instance.slider.value -= 600f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        }
    }
}
