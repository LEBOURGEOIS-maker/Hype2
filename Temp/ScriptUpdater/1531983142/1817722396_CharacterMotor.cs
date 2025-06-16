using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    // Animation component (legacy)
    private Animation animations;

    // Movement settings
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float turnSpeed = 120f;

    // Attack settings
    public float attackCooldown = 1f;
    public float attackRange = 2f;
    private bool isAttacking = false;
    private float currentCooldown = 0f;

    // Jump settings
    public Vector3 jumpSpeed = new Vector3(0f, 7f, 0f);

    // Movement key bindings (use AZERTY by default)
    public string inputFront = "z";
    public string inputBack = "s";
    public string inputLeft = "q";
    public string inputRight = "d";

    // State tracking
    public bool isDead = false;

    // Cached components
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private GameObject rayHit;

    void Start()
    {
        animations = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        rayHit = GameObject.Find("RayHit");

        if (rayHit == null)
        {
            Debug.LogWarning("RayHit GameObject not found. Attack raycasting may not work.");
        }

        currentCooldown = attackCooldown;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleAttackCooldown();
    }

    void HandleMovement()
    {
        bool isMoving = false;

        // Walk forward
        if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0f, 0f, walkSpeed * Time.deltaTime);
            PlayAnimation("walk");
            isMoving = true;
        }

        // Run forward
        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0f, 0f, runSpeed * Time.deltaTime);
            PlayAnimation("run");
            isMoving = true;
        }

        // Walk backward
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0f, 0f, -(walkSpeed / 2f) * Time.deltaTime);
            PlayAnimation("walk");
            isMoving = true;
        }

        // Rotate left
        if (Input.GetKey(inputLeft))
        {
            transform.Rotate(0f, -turnSpeed * Time.deltaTime, 0f);
        }

        // Rotate right
        if (Input.GetKey(inputRight))
        {
            transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
        }

        // Idle if not moving
        if (!isMoving && !isAttacking)
        {
            PlayAnimation("idle");
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = jumpSpeed.y;
            rb.linearVelocity = velocity;
        }

        // Attack input (can attack while idle or moving)
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void HandleAttackCooldown()
    {
        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                isAttacking = false;
                currentCooldown = attackCooldown;
            }
        }
    }

    void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;
        currentCooldown = attackCooldown;
        PlayAnimation("attack");

        if (rayHit != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayHit.transform.position, transform.forward, out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red, 1f);
                // Optionally: Deal damage to hit target
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }

    bool IsGrounded()
    {
        int groundLayer = 1 << 9; // Only layer 9
        return Physics.CheckCapsule(
            playerCollider.bounds.center,
            new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z),
            0.08f,
            groundLayer
        );
    }

    void PlayAnimation(string animationName)
    {
        if (!animations.IsPlaying(animationName))
        {
            animations.Play(animationName);
        }
    }
}

