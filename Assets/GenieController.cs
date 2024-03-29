﻿using System.Collections;
using UnityEngine;

public class GenieController : MonoBehaviour {
    public static GenieController instance;
    private Vector3 startPos;
    private bool movingRight = true;
    private Transform playerTransform;
    public Animator animator;
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public float stoppingDistance = 5f;

    private void Awake() {
        instance = this;
    }

    void Start() {
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(MoveRoutine());
    }

    private void Update() {
        if(GenieHealthBar.instance.slider.value <= 0) {
            animator.SetBool("isDie", true);
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator MoveRoutine() {
        while (true) {
            if (movingRight) {
                while (transform.position.x < startPos.x + 3f) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack", true);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = false;
                        break;
                    }
                    animator.SetBool("isAttack", false);
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                    transform.localScale = new Vector2(1, 1);
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
                movingRight = false;
            } else {
                while (transform.position.x > startPos.x - 3f) {
                    if (IsPlayerNearby()) {
                        animator.SetBool("isAttack", true);
                        TurnTowardsPlayer();
                        yield return new WaitForSeconds(waitTime);
                        movingRight = true;
                        break;
                    }
                    animator.SetBool("isAttack", false);
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    transform.localScale = new Vector2(-1, 1);
                    yield return null;
                }
                yield return new WaitForSeconds(waitTime);
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
            GenieHealthBar.instance.slider.value -= 200f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
        } else if (collision.gameObject.CompareTag("Skill1Effect")) {
            GenieHealthBar.instance.slider.value -= 600f;
            animator.SetBool("isHurt", true);
            moveSpeed = 0;
            StartCoroutine(ExitStatus());
            //EnenmyHealthBar.instance.slider.value -= EnenmyHealthBar.instance.slider.maxValue;
        }
    }
}
