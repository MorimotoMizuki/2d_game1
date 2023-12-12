using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    //�e�̃v���n�u�I�u�W�F�N�g
    public GameObject tama;

    //root�U���̍��W�擾�I�u�W�F�N�g
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject Point21;
    public GameObject Point22;
    public GameObject Point23;
    public GameObject Point31;
    public GameObject Point32;
    public GameObject Point33;
    public GameObject Point41;
    public GameObject Point42;
    public GameObject Point43;
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;
    public GameObject DangerArea4;

    private float count = 5.0f;     //root�쐬�J�E���g�p
    public int hp = 50;             //�l�`�w����
    public float reactionDistance = 8.0f;//��������
    private float targetTime = 5.0f;
    private float currentTime = 0;
    private bool stop = false;//��������

    private int Torent_Hp;

    //��l���̍U��
    private int rushdamage = Global.GRush;
    private int buresball = Global.GBures;

    private bool inDamage = false;
    private bool isActive = false;
    private bool isLeafAttack = false;

    //SE�p
    [SerializeField]
    AudioSource leafAudioSource;

    [SerializeField]
    AudioSource rootAudioSource;



    //�������p�肷��
    int start = 1;
    int end = 4;
    List<int> numbers = new List<int>();
    bool random = false;
    public int i = 0;
    private int rnd = 0;
    bool DamageT = false;

    // Start is called before the first frame update
    void Start()
    {
        Torent_Hp = hp;
        stop = true;
        DamageT = true;

        for (int i = start; i <= end; i++)//���X�g�̑��
        {
            numbers.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (stop)
        {
            //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (isActive && Torent_Hp > 0)
                {
                    currentTime += Time.deltaTime;

                    if (targetTime < currentTime)
                    {
                        StartCoroutine(LeafAttack());
                        
                        
                    }

                    if (DamageT)
                    {
                        count -= Time.deltaTime;
                        if (count < 0)
                        {
                            Debug.Log("�J�n");
                            
                            StartCoroutine(RootRandom());
                        }
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

            if(isLeafAttack)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 3, 255);//�ԐF�ɓ_��
                //�񕜒��_�ł�����
                float val = Mathf.Sin(Time.time * 20);
                Debug.Log(val);
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
        if (other.gameObject.tag == "rushWall")
        {
            //�_���[�W
            Torent_Hp -= rushdamage;
            Debug.Log(Torent_Hp);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "Fireball")
        {
            //�_���[�W
            Torent_Hp -= buresball;
            Debug.Log(Torent_Hp);
            inDamage = true;
            Destroy(other.gameObject);//���������u���X������
            //SE
            GetComponent<AudioSource>().Play();
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }
    


    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Torent_Hp <= 0)
        {
            Debug.Log("�G���|��Ă���");
            DangerArea1.gameObject.SetActive(false);
            DangerArea2.gameObject.SetActive(false);
            DangerArea3.gameObject.SetActive(false);
            DangerArea4.gameObject.SetActive(false);
            PlayerController.SougenBoss = true;
            StartCoroutine(Bossdown());
        }
    }
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator Bossdown()
    {
        stop = false;
        PlayerController.stop = false;
        PlayerController.gameState = "gameclear";
        Debug.Log("�Q�[���N���A");

        yield return new WaitForSeconds(0.2f);
        this.enabled = false;
        //Destroy(gameObject, 0.2f);//0.2�����ēG������
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        SceneManager.LoadScene("GameClear");
        yield break;
    }

    void make_naihu()
    {
        EnemyBossGan.Naihu = true;
    }
    IEnumerator LeafAttack()
    {
        currentTime = 0;
        isLeafAttack = true;

        yield return new WaitForSeconds(0.4f);


        //�񕜃t���O���낷
        isLeafAttack = false;

        //�X�v���C�g�����Ƃɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.2f);


        //�G�̍��W��ϐ�pos�ɕۑ�
        var pos = this.gameObject.transform.position + transform.up * -1.7f;
        //�e�̃v���n�u���쐬
        var t = Instantiate(tama) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        t.AddComponent<TorentLeaf>();
        //make_naihu();
        //SE�t����
        leafAudioSource.Play();
        //currentTime = 0;

        yield break;
    }
    IEnumerator RootRandom()
    {
        count = 5.0f;
        DamageT = false;

        for (i = 0; i < 4; i++)
        {
            int index = Random.Range(0, numbers.Count);//�����_���擾

            rnd = numbers[index];//�g����悤��
            Debug.Log(rnd);

            numbers.RemoveAt(index);//�擾������������

            if (rnd == 1)
            {
                //�댯�G���A�\��
                DangerArea1.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Root1());
            }
            if (rnd == 2)
            {
                //�댯�G���A�\��
                DangerArea2.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Root2());
            }
            if (rnd == 3)
            {   //�댯�G���A�\��
                DangerArea3.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Root3());

            }
            if (rnd == 4)
            {   //�댯�G���A�\��
                DangerArea4.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Root4());

            }
            yield return new WaitForSeconds(1.0f);//�ҋ@
            if (i == 3)
            {
                random = true;  //���X�g�������̊J�n
                DamageT = true;
                yield break;
            }
        }

    }
    private IEnumerator Root1()
    {

        yield return new WaitForSeconds(2.0f);//�ҋ@

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos1 = Point1.gameObject.transform.position;
        var pos2 = Point2.gameObject.transform.position;
        var pos3 = Point3.gameObject.transform.position;

        //�e�̃v���n�u���쐬
        var t1 = Instantiate(ball) as GameObject;
        var t2 = Instantiate(ball) as GameObject;
        var t3 = Instantiate(ball) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        //SE
        rootAudioSource.Play();
        t1.transform.position = pos1;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t2.transform.position = pos2;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t3.transform.position = pos3;
        yield return new WaitForSeconds(0.2f);
        //�댯�G���A��\��
        DangerArea1.gameObject.SetActive(false);
    }
    private IEnumerator Root2()
    {

        yield return new WaitForSeconds(2.0f);//�ҋ@

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos1 = Point21.gameObject.transform.position;
        var pos2 = Point22.gameObject.transform.position;
        var pos3 = Point23.gameObject.transform.position;

        //�e�̃v���n�u���쐬
        var t1 = Instantiate(ball) as GameObject;
        var t2 = Instantiate(ball) as GameObject;
        var t3 = Instantiate(ball) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        //SE
        rootAudioSource.Play();
        t1.transform.position = pos1;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t2.transform.position = pos2;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t3.transform.position = pos3;
        yield return new WaitForSeconds(0.2f);
        //�댯�G���A��\��
        DangerArea2.gameObject.SetActive(false);
    }
    private IEnumerator Root3()
    {

        yield return new WaitForSeconds(2.0f);//�ҋ@

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos1 = Point31.gameObject.transform.position;
        var pos2 = Point32.gameObject.transform.position;
        var pos3 = Point33.gameObject.transform.position;

        //�e�̃v���n�u���쐬
        var t1 = Instantiate(ball) as GameObject;
        var t2 = Instantiate(ball) as GameObject;
        var t3 = Instantiate(ball) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        //SE
        rootAudioSource.Play();
        t1.transform.position = pos1;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t2.transform.position = pos2;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t3.transform.position = pos3;
        yield return new WaitForSeconds(0.2f);
        //�댯�G���A��\��
        DangerArea3.gameObject.SetActive(false);
    }
    private IEnumerator Root4()
    {

        yield return new WaitForSeconds(2.0f);//�ҋ@

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos1 = Point41.gameObject.transform.position;
        var pos2 = Point42.gameObject.transform.position;
        var pos3 = Point43.gameObject.transform.position;

        //�e�̃v���n�u���쐬
        var t1 = Instantiate(ball) as GameObject;
        var t2 = Instantiate(ball) as GameObject;
        var t3 = Instantiate(ball) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        //SE
        rootAudioSource.Play();
        t1.transform.position = pos1;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t2.transform.position = pos2;
        yield return new WaitForSeconds(0.2f);
        //SE
        rootAudioSource.Play();
        t3.transform.position = pos3;
        yield return new WaitForSeconds(0.2f);
        //�댯�G���A��\��
        DangerArea4.gameObject.SetActive(false);
    }





}




