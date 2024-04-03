using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public MMFeedbacks jumpFeedBack;
    private Collider2D playerCollider;
    public GameObject medusaSkill;
    public float walkSpeed;
    public float runSpeed;
    public float airWalkSpeed;
    public float jumpImpulse;
    private float pushForce = 4f;
    public bool isMovingRight;
    public bool isMovingLeft;
    Vector2 moveInput;
    public TouchingDirections touchingDirections;
    public bool isFlip = true;
    private float timeSinceLastVPressed = 0f;
    private float cooldownDuration = 5f; // Thời gian chờ giữa các lần ấn V
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
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        timeSinceLastVPressed += Time.deltaTime;
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        if (Input.GetKey(KeyCode.C)) {
            if (!touchingDirections.IsGrounded && touchingDirections.IsOnWall) {
                animator.SetBool(AnimationStrings.isClimb, true);
                StartCoroutine(ExitClimb());
            }
        } else if (Input.GetKey(KeyCode.Alpha1) && timeSinceLastVPressed >= cooldownDuration) {
            if (touchingDirections.IsGrounded && PlayerEnergyBar.instance.slider.value >= 200) {
                PlayerEnergyBar.instance.slider.value -= 200;
                animator.SetBool(AnimationStrings.isKick, true);
                runSpeed = 0;
                walkSpeed = 0;
                jumpImpulse = 0;
                isFlip = false;
                // Đặt lại thời gian đếm ngược
                timeSinceLastVPressed = 0f;
                Skill1.Instance.slider.value = Skill1.Instance.slider.maxValue;
            }
        } else if (PlayerHealthBar.instance.slider.value <= 0) animator.SetBool(AnimationStrings.isAlive, false);
        if (isMovingRight) {
            transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
        }
        if (isMovingLeft) {
            transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
        }
        //else if (Input.GetMouseButtonDown(0)) {
        //    animator.SetBool(AnimationStrings.isAttack, true);
        //    StartCoroutine (ExitAttack());
        //}
    }

    public void UseSkill1() {
        if (touchingDirections.IsGrounded && PlayerEnergyBar.instance.slider.value >= 200
            && timeSinceLastVPressed >= cooldownDuration) {
            PlayerEnergyBar.instance.slider.value -= 200;
            animator.SetBool(AnimationStrings.isKick, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
            isFlip = false;
            // Đặt lại thời gian đếm ngược
            timeSinceLastVPressed = 0f;
            Skill1.Instance.slider.value = Skill1.Instance.slider.maxValue;
        }
    }
    /// <summary>
    /// Mobile Phone
    /// </summary>
    public void ShootArrow() {
        animator.SetBool(AnimationStrings.isFire, true);
        runSpeed = 0;
        walkSpeed = 0;
        jumpImpulse = 0;
    }

    public void ExitShoot() {
        animator.SetBool(AnimationStrings.isFire, false);
        animator.SetBool(AnimationStrings.isAirFire, false);
        runSpeed = 7;
        walkSpeed = 3;
        jumpImpulse = 6;
    }

    public void MoveRight() {
        animator.SetBool("isMoving", true);
        transform.localScale = new Vector3(1, 1, 1);
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void ExitMoveRight() {
        animator.SetBool("isMoving", false);
        isMovingLeft = false;
        isMovingRight = false;
    }

    public void MoveLeft() {
        animator.SetBool("isMoving", true);
        transform.localScale = new Vector2(-1, 1);
        isMovingLeft = true;
        isMovingRight = false;
    }
    public void JumpBtn() {
        if (touchingDirections.IsGrounded && CanMove) {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void UseSword() {
        animator.SetBool("isAttack", true);
    }

    public void Climb() {
        StartCoroutine(WaitForClimbing());
    }

    public IEnumerator WaitForClimbing() {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall && CanMove) {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            yield return new WaitForSeconds(0.35f);
            animator.SetBool(AnimationStrings.isClimb, true);
            StartCoroutine(ExitClimb());

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
            jumpFeedBack?.PlayFeedbacks();
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
        //if (context.started) {
        //    animator.setbool(animationstrings.isattack, true);
        //    runspeed = 0;
        //    walkspeed = 0;
        //    jumpimpulse = 0;
        //}
        //else if (context.canceled) {
        //    exitattack();
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
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            PlayerHealthBar.instance.slider.value -= 200;
        } else if (collision.gameObject.CompareTag("Magic")) {
            animator.SetBool(AnimationStrings.isHit, true);
            runSpeed = 0;
            walkSpeed = 0;
            jumpImpulse = 0;
            isFlip = false;
            Vector2 pushDirection = collision.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            PlayerHealthBar.instance.slider.value -= 200;
        } else if (collision.gameObject.CompareTag("MedusaSkill")) {
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y - .3f);
            Instantiate(medusaSkill, spawnPos, transform.rotation);
            //gameObject.SetActive(false);    
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
        //playerCollider.enabled = false;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1.65f);
        //playerCollider.enabled = true;
        rb.gravityScale = 1;
    }
}
