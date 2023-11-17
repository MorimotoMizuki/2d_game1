using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//bird���擾
    public GameObject player;

    private int Headman_HP;
    public int HP = 100;

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����

    private bool inDamage = false;  //�_���[�W����

    private float reactionDistance = 10.0f;//��������

    bool isActive = false;//�����o���t���O
    private bool OnAttack = false;

    private float create_bird_count = 0.0f;
    private float create_time = 5.0f;

    //�A�j���V����
    Animator animator; //�A�j���[�^�[
    public string attackanime = "EnemyHeadmanAttack";
    public string Caneanime = "EnemyHeadmanCane";
    public string Stopanime = "EnemyHeadmanStop";

    GameObject[] tagObjects;

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
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Headman_HP > 0)
            {
               
                create_bird_count += Time.deltaTime;
                Debug.Log(create_bird_count);
                if (create_bird_count > create_time)
                {
                   // animator.Play(attackanime);
                    StartCoroutine(bird_attack());
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
    private void FixedUpdate()
    {
        if (OnAttack)
        {
            animator.Play(Stopanime);
            Debug.Log("������");
            float vecX = Random.Range(-5.0f, 7.0f);
           // Check("bird");
            var t = Instantiate(bird) as GameObject;

            //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            t.transform.position = new Vector3(vecX, 5.0f, 0);

            t.AddComponent<HeadmanBird>();
            tagObjects = GameObject.FindGameObjectsWithTag("bird");
            if (tagObjects.Length >= 1)
            {
                OnAttack = false;
                create_bird_count = 0.0f;
            }

        }
        else
        {
            
        }
    }
    //�R���[�`��
    private IEnumerator bird_attack()
    {
        //�A�j���V����
        animator.Play(attackanime);
        yield return new WaitForSeconds(0.2f);

        OnAttack = true;

        Debug.Log("�I��");
        yield break;
        
    }
    void Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        if (tagObjects.Length >= 1)
        {
            OnAttack = false;
            create_bird_count = 0.0f;
        }
    }

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
