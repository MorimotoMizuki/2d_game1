using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTest : MonoBehaviour
{
    //�v���C���[�I�u�W�F�N�g
    public GameObject player;

    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;

    Rigidbody2D rb = null;

    //5�b���Ƃɒe�𔭎˂��邽�߂̂���
    private float targetTime = 5.0f;
    private float currentTime = 0;

    public int hp = 30;
    public float reactionDistance = 4.0f;//��������

    private int A_Hp;

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����

    private bool inDamage = false;
    private bool isActive = false;


    public EnemyArrow bullet;
    private SpriteRenderer sr = null;

    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    bool aniflag = false;
    int aniTime = 0;

    //SE�p
    [SerializeField]
    AudioSource archerAudioSource;

    private void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        A_Hp = hp;
        sr = GetComponent<SpriteRenderer>();

        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {        
            return;
        }
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null )
        {
            if (isActive && A_Hp > 0 && sr.isVisible)
            {
                // PLAYER�̈ʒu���擾
                // Vector3 targetPos = player.transform.position;
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
                    make_naihu();

                    //SE
                    archerAudioSource.Play();

                    //�A�j���[�V����
                    animator.Play("Archer");
                    aniflag = true;
                }
            }
            else if(isActive == false)
            {
                //�v���C���[�Ƃ̋��������߂�
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //�A�N�e�B�u�ɂ���
                }
            }
            //else
            //{
            //    rb.Sleep();
            //}
        }
        else if (isActive)
        {
            isActive = false;
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

        if(aniflag)//�A�j���[�V������߂��܂ł̎��Ԃ�����
        {
            aniTime++;
        }

        if (aniTime == 150)//�A�j���[�V������߂�
        {
            animator.Play("StopArcher");
            aniTime = 0;
            aniflag = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            A_Hp -= rushdamage;
            Debug.Log(A_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            A_Hp -= buresball;
            Debug.Log(A_Hp);
            Destroy(other.gameObject);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (A_Hp <= 0)
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

    void make_naihu()
    {
        EnemyArrow.Naihu = true;

    }
}
