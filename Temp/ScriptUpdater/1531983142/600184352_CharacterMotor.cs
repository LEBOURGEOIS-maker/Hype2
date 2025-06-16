using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    // Animations du personnage
    private Animation animations;

    // Vitesse de déplacement
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float turnSpeed = 120f;

    // Variables concernant l'attaque
    public float attackCooldown = 1f;
    private bool isAttacking;
    private float currentCooldown;
    public float attackRange = 2f;
    private GameObject rayHit;

    // Inputs configurables
    public string inputFront = "z";
    public string inputBack = "s";
    public string inputLeft = "q";
    public string inputRight = "d";

    public Vector3 jumpSpeed = new Vector3(0, 7f, 0);

    private CapsuleCollider playerCollider;
    private Rigidbody rb;

    // Le personnage est-il mort ?
    public bool isDead = false;

    void Start()
    {
        animations = GetComponent<Animation>();
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rayHit = GameObject.Find("RayHit");

        if (rayHit == null)
        {
            Debug.LogWarning("RayHit GameObject not found. Attack raycasting may not work.");
        }

        currentCooldown = attackCooldown;
    }

    bool IsGrounded()
    {
        int groundLayer = 1 << 9; // Use layer 9
        return Physics.CheckCapsule(
            playerCollider.bounds.center,
            new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z),
            0.08f,
            groundLayer
        );
    }

    void Update()
    {
        if (!isDead)
        {
            // Avancer (marche)
            if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);

                if (!isAttacking)
                    animations.Play("walk");

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Attack();
            }

            // Courir
            if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");
            }

            // Reculer
            if (Input.GetKey(inputBack.ToLower()))
            {
                transform.Translate(0, 0, -(walkSpeed / 2f) * Time.deltaTime);

                if (!isAttacking)
                    animations.Play("walk");

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Attack();
            }

            // Tourner à gauche
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }

            // Tourner à droite
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }

            // Rester immobile
            if (!Input.GetKey(inputFront.ToLower()) && !Input.GetKey(inputBack.ToLower()))
            {
                if (!isAttacking)
                    animations.Play("idle");

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    Attack();
            }

            // Sauter
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                Vector3 velocity = rb.linearVelocity;
                velocity.y = jumpSpeed.y;
                rb.linearVelocity = velocity;
            }
        }

        // Cooldown attaque
        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
        }
    }

    // Fonction d'attaque
    public void Attack()
    {
        if (!isAttacking)
        {
            animations.Play("attack");

            if (rayHit != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(rayHit.transform.position, transform.forward, out hit, attackRange))
                {
                    Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);
                    // Ici, vous pouvez ajouter des effets de dégâts à l'objet touché
                }
            }

            isAttacking = true;
        }
    }
}

