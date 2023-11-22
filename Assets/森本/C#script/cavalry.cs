using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavalry : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 0.5f;      //���x
    public int hpMax = 60;          //�R����HP
    public float reactionDistance = 4.0f;//��������
    private int hp;

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����

    private bool inDamage = false;  //�_���[�W����

    bool isActive = false;
    private bool stop = true;//false�Ȃ�Î~

    public GameObject explode1;  //�G�t�F�N�g�p
    public GameObject explode2; 

    public Transform Point1;     //�G�t�F�N�g���o��������p
    public Transform Point2;

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
            if (isActive && hp > 0 && stop)
            {
                rb.isKinematic = false;//�d�͕���
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
                //  Debug.Log("�R�����[�u");

                //���]
                if (transform.position.x < player.transform.position.x)
                {
                    transform.localScale = new Vector3(-4.5f, 4.5f, 1);
                    explode1.transform.localScale = new Vector3(-2.5f, 1.5f, 1);
                    explode2.transform.localScale = new Vector3(-2.5f, 1.5f, 1);
                }
                else if (transform.position.x == player.transform.position.x)
                {
                    transform.localScale = transform.localScale;
                    explode1.transform.localScale = new Vector3(2.5f, 1.5f, 1);
                    explode2.transform.localScale = new Vector3(2.5f, 1.5f, 1);

                }
                else if (transform.position.x > player.transform.position.x)
                {
                    transform.localScale = new Vector3(4.5f, 4.5f, 1);
                    explode1.transform.localScale = new Vector3(2.5f, 1.5f, 1);
                    explode2.transform.localScale = new Vector3(2.5f, 1.5f, 1);
                }
            }
            else if(!stop)
            {
                rb.isKinematic = true;//�d�͐Î~
                rb.velocity = Vector2.zero;//������Î~
                Debug.Log("stop");
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
        if (other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            hp -= buresball;
            Debug.Log(hp);
            Destroy(other.gameObject);
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
            stop = false;
            StartCoroutine(Cavalry());
        }

    }
    private IEnumerator Cavalry()
    {
        yield return new WaitForSeconds(1.0f);//1.0�Î~

        //�Ԃ������ʒu��explode�Ƃ���prefab��z�u����@�a���G�t�F�N�g
        Instantiate(explode1, Point1.transform.position, Quaternion.identity);

        Instantiate(explode2, Point2.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f);//0.1�Î~
        stop = true;

        yield break;//�R���[�`���I��
    }
}


