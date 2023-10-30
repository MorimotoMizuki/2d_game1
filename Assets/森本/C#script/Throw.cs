using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    public GameObject player;

    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;

    Rigidbody2D rb;

    //1�b���Ƃɒe�𔭎˂��邽�߂̂���
    private float targetTime = 3.0f;
    private float currentTime = 0;

    public int hp = 30;
    public float reactionDistance = 4.0f;//��������

    private int T_Hp;

    private int rushdamage = 10;
    private bool inDamage = false;
    private bool isActive = false;

    private void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        T_Hp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && T_Hp > 0)
            {
                // PLAYER�̈ʒu���擾
                Vector2 targetPos = player.transform.position;
                // PLAYER��x���W
                float x = targetPos.x;
                // ENEMY�́A�n�ʂ��ړ�������̂ō��W�͏��0�Ƃ���
                float y = 0;
                // �ړ����v�Z�����邽�߂̂Q�����̃x�N�g�������
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                //1�b�o���Ƃɒe�𔭎�
                currentTime += Time.deltaTime;

                if (targetTime < currentTime)
                {
                    currentTime = 0;
                    //�G�̍��W��ϐ�pos�ɕۑ�
                    var pos = this.gameObject.transform.position;

                    //�e�̃v���n�u���쐬
                    var t = Instantiate(tama) as GameObject;

                    //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                    t.transform.position = pos;

                    //�G����v���C���[�Ɍ������x�N�g�������
                    //�v���C���[�̈ʒu����G�̈ʒu(�e�̈ʒu)������
                    Vector2 vec = player.transform.position - pos;

                    //�e��RigidBody2D�R���|�l���g��velocity�ɂ��������߂��x�N�g�������ė͂�������
                    t.GetComponent<Rigidbody2D>().velocity = vec;
                }
            }
            else
            {
                //�v���C���[�Ƃ̋��������߂�
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //�A�N�e�B�u�ɂ���
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
            rb.velocity = Vector2.zero;
        }



        if (inDamage)
        {
            //�_���[�W���_�ł�����
            float val = Mathf.Sin(Time.time * 50);
            // Debug.Log(val);
            if (val > 0)
            {
                //�X�v���C�g��\��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�X�v���C�g���\��
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;//�_���[�W���͑���ɂ��ړ��͂����Ȃ�
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            T_Hp -= rushdamage;
            Debug.Log(T_Hp);
            inDamage = true;
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (T_Hp <= 0)
        {
            Debug.Log("�G���|��Ă���");
            Destroy(gameObject, 0.2f);//0.2�����ēG������
        }
    }
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
