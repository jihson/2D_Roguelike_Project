using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5.0f; //플레이어 이동속도
    public float _jumpPower;

    private Rigidbody2D rigid;
    private Animator animator;

    private bool isjumping;
    private string animationState = "AnimationState";
    private Vector2 movement = new Vector2();

    enum States
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
    }

    void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Init();
        
        Managers.input.KeyAction -= OnKeyBoard; // 이미 작동된 실수 방지
        Managers.input.KeyAction += OnKeyBoard;

        Managers.input.NonKeyAction -= NonKeyBoard;
        Managers.input.NonKeyAction += NonKeyBoard;
    }

    void Update()
    {
        
        //animator.SetBool("Run", false);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Backgrounds"))
        {
            isjumping = false;
            //animator.enabled = true;
        }
    }

    void OnKeyBoard()
    {
        if (Input.GetKey(KeyCode.A)) // 왼쪽 이동
        {
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(-2, 2, 1); //왼쪽 바라보는 방향
            transform.Translate(Vector3.left * Time.deltaTime * _speed);  //방향 * 속도
        }
        else if (Input.GetKey(KeyCode.D)) //오른쪽 이동
        {
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(2, 2, 1); //오른쪽 바라보는 방향
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.Space)) //점프
        {
            if (isjumping == false)
            {
                animator.SetInteger(animationState, (int)States.Jump);
                rigid.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

                isjumping = true;
            }
            else return;
        }
    }

    void NonKeyBoard()
    {
        animator.SetInteger(animationState, (int)States.Idle);
    }

    
}
