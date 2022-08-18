using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HypeMotor : MonoBehaviour {

    // Animations du perso
    Animator _animator;

    // Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // Variables concernant l'attaque
    public float attackCooldown;
    private bool isAttacking;
    private float currentCooldown;
    public float attackRange;
    private GameObject rayHit;

    // Inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    void Start () {
        //_animator = GetComponent<Animator>();
        _animator = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        rayHit = GameObject.Find("RayHit");
    }

    bool IsGrounded()
    {
       return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, layerMask:9);
    }

	void Update () {
        // ## si on avance ## //
        if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);

            //if (!isAttacking)
            //{
                _animator.Play("Walk");
            //}
        }
                
        // ## si on recule ## //
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);

            //if (!isAttacking)
            //{
                _animator.Play("Walk");
            //}

            /* if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            } */
        }
        // Si on sprint
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, (walkSpeed*1.5f) * Time.deltaTime);
            _animator.Play("Walk");
        }
        
        // rotation à gauche
        if (Input.GetKey(KeyCode.Q))
        {
            //transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            transform.Rotate(0, -100 * Time.deltaTime, 0);
        }

        // rotation à droite
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
            //transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }

        // ## Si on saute ## 
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Préparation du saut (Nécessaire en C#)
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpSpeed.y;

            // Saut
            gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
        }
    }           
}