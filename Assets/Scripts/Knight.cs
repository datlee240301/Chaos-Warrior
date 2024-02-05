using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Knight : MonoBehaviour {
    public static Knight Instance;
    private int hitCount;
    private int hitShotCount;
    private float pushForce = 4f;
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    public Animator animator;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value) {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right) {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left) {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool Hastarget {
        get { return _hasTarget; }
        private set {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove {
        get {
            return animator.GetBool(AnimationStrings.canMove);
        }
        //private set {
        //    _hasTarget = value;
        //    animator.SetBool(AnimationStrings.hasTarget, value);
        //}
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        Instance = this;
    }

    private void Update() {
        Hastarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate() {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall) {
            FlipDirection();
        }
        if (CanMove) {
            Vector3 newPosition = transform.position + new Vector3(walkSpeed * walkDirectionVector.x * Time.fixedDeltaTime, 0, 0);
            transform.position = newPosition;
        }
        //else rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void FlipDirection() {
        if (WalkDirection == WalkableDirection.Right) {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left) {
            WalkDirection = WalkableDirection.Right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerHitbox")) {
            animator.SetBool(AnimationStrings.isKnightHit, true);
            walkSpeed = 0;
            StartCoroutine(ExitStatus());
            hitCount++;
            Vector2 pushDirection = collision.transform.localScale.x >0 ? Vector2.right : Vector2.left;
            rb.AddForce(pushDirection *pushForce, ForceMode2D.Impulse);
            if (hitCount > 4) {
                Destroy(gameObject,1);
                animator.SetBool(AnimationStrings.isDeath, true);
            }
        } else if (collision.gameObject.CompareTag("Arrow")) {
            hitShotCount++;
            walkSpeed = 0;
            if (hitShotCount > 4) {
                animator.SetBool(AnimationStrings.isDeath, true);
                animator.SetBool(AnimationStrings.canMove, false);
                Destroy(gameObject, 1f);
            }
        }
    }

    public IEnumerator ExitStatus() {
        yield return new WaitForSeconds(1);
        animator.SetBool(AnimationStrings.isKnightHit, false);
        walkSpeed = 3;
    }
}
