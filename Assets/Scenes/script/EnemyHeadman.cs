using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHeadman : MonoBehaviour
{
    public GameObject bird;//bird���擾
    public GameObject cane;//cane���擾
    public GameObject player;//player���擾
    public GameObject Object;//HPber�p

    private bool stop = false;//��������

    private int Headman_HP;//������HP
    public int HP = 100;    //�ő�HP

    //��l���̍U��
    private int rushdamage = 10;    //�ːi�̍U����
    private int buresball = 30;     //�΋��̍U����


    private bool inDamage = false;  //�_���[�W����
    private float reactionDistance = 15.0f;//��������

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
    public string Downanime = "EnemyHeadmanDown";    //�_�E�����[�V����

    //SE�p�a��
    [SerializeField]
    AudioSource swordAudioSource;
    //SE�p�񂽂���
    [SerializeField]
    AudioSource tueAudioSource;

    GameObject[] tagObjects;//�I�u�W�F�N�g�J�E���g�p

    //�t�F�[�h�p
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    //�p�[�e�B�N���p
    static public bool particleonV = false;
    //������肯��
    private byte transparent_count;

    //HP�o�[�̃V�F�C�_�[
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Object.gameObject.SetActive(false);
        stop = true;
        animator = GetComponent<Animator>();
        Headman_HP = HP;//�ő�HP��ݒ�
        OnAttack = true;
        transparent_count = 255;

        slider.value = 1;
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
                if (isActive && Headman_HP > 0)
                {

                    create_bird_count += Time.deltaTime;//�J�E���g
                    create_cane_count += Time.deltaTime;//�J�E���g
                                                        //Debug.Log(create_bird_count);
                    if (OnAttack)
                    {
                        if (create_bird_count > create_bird_time)
                        {
                            StartCoroutine(Bird_attack());//�R���[�`���J�n
                        }
                        else if (create_cane_count > create_cane_time)
                        {
                            StartCoroutine(Cane_attack());//�R���[�`���J�n
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
                        Object.gameObject.SetActive(true);//HP�o�[���A�N�e�B�u�ɂ���
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
    }
    
    //�R���[�`��
    private IEnumerator Bird_attack()
    {
        create_bird_count = 0.0f;//���Z�b�g
        OnAttack = false;

        //�A�j���V����
        animator.Play(attackanime);
        //SE
        tueAudioSource.Play();

        yield return new WaitForSeconds(0.2f);//0.2�Î~
        animator.Play(Stopanime);
        
        for(int i =0; i<10;i++)
        {
            if (HeadmanBird.HeadmanDown)
            {
                yield break;
            }
            //Debug.Log("������");
            float vecX = Random.Range(128f, 147f);//�����_�����i�j�͈͂Őݒ�
            var t = Instantiate(bird) as GameObject;//�I�u�W�F�N�g���쐬

            //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
            t.transform.position = new Vector3(vecX, 10.0f, 0);
            
            t.AddComponent<HeadmanBird>();//bird�̓��������߂�
            
            yield return new WaitForSeconds(0.2f);
        }
    
        yield return new WaitForSeconds(0.5f);//0.5�Î~

        OnAttack = true;
        create_cane_count = 2.0f;//���Z�b�g

        //Debug.Log("�I��");
        yield break;//�R���[�`���I��
        
    }
    private IEnumerator Cane_attack()
    {
        //�A�j���[�V����
        animator.Play(Caneanime);

        //SE
        swordAudioSource.Play();

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
            //SE
            GetComponent<AudioSource>().Play();

            slider.value = (float)Headman_HP / (float)HP;
        }
        if (collider.gameObject.tag =="Fireball")
        {
            //�_���[�W
            Headman_HP -= buresball;
            Debug.Log(Headman_HP);
            Destroy(collider.gameObject);
            inDamage = true;
            //SE
            GetComponent<AudioSource>().Play();
            slider.value = (float)Headman_HP / (float)HP;
        }
        EnemyDamage();//�|��Ă��邩���ׂ�
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
        if (Headman_HP <= 0)
        {
            TimeCounter.BossdownT = false;//�^�C�߁[���~�߂�t���O
            Debug.Log("�G���|��Ă���");
            PlayerController.VillageBoss = true;//���X�e�[�W�N���A
            PlayerController.stop = false;      //��l���̓������~�߂�
            PlayerController.gameState = "gameclear";//�N���A
            HeadmanBird.HeadmanDown = true;     //down�t���O��������
            stop = false;//������~
            StartCoroutine(Bossdown());//down�R���[�`��
        }
    }
    IEnumerator Bossdown()
    {
        Debug.Log("�Q�[���N���A");

        yield return new WaitForSeconds(0.2f);
        this.enabled = false;
        //Destroy(gameObject, 0.2f);//0.2�����ēG������

        particleonV = true;
        Global.Clear = true;
        animator.Play("EnemyHeadmanDown");
        for (; transparent_count > 0; transparent_count--)
        {
            //�{�X������������
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, transparent_count);
            Debug.Log(transparent_count);
            yield return new WaitForSeconds(0.01f);
            if (transparent_count == 1)
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


}
