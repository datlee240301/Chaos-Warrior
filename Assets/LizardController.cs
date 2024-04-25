using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LizardController : MonoBehaviour
{
    public static LizardController instance;
    public Slider slider;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance;
    public float distanceToHit;

    private void Awake() {
        instance = this;
        slider = GetComponent<Slider>();
    }

    void Start() {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
        slider.value = slider.maxValue;
    }

    private void Update() {
        //if (PlayerHealthBar.instance.slider.value <= 0) {
        //    StopAllCoroutines();
        //    StartCoroutine(MoveRoutine2());
        //}
        if (slider.value <= 0) {
            animator.SetBool("isDie", true);
            Destroy(gameObject, 2f);
            moveSpeed = 0f;
        }
    }

    IEnumerator MoveRoutine() {
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
                        animator.SetBool("isWalk", false);
                        animator.SetBool("isAttack", true);
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
    IEnumerator MoveRoutine2() {
        while (true) {
            if (movingRight) {
                while (transform.position.x < startPos.x + distanceToHit) {
                    if (IsPlayerNearby()) {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerHitbox")) {
            LizardHealthbar.instance.slider.value -= 200f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            LizardHealthbar.instance.slider.value -= 600f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Arrow")) {
            animator.SetBool("isHurt", true);
            slider.value -= 100;
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        }
    }
}
