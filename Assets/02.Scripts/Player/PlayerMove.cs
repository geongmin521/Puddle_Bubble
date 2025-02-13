using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float idleSpeed = 0f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shootSpeed;
    private float currentSpeed;

    private float horizontal;
    private float vertical;

    private bool isShooting;
    private bool isMoving;
    private bool isBack;
    private bool isRight;

    //component
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator ani;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        ani = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Player.Instance.isDie == true)
        {
            moveSpeed = 0;
            return;
        }
          
        GetInput();
        Move();
    }

    // Input
    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    // Move
    private void Move()
    {
        // bool set
        isShooting = Input.GetMouseButton(0);
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isBack = vertical > 0;
        isRight = horizontal > 0;

        // speed, animation set
        if (isShooting)
        {
            Player.Instance.currentPlayerState = Player.PlayerState.Shoot;
            currentSpeed = shootSpeed;
            ani.SetBool("Attack", true);
        }
        else if (isMoving)
        {
            Player.Instance.currentPlayerState = Player.PlayerState.Move;
            currentSpeed = moveSpeed;
            ani.SetBool("Attack", false);
            ani.SetBool("Walk", true);
            ani.SetBool("Idle", false);
            if (isBack) { ani.SetBool("isBack", true); }
            else { 
                ani.SetBool("isBack", false); 
                if(isRight) { sr.flipX = false; }
                else { sr.flipX = true; }
            }
        }
        else
        {
            Player.Instance.currentPlayerState = Player.PlayerState.Idle;
            currentSpeed = idleSpeed;
            ani.SetBool("Attack", false);
            ani.SetBool("Walk", false);
            ani.SetBool("Idle", true);
            if (isBack) { ani.SetBool("isBack", true); }
            else { ani.SetBool("isBack", false); }
        }

        // move
        rb.velocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
    }
}
