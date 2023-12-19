using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool stop = false;//��������

    Rigidbody2D rb;

    //5�b���Ƃɒe�𔭎˂��邽�߂̂���
    private float targetTime = 5.0f;
    private float currentTime = 0;
    private bool count = true;

    public int hp = 300;
    public float reactionDistance = 20.0f;//��������

    private int Hero_Hp;

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����

    private bool inDamage = false;
    private bool isActive = false;

    private int oldHP;//HP�L���p
    private int rnd;//�����_���i�[�p
    public int i;//for�p

    private bool DamageT = true;//false�̎��T���_�[�U���ȊO���Ȃ�

    //�T���_�[�����_���p�肷��
    int start = 1;
    int end = 3;
    List<int> numbers = new List<int>();
    bool random = false;

    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    public string stopAnime = "StopMove";
    public string attack    = "attack";
    public string attackT   = "attackT";
    public string HeroDown = "EnemyHeroDown";

    //�p�[�e�B�N���p
    static public bool particleonC = false;
    //�����������p
    private byte transparent_count;

    //�t�F�[�h�p
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    private void Start()
    {
        //Rigidbody2D ���Ƃ�
        rb = GetComponent<Rigidbody2D>();
        Hero_Hp = hp;
        oldHP = hp;
        animator = GetComponent<Animator>();
        stop = true;
        transparent_count = 255;

        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (stop)
        {
            //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && DamageT)
            {
                if (isActive && Hero_Hp > 0)
                {
                    if (count)
                    {
                        currentTime += Time.deltaTime;
                        //Debug.Log(currentTime);
                    }
                    if (targetTime < currentTime)
                    {
                        StartCoroutine(Slashing()); //�a���R���[�`��
                    }

                    if (Hero_Hp != oldHP)
                    {
                        DamageT = false;//���ׂĂ̓�����~
                        StartCoroutine(CountT());//�J�E���g�T���_�[�R���[�`��
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
            if (random)//���X�g�̔z��쐬
            {
                for (int i = start; i <= end; i++)
                {
                    numbers.Add(i);
                }
                random = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (other.gameObject.tag == "rushWall" && DamageT)
        {
            //�_���[�W
            Hero_Hp -= rushdamage;
            Debug.Log(Hero_Hp);
            inDamage = true;
        }
        if (other.gameObject.tag == "Fireball" && DamageT)
        {
            //�_���[�W
            Hero_Hp -= buresball;
            Debug.Log(Hero_Hp);
            Destroy(other.gameObject);
            inDamage = true;
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    //�R���[�`��
    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Hero_Hp <= 0)
        {
            TimeCounter.BossdownT = false;//�^�C�߁[���~�߂�t���O
            StartCoroutine(Bossdown());
            DangerArea1.gameObject.SetActive(false);//�|��Ă���ꍇ�͎��s���Ȃ�
            DangerArea2.gameObject.SetActive(false);//�|��Ă���ꍇ�͎��s���Ȃ�
            DangerArea3.gameObject.SetActive(false);//�|��Ă���ꍇ�͎��s���Ȃ�
            Debug.Log("�G���|��Ă���");
            stop = false;
            PlayerController.stop = false;
            PlayerController.gameState = "gameclear";
            Deletethunder.HeroDown = true;
        }
    }
    IEnumerator Bossdown()
    {
       
        Debug.Log("�Q�[���N���A");

        yield return new WaitForSeconds(0.2f);
        this.enabled = false;
        //Destroy(gameObject, 0.2f);//0.2�����ēG������

        particleonC = true;//�p�[�e�B�N���p�t���O��������
        Global.Clear = true;
        animator.Play(HeroDown);
        for (; transparent_count > 0; transparent_count--)
        {
            //�{�X������������
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, transparent_count);
            Debug.Log(transparent_count);
            yield return new WaitForSeconds(0.01f);
            if (transparent_count == 0)
            {
                yield return new WaitForSeconds(3.0f);
                Destroy(gameObject);

                Initiate.Fade(sceneName, fadeColor, fadeSpeed);
                yield break;
            }
        }
      
    }
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator Slashing()//�a���R���[�`��
    {
        count = false;//�a���̃J�E���g���~
        currentTime = 0;//�^�C�����Z�b�g
        for (i = 0; i < 3; i++)
        {
            //Debug.Log(i);
            animator.Play(attack);//�a�����[�V����
            yield return new WaitForSeconds(0.2f);//�ҋ@
            //�G�̍��W��ϐ�pos�ɕۑ�
            var pos = this.gameObject.transform.position;
            //�e�̃v���n�u���쐬
            var t = Instantiate(tama) as GameObject;
            //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            t.transform.position = pos;
            t.AddComponent<HeroGan>();//�������Z�b�g
            yield return new WaitForSeconds(0.1f);//�ҋ@
            animator.Play(stopAnime);//���ɖ߂�
            yield return new WaitForSeconds(0.5f);//�ҋ@
            if (i == 2)
            {
                count = true;//�J�E���g�J�n
                yield break;//�I��
            }
        }
    }

    IEnumerator CountT()//�J�E���^�[�T�����[�R���[�`��
    {

        for (i = 0; i < 3; i++)
        {
            int index = Random.Range(0, numbers.Count);//�����_���擾

            rnd = numbers[index];//�g����悤��
            //Debug.Log(rnd);

            numbers.RemoveAt(index);//�擾������������

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
            yield return new WaitForSeconds(1.0f);//�ҋ@
            if (i == 2)
            {
                //Debug.Log("�I���T���_�[");
                random = true;  //���X�g�������̊J�n
                DamageT = true;//�����J�n
                if(currentTime < 4.0f)
                {
                    currentTime = 4.0f;
                }
                yield break;
            }
        }


    }

    private IEnumerator Thunder1()
    {
        animator.Play(attackT);
       
        yield return new WaitForSeconds(2.0f);//�ҋ@

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point1.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea1.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }
    private IEnumerator Thunder2()
    {
        animator.Play(attackT);
        
        yield return new WaitForSeconds(2.0f);//�ҋ@
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point2.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea2.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }
    private IEnumerator Thunder3()
    {
        animator.Play(attackT);
       
        yield return new WaitForSeconds(2.0f);//�ҋ@
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point3.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea3.gameObject.SetActive(false);
        animator.Play(stopAnime);
        
    }

}
