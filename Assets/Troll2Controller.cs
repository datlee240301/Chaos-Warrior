using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll2Controller : MonoBehaviour {
    public static Troll2Controller instance;
    TouchingDirections directions;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance;
    private int hitCount = 0;
    private int palyerHitBoxCount = 0;
    bool hasPlayedEarthquake = false;
    bool hasRun = false;
    bool hasMove = false;

    private void Awake() {
        instance = this;
    }

    void Start() {
        directions = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
    }

    private void Update() {
        if (PlayerHealthBar.instance.slider.value <= 0) {
            StopAllCoroutines();
            StartCoroutine(MoveRoutine());
        }
        if (Troll2HealthBar.instance.slider.value <= 0) {
            animator.SetBool("isDie", true);
            Destroy(gameObject, 4f);
            moveSpeed = 0f;
        }
        if (Troll2HealthBar.instance.slider.value <= 1000 && !hasPlayedEarthquake) {
            StartCoroutine(PlayEarthquake());
            hasPlayedEarthquake = true;
        }
        //ContinuousMove();
    }

    private void FixedUpdate() {
        //if (PlayerHealthBar.instance.slider.value <= 1700) {
        //    StopAllCoroutines();
        //    animator.SetBool("isShake", false);
        //    StartCoroutine(MoveRoutine());
        //    moveSpeed = 3f;
        //}
    }

    //void ContinuousMove() {
    //    if (movingRight) {
    //        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    //        if (directions.IsOnWall) {
    //            movingRight = false;
    //        }
    //    } else {
    //        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    //        if (directions.IsOnWall) {
    //            movingRight = true;
    //        }
    //    }
    //}
    public IEnumerator MoveRoutine() {
        while (true) {
            moveSpeed = 3f;
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
                yield return new WaitForSeconds(.4f);
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
                yield return new WaitForSeconds(.4f);
                movingRight = true;
            }
            //if (Vector3.Distance(transform.position, playerTransform.position) > 7) {
            //    StopAllCoroutines();
            //    StartCoroutine(MoveRoutine());
            //    moveSpeed = 3;
            //}
        }
    }

    public IEnumerator PlayEarthquake() {
        StopAllCoroutines();
        animator.SetBool("isShake", true);
        animator.SetBool("isAttack", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isWalk", false);
        yield return new WaitForSeconds(0f);
    }

    public void MakeEarthquake() {
        FindObjectOfType<CameraShake>().Shake();
        if (PlayerController.instance.touchingDirections.IsGrounded)
            PlayerHealthBar.instance.slider.value -= 100;
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
            if (Troll2HealthBar.instance.slider.value <= 1000) animator.SetBool("isHurt", true);
            palyerHitBoxCount++;
            if (palyerHitBoxCount == 1) {
                StopAllCoroutines();
                StartCoroutine(RunRoutine());
            }
            //animator.SetBool("isHurt", true);
            //StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            Troll2HealthBar.instance.slider.value -= 600f;
            animator.SetBool("isHurt", true);
            //StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Arrow")) {
            hitCount++;
            if (Troll2HealthBar.instance.slider.value <= 1000) animator.SetBool("isHurt", true);
            if (hitCount == 1) {
                StopAllCoroutines();
                StartCoroutine(RunRoutine());
            }
        }
    }
}
