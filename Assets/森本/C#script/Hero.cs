using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject thunder;
    public GameObject Player;
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;

    //�v���C���[�I�u�W�F�N�g
    public GameObject player;
    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;




    Rigidbody2D rb;

    //5�b���Ƃɒe�𔭎˂��邽�߂̂���
    private float targetTime = 5.0f;
    private float currentTime = 0;

    public int hp = 300;
    public float reactionDistance = 20.0f;//��������

    private int Hero_Hp;

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����

    private bool inDamage = false;
    private bool isActive = false;

    private int oldHP;
    private int rnd;
    
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    public string stopAnime = "StopMove";
    public string attack    = "attack";
    public string attackT   = "attackT";

    private void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        Hero_Hp = hp;
        oldHP = hp;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Hero_Hp > 0)
            {    
                currentTime += Time.deltaTime;

                if (targetTime < currentTime)
                {
                    StartCoroutine(Slashing()); 
                }

                if (Hero_Hp != oldHP)
                {
                    rnd = Random.Range(1, 4);

                    if (rnd == 1)
                    {
                        //�댯�G���A�\��
                        DangerArea1.gameObject.SetActive(true);
                        //�R���[�`���Ăяo��
                        StartCoroutine(Thunder1());
                    }
                    if (rnd == 2)
                    {
                        //�댯�G���A�\��
                        DangerArea2.gameObject.SetActive(true);
                        //�R���[�`���Ăяo��
                        StartCoroutine(Thunder2());
                    }
                    if (rnd == 3)
                    {   //�댯�G���A�\��
                        DangerArea3.gameObject.SetActive(true);
                        //�R���[�`���Ăяo��
                        StartCoroutine(Thunder3());
                    }
                    oldHP = Hero_Hp;
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
    IEnumerator Slashing()
    { 
        currentTime = 0;//�^�C�����Z�b�g
        yield return new WaitForSeconds(1.0f);//�ҋ@
        animator.Play(attack);//�a�����[�V����
        //�G�̍��W��ϐ�pos�ɕۑ�
        var pos = this.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(tama) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        t.AddComponent<HeroGan>();//�������Z�b�g
        animator.Play(stopAnime);//���ɖ߂�
        yield return new WaitForSeconds(0.5f);//�ҋ@
        animator.Play(attack);//�a�����[�V����

        //�e�̃v���n�u���쐬
        var t2 = Instantiate(tama) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t2.transform.position = pos;
        t2.AddComponent<HeroGan>();//�������Z�b�g

        yield return new WaitForSeconds(0.5f);//�ҋ@
        animator.Play(stopAnime);//���ɖ߂�
        yield break;//�I��
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            Hero_Hp -= rushdamage;
            Debug.Log(Hero_Hp);
            inDamage = true;
        }
        if (other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            Hero_Hp -= buresball;
            Debug.Log(Hero_Hp);
            Destroy(other.gameObject);
            inDamage = true;
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Hero_Hp <= 0)
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

    private IEnumerator Thunder1()
    {
        yield return new WaitForSeconds(3.0f);

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point1.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea1.gameObject.SetActive(false);

    }
    private IEnumerator Thunder2()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point2.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea2.gameObject.SetActive(false);
    }
    private IEnumerator Thunder3()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point3.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea3.gameObject.SetActive(false);
    }

}
