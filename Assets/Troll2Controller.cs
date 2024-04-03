using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll2Controller : MonoBehaviour {
    public static Troll2Controller instance;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance;
    private int hitCount = 0;
    bool hasPlayedEarthquake = false;

    private void Awake() {
        instance = this;
    }

    void Start() {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        //StartCoroutine(MoveRoutine());
        InvokeRepeating("MoveRoutine", 0f, 9f); // Gọi MoveRoutine mỗi 9 giây
        InvokeRepeating("RunRoutine", 3f, 9f); // Gọi RunRoutine mỗi 9 giây, bắt đầu từ sau 3 giây
        InvokeRepeating("PlayEarthquake", 6f, 9f);
    }

    private void Update() {
        if (Troll2HealthBar.instance.slider.value <= 0) {
            animator.SetBool("isDie", true);
            Destroy(gameObject, 2f);
            moveSpeed = 0f;
        }
        //if (Troll2HealthBar.instance.slider.value <= 500 && !hasPlayedEarthquake) {
        //    StartCoroutine(PlayEarthquake());
        //    hasPlayedEarthquake = true;
        //}
        //if (PlayerHealthBar.instance.slider.value <= 1700) {
        //    StopAllCoroutines();
        //    animator.SetBool("isShake", false);
        //    StartCoroutine(MoveRoutine());
        //    moveSpeed = 3f;
        //}

    }

    private void FixedUpdate() {
        //if (PlayerHealthBar.instance.slider.value <= 1700) {
        //    StopAllCoroutines();
        //    animator.SetBool("isShake", false);
        //    StartCoroutine(MoveRoutine());
        //    moveSpeed = 3f;
        //}
    }

    public IEnumerator MoveRoutine() {
        while (true) {
            animator.SetBool("isShake", false);
            if (movingRight) {
                while (transform.position.x < startPos.x + 3f) {
                    //if (IsPlayerNearby()) {
                    //    //animator.SetBool("isAttack", true);
                    //    animator.SetBool("isWalk", false);
                    //    animator.SetBool("isRunning", false);
                    //    //animator.SetBool("isHurt", false);
                    //    animator.SetBool("isDie", false);
                    //    animator.SetBool("isAttack", false);
                    //    TurnTowardsPlayer();
                    //    yield return new WaitForSeconds(waitTime);
                    //    movingRight = false;
                    //    break;
                    //}
                    //animator.SetBool("isAttack", false);
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isRunning", false);
                    //animator.SetBool("isHurt", false);
                    animator.SetBool("isDie", false);
                    animator.SetBool("isAttack", false);
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
                    //if (IsPlayerNearby()) {
                    //    //animator.SetBool("isAttack", true);
                    //    animator.SetBool("isWalk", false);
                    //    animator.SetBool("isRunning", false);
                    //    //animator.SetBool("isHurt", false);
                    //    animator.SetBool("isDie", false);
                    //    animator.SetBool("isAttack", false);
                    //    TurnTowardsPlayer();
                    //    yield return new WaitForSeconds(waitTime);
                    //    movingRight = true;
                    //    break;
                    //}
                    //animator.SetBool("isAttack", false);
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isRunning", false);
                    //animator.SetBool("isHurt", false);
                    animator.SetBool("isDie", false);
                    animator.SetBool("isAttack", false);
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
    public IEnumerator RunRoutine() {
        while (true) {
            if (movingRight) {
                animator.SetBool("isShake", false);

                while (transform.position.x < playerTransform.position.x - 2f) {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isWalk", false);
                    transform.position += Vector3.right * 6 * Time.deltaTime;
                    transform.localScale = new Vector2(1, 1);
                    yield return null;
                }
                animator.SetBool("isRunning", false);
                animator.SetBool("isHurt", false);
                animator.SetBool("isWalk", false);
                animator.SetBool("isDie", false);
                animator.SetBool("isAttack", true);
                yield return new WaitForSeconds(waitTime);
                movingRight = false;
            } else {
                animator.SetBool("isShake", false);

                while (transform.position.x > playerTransform.position.x + 2f) {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isWalk", false);
                    transform.position += Vector3.left * 6 * Time.deltaTime;
                    transform.localScale = new Vector2(-1, 1);
                    yield return null;
                }
                animator.SetBool("isRunning", false);
                animator.SetBool("isHurt", false);
                animator.SetBool("isWalk", false);
                animator.SetBool("isDie", false);
                animator.SetBool("isAttack", true);
                yield return new WaitForSeconds(waitTime);
                movingRight = true;
            }
            if (Vector3.Distance(transform.position, playerTransform.position) > 7) {
                StopAllCoroutines();
                StartCoroutine(MoveRoutine());
                moveSpeed = 3;
            }
        }
    }

    public IEnumerator PlayEarthquake() {
        StopAllCoroutines();
        animator.SetBool("isShake", true);
        animator.SetBool("isAttack", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isWalk", false);
        yield return new WaitForSeconds(2f);
    }

    public void MakeEarthquake() {
        FindObjectOfType<CameraShake>().Shake();
        //if (PlayerController.instance.touchingDirections.IsGrounded)
        //    PlayerHealthBar.instance.slider.value -= 100;
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
            Troll2HealthBar.instance.slider.value -= 200f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            //StartCoroutine(ExitStatus());
            StopAllCoroutines();
            StartCoroutine(RunRoutine());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            Troll2HealthBar.instance.slider.value -= 600f;
            //animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StopAllCoroutines();
            StartCoroutine(MoveRoutine());
            moveSpeed = 3;
            //StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Arrow")) {
            StopAllCoroutines();
            StartCoroutine(PlayEarthquake());
            //hitCount++;
            //if (hitCount >= 3) {
            //    StopAllCoroutines();
            //    StartCoroutine(RunRoutine());
            //    hitCount = 0;
            //}
        }
    }
}
