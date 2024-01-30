using UnityEngine;

public class Damageable : MonoBehaviour {
    Animator animator;
    [SerializeField]
    public int _maxHealth = 100;
    public int MaxHealth {
        get { return _maxHealth; }
        set {
            _maxHealth = value;
        }
    }
    [SerializeField]
    public int _health = 100;
    public int Health {
        get { return _health; }
        set {
            _health = value;
            if (_health < 0) IsAlive = false;
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit;
    private float invincibilityTime = 0.25f;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        private set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (isInvincible) {
            if (timeSinceHit > invincibilityTime) {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage) {
        if (IsAlive && !isInvincible) {
            Health -= damage;
            isInvincible = true;
        }
    }
}
