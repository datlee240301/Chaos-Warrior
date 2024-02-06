using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour {
    private Collider2D playerCollider;
    public float walkSpeed;
    public float runSpeed;
    public float airWalkSpeed;
    public float jumpImpulse;
    private float pushForce = 4f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public bool isFlip = true;
    public float CurrentMoveSpeed {
        get {
            if (CanMove) {
                if (IsMoving && !touchingDirections.IsOnWall) {
                    if (touchingDirections.IsGrounded) {
                        if (IsRunning) {
                            return runSpeed;
                        } else {
                            return walkSpeed;
                        }
                    } else return airWalkSpeed;
                } else return 0;
            } else return 0;
        }

    }
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving {
        get {
            return _isMoving;
        }
        private set {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning {
        get { return _isRunning; }
        set {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    public bool _isFacingRight = true;
    public bool IsFacingRight {
        get { return _isFacingRight; }
        private set {
            if (_isFacingRight != value) {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    public bool CanMove {
        get {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public bool IsAlive {
        get {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        if (Input.GetKey(KeyCode.C)) {
            if (!touchingDirections.IsGrounded && touchingDirections.IsOnWall) {
                animator.SetBool(AnimationStrings.isClimb, true);
                StartCoroutine(ExitClimb());
            }
        } else if (Input.GetKey(KeyCode.V)) {
            if (touchingDirections.IsGrounded && PlayerEnergyBar.instance.slider.value >= 200) {
                PlayerEnergyBar.instance.slider.value -= 200;
                animator.SetBool(AnimationStrings.isKick, true);
                runSpeed = 0;
                walkSpeed = 0;
                jumpImpulse = 0;
                isFlip = false;
            }
        }
        //else if (Input.GetMouseButtonDown(0)) {
        //    animator.SetBool(AnimationStrings.isAttack, true);
        //    StartCoroutine (ExitAttack());
        //}
    }

    private IEnumerator ExitClimb() {
        yield return new WaitForSeconds(.3f);
        animator.SetBool(AnimationStrings.isClimb, false);
    }

    private void ExitKick() {
        animator.SetBool(AnimationStrings.isKick, false);
        runSpeed = 7;
        walkSpeed = 3;
        jumpImpulse = 6;
        if (transform.localScale.x > 0) {
            transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y);
            isFlip = true;
        } else {
            transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y);
            isFlip = true;
        }
    }

    private void ExitAttack() {
        animator.SetBool(AnimationStrings.isAttack, false);
        runSpeed = 7;
        walkSpeed = 3;
        jumpImpulse = 6;
    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive) {
            if (moveInput.x != 0) {
                IsMoving = true;
            } else IsMoving = false;
            if (isFlip)
                SetFacingDirecTion(moveInput);
        } else IsMoving = false;
    }

    private void SetFacingDirecTion(Vector2 moveInput) {
        if (moveInput.x > 0 && !IsFacingRight) {
            IsFacingRight = true;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        } else if (moveInput.x < 0 && IsFacingRight) {
            IsFacingRight = false;
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started) {
            IsRunning = true;
        } else if (context.canceled) {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started && touchingDirections.IsGrounded && CanMove) {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.started && touchingDirections.IsGrounded) {
            animator.SetBool(AnimationStrings.isFire, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
        } else if (context.started && !touchingDirections.IsGrounded) {
            animator.SetBool(AnimationStrings.isAirFire, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
        } else if (context.canceled) {
            animator.SetBool(AnimationStrings.isFire, false);
            animator.SetBool(AnimationStrings.isAirFire, false);
            runSpeed = 7;
            walkSpeed = 3;
            jumpImpulse = 6;
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.started) {
            animator.SetBool(AnimationStrings.isAttack, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
        }
        //else if (context.canceled) {
        //    ExitAttack();
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("KnightSword")) {
            animator.SetBool(AnimationStrings.isHit, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
            isFlip = false;
            Vector2 pushDirection = collision.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            pushDirection.y = .75f;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
        StartCoroutine(ExitStatus());
    }

    private IEnumerator ExitStatus() {
        yield return new WaitForSeconds(1f);
        animator.SetBool(AnimationStrings.isHit, false);
        runSpeed = 7;
        walkSpeed = 3;
        jumpImpulse = 6;
        isFlip = true;
    }
    private void FinishClimb() {
        if (transform.localScale.x > 0)
            transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y + 0.8f);
        else if (transform.localScale.x < 0) transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y + 1f);
    }

    public void EnableCollider() {
        
    }

    public IEnumerator DisableCollider() {
        playerCollider.enabled = false;
        rb.gravityScale = 0;
        yield return new WaitForSeconds (1.65f);
        playerCollider.enabled = true;
        rb.gravityScale = 1;
    }
}
