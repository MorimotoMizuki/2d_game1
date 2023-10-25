using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuraimu : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 0.5f;
    public int hpMax = 10;
    public float reactionDistance = 4.0f;
    public int hp;

    private int rushdamage = 10;
    private bool inDamage = false;

    bool isActive = false;
   
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        hp = hpMax;
        Debug.Log(rushdamage);
        
    }

    // Update is called once per frame
    void Update()
    {
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
                //  Debug.Log("�X���C�����[�u");


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

        if(inDamage)
        {
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
        else
        {
            inDamage = false;
        }
       
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

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
        if (hp <= 0)
        {    
            Debug.Log("�G���|��Ă���");
            //�ړ���~
            rb.velocity = new Vector2(0, 0);
            Destroy(gameObject,0.2f);//0.2�����ēG������
        }
    }
       
}

  