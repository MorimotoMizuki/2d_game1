using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody�^�̕ϐ�
    float axisH = 0.0f;         //����
    public LayerMask GroundLayer;

    public float speed = 3.0f;  //�ړ����x
    public float jump = 5.0f;   //�W�����v��
    public float rush = 30.0f;  //�ːi�̗�

    //�t���O
    bool gojump  = false;       //�W�����v����
    bool ongrond = false;       //�n�ʔ���
    bool gorush  = false;       //�U������(�ːi)
    bool horizon = false;       //����

    int rush_time = 100;    //�U��(�ːi)�N�[���^�C��

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���������̃`�F�b�N
        axisH = Input.GetAxisRaw("Horizontal");

        //�����̒���
        if(axisH > 0.0f)
        {
            //�E�ړ�
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(5, 5);
            horizon = true;
        }
        if(axisH < 0.0f)
        {
            //���ړ�
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-5, 5);
            horizon = false;
        }
        //�L�����N�^�[�̃W�����v
        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //�L�����N�^�[�̓ːi�U��
        if (ongrond)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Rush();
            }
        }
    }

    private void FixedUpdate()
    {
        //�n�㔻��
        ongrond = Physics2D.Linecast(transform.position,
                                     transform.position - (transform.up * 0.1f),
                                     GroundLayer);
        if(ongrond || axisH != 0 || gorush==true)
        {
            //�n��or���x���O�ł͂Ȃ�or�U�����ł͂Ȃ�
            //���x���X�V
            rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
        }
        if(ongrond && gojump)
        {
            //�n�ォ�W�����v�L�[�������ꂽ�Ƃ�
            //�W�����v����
            Debug.Log("�W�����v");
            Vector2 jumpPw = new Vector2(0, jump);      //�W�����v������x�N�g��
            rb.AddForce(jumpPw, ForceMode2D.Impulse);   //�u�ԓI�ȗ͂�������
            gojump = false; //�W�����v�t���O�����낷
        }

        if(rush_time < 50)
        {
            rush_time++;//�N�[���^�C��
        }

        if(gorush && horizon == true)
        {
            //�n�ォ���N���b�N�������ꂽ�Ƃ����E����
            //�ːi����
            Debug.Log("�ːi");
            Vector2 rushPw = new Vector2(rush, 0);      //�W�����v������x�N�g��
            rb.AddForce(rushPw,ForceMode2D.Impulse);
            gorush = false; //�U���t���O�����낷
            rush_time = 0;
        }
        else if (gorush && horizon == false)
        {
            //�n�ォ���N���b�N�������ꂽ�Ƃ���������
            //�ːi����
            Debug.Log("�ːi");
            Vector2 rushPw = new Vector2(-rush, 0);      //�W�����v������x�N�g��
            rb.AddForce(rushPw, ForceMode2D.Impulse);
            gorush = false; //�U���t���O�����낷
            rush_time = 0;
        }
    }
    void Jump()
    {
        gojump = true; //�W�����v�t���O�𗧂Ă�
    }
    void Rush()
    {
        if(rush_time >= 50)
        gorush = true; //�U��(�ːi)�t���O�𗧂Ă�
        
    }
}
