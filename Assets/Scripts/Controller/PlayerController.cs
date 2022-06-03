using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed = 5.0f; //�÷��̾� �̵��ӵ�
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
        
        Managers.input.KeyAction -= OnKeyBoard; // �̹� �۵��� �Ǽ� ����
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
        if (Input.GetKey(KeyCode.A)) // ���� �̵�
        {
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(-2, 2, 1); //���� �ٶ󺸴� ����
            transform.Translate(Vector3.left * Time.deltaTime * _speed);  //���� * �ӵ�
        }
        else if (Input.GetKey(KeyCode.D)) //������ �̵�
        {
            animator.SetInteger(animationState, (int)States.Run);
            transform.localScale = new Vector3(2, 2, 1); //������ �ٶ󺸴� ����
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(KeyCode.Space)) //����
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
