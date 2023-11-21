using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//bird���擾
    public GameObject cane;//cane���擾
    public GameObject player;//player���擾

    private int Headman_HP;//������HP
    public int HP = 100;    //�ő�HP

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����


    private bool inDamage = false;  //�_���[�W����
    private float reactionDistance = 10.0f;//��������

    bool isActive = false;//�����o���t���O
    private bool OnAttack = false;

    //bird�U��time�֌W
    private float create_bird_count = 0.0f;
    private float create_bird_time = 5.0f;

    //cane�U��time�֌W
    private float create_cane_count = 0.0f;
    private float create_cane_time = 3.0f;


    //�A�j���V����
    Animator animator; //�A�j���[�^�[
    public string attackanime = "EnemyHeadmanAttack";//���U���̃��[�V����
    public string Caneanime = "EnemyHeadmanCane";    //��U���̃��[�V����
    public string Stopanime = "EnemyHeadmanStop";    //�Î~���[�V����

    GameObject[] tagObjects;//�I�u�W�F�N�g�J�E���g�p

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Headman_HP = HP;//�ő�HP��ݒ�
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Headman_HP > 0)
            {

                create_bird_count += Time.deltaTime;//�J�E���g
                create_cane_count += Time.deltaTime;//�J�E���g
                //Debug.Log(create_bird_count);
                if (create_bird_count > create_bird_time)
                {   
                    StartCoroutine(Bird_attack());//�R���[�`���J�n
                }
                else if(create_cane_count > create_cane_time)
                {
                    StartCoroutine(Cane_attack());//�R���[�`���J�n
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
            isActive = false;//��A�N�e�B�u
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
    private void FixedUpdate()
    {
        if (OnAttack)
        {
            animator.Play(Stopanime);
            //Debug.Log("������");
            float vecX = Random.Range(-5.0f, 7.0f);//�����_�����i�j�͈͂Őݒ�
            var t = Instantiate(bird) as GameObject;//�I�u�W�F�N�g���쐬

            //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            t.transform.position = new Vector3(vecX, 5.0f, 0);

            t.AddComponent<HeadmanBird>();//bird�̓��������߂�
            tagObjects = GameObject.FindGameObjectsWithTag("bird");//���𐔂���
            if (tagObjects.Length >= 1)
            {
                OnAttack = false;
                create_bird_count = 0.0f;//���Z�b�g
                create_cane_count = 2.0f;//���Z�b�g
            }
        }
    }
    //�R���[�`��
    private IEnumerator Bird_attack()
    {
        //�A�j���V����
        animator.Play(attackanime);
        yield return new WaitForSeconds(0.2f);//0.2�Î~

        OnAttack = true;//�U���ibird�j�̃t���O���グ��

        yield return new WaitForSeconds(0.5f);//0.5�Î~

        //Debug.Log("�I��");
        yield break;//�R���[�`���I��
        
    }
    private IEnumerator Cane_attack()
    {
        //�A�j���[�V����
        animator.Play(Caneanime);
        yield return new WaitForEndOfFrame();//�P�t���[���Î~
        var pos = this.gameObject.transform.position + transform.up *2.0f - transform.right*2.0f; //�ʒu�ݒ�
        Instantiate(cane, pos, Quaternion.identity);//�쐬

        create_cane_count =0.0f;//���Z�b�g

        yield return new WaitForSeconds(0.5f);//�O�D�T�Î~
        animator.Play(Stopanime);

        yield break;//�R���[�`���I��

    }
    //�ڐG�Ǘ�
        private void OnTriggerEnter2D(Collider2D collider)
        {
        Debug.Log("OntriggerEnter2D:" + collider.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (collider.gameObject.tag == "rushWall")
        {
            //�_���[�W
            Headman_HP -= rushdamage;
            Debug.Log(Headman_HP);
            inDamage = true;
        }
        if(collider.gameObject.tag =="Fireball")
        {
            //�_���[�W
            Headman_HP -= buresball;
            Debug.Log(Headman_HP);
            inDamage = true;
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Headman_HP <= 0)
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
