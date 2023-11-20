using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmer : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 0.5f;      //���x
    public int hpMax = 10;          //�_����HP
    public float reactionDistance = 4.0f;//��������
    private int hp;

    private int rushdamage = 10;    //�ːi�̍U����
    private bool inDamage = false;  //�_���[�W����

    bool isActive = false;

    public GameObject explode;  //�G�t�F�N�g�p

    public Transform Point;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (isActive && hp > 0)
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
                // ENEMY��Rigidbody2D�Ɉړ����x���w�肷��
                rb.velocity = direction * speed;
                //  Debug.Log("�_�����[�u");

                //���]
                if(transform.position.x < player.transform.position.x)
                {
                    transform.localScale = new Vector3(-5, 5, 1);
                    explode.transform.localScale = new Vector3(-3, 3, 1);
                }
                else if(transform.position.x == player.transform.position.x)
                {
                    transform.localScale = transform.localScale;
                    explode.transform.localScale = new Vector3(3, 3, 1);

                }
                else if(transform.position .x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(5, 5, 1);
                    explode.transform.localScale = new Vector3(3, 3, 1);

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
        // Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            hp -= rushdamage;
            inDamage = true;

        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (hp <= 0)
        {
            Debug.Log("�G���|��Ă���");
            //�ړ���~
            rb.velocity = new Vector2(0, 0);
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

    //�����G�t�F�N�g
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player�ɓ���������
        {
            //�Ԃ������ʒu��explode�Ƃ���prefab��z�u����@�a���G�t�F�N�g
            Instantiate(explode, Point.transform.position, Quaternion.identity);
            
        }

    }
}

