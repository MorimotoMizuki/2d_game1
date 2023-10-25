using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuraimu : MonoBehaviour
{
    public float speed = 0.5f;
    public int hpMax = 10;
    Rigidbody2D rb;
    public float reactionDistance = 4.0f;
    private int rush_damage = 10;
    private bool inDamage = false;

    bool isActive = false;

    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        hp = hpMax;
        Debug.Log(rush_damage);
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
        if (hp <= 0)
        {
            //0.5�b��ɏ���
            Destroy(gameObject, 0.5f);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rushWall")
        {
            //�_���[�W
            hp -= rush_damage;
            if (hp <= 0)
            {             //�Q�[���I�[�o�[
                Debug.Log("Enemy HP" + hp);
                //�����������
                GetComponent<CircleCollider2D>().enabled = false;
                //�ړ���~
                rb.velocity = new Vector2(0, 0);
            }
        }




    }
}
