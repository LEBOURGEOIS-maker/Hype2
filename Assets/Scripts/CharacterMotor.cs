using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
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


        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayLength = 1f;

        if (Physics.Raycast(origin, Vector3.down, rayLength))
        {
            Debug.Log("Au sol !");
        }
        else
        {
            Debug.Log("Pas au sol !");
        }

    }

    void HandleMovement()
    {
        float vertical = Input.GetAxis("Vertical");     // Z (avant/arrière)
        float horizontal = Input.GetAxis("Horizontal"); // Q/D (gauche/droite)

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isMoving = Mathf.Abs(vertical) > 0.01f || Mathf.Abs(horizontal) > 0.01f;

        float moveSpeed = isRunning ? runSpeed : walkSpeed;

        // Déplacement avant/arrière
        Vector3 move = transform.forward * vertical * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Rotation gauche/droite
        float rotation = horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

/*
        // Animation
        if (isMoving)
        {
            PlayAnimation(isRunning ? "run" : "walk");
        }
        else if (!isAttacking)
        {
            PlayAnimation("idle");
        }*/

        //JUUUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Barre espace pressée");
            if (IsGrounded())
            {
                Debug.Log("Le personnage est au sol, saut possible");
                Vector3 velocity = rb.linearVelocity;
                velocity.y = jumpSpeed.y;
                rb.linearVelocity = velocity;
            }
            else
            {
                Debug.Log("Personnage pas au sol, saut impossible");
            }
        }


        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            //Attack();
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
/*
    void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;
        currentCooldown = attackCooldown;
        PlayAnimation("attack");

        if (rayHit != null)
        {
            if (Physics.Raycast(rayHit.transform.position, transform.forward, out RaycastHit hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red, 1f);
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    }
*/
    bool IsGrounded()
    {
        float checkRadius = 0.3f;
        Vector3 spherePosition = transform.position + Vector3.down * 0.5f; // sous le personnage

        Debug.DrawRay(spherePosition, Vector3.up * 0.1f, Color.green);

        int layerMask = ~0;
        return Physics.CheckSphere(spherePosition, checkRadius, layerMask);
    }



/*
    void PlayAnimation(string animationName)
    {
        if (!animations.IsPlaying(animationName))
        {
            animations.Play(animationName);
        }
    }
    */
}
