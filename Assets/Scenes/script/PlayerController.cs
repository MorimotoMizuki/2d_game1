using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody�^�̕ϐ�
    float axisH = 0.0f;         //����
    public LayerMask GroundLayer;
    public Slider slider;
    public GameObject tama;
    public CapsuleCollider2D bx;

    public float speed = 3.0f;  //�ړ����x
    public float jump = 6.0f;   //�W�����v��
    public float rush = 2.0f;   //�ːi�̗�
    public int D_HP;          �@//�h���S����HP
    private int Max_D_HP;       //�ő�HP�ۑ��p

    private int S_D_HP = 50;     //�����ł̃h���S��HP
    private int V_D_HP = 100;    //���ł�HP
    private int C_D_HP = 150;    //��ł�HP

    public static string gameState = "playing";//�Q�[���̏��
    static public bool stop = false;

    #region//�G�̍U��
    private int Suraimu = 5;    //�X���C���̃_���[�W
    private int Goburin = 5;    //�S�u�����̃_���[�W
    private int touzokugan = 15;//�����̉������U���̃_���[�W
    private int artillery = 25; //��C�̍U��
    private int bird = 5;       // ���̍U��
    private int cane = 20;      // ��̍U��
    private int stone = 10;     //�q���̐΍U��
    private int famer = 15;     //�_���̍U��
    private int mercenary =20;  //�b���̍U��
    private int arrow = 15;     //�|�g���̍U��
    private int knight = 30;     //�R�m�̍U��
    private int Explosion = 40;  //���U��
    private int witch = 20;      //�����̍U��
    private int caliver = 30;    //�R���̍U��
    private int toge = 10;�@�@�@�@//�j�̍U��
    private int thunder = 40;     //���U��
    private int heroattack = 30; //�a���U��
    private int TLeaf = 10;      //�t���ύU��
    #endregion

    #region//��l���̓����֌W�t���O
    bool gojump = false;                     //�W�����v����
    bool ongrond = false;                    //�n�ʔ���
    public static bool gorush = false;       //�U������(�ːi)
    bool Fireball_F = false;                 // �΋��U������
    static public bool horizon = true;       //����
    bool inDamage = false;                   //�_���[�W���t���O
    bool inrecovery = false;                 //�񕜒��t���O

    //�Z�̃t���O
    static public bool SougenBoss = false;
    static public bool VillageBoss = false;
    static public bool CastleBoss = false;

    //�񕜃A�C�e��
    private int meat = Global.GRecoveryMeat;

    //clear��̓���
    public int CJump = 0;
    #endregion

    //�N�[���^�C��
    public bool isCountDown = true;//true = ���Ԃ��J�E���g�_�E���v�Z����
    public bool AnimeCount = true;
    float rush_time = 1.5f;          //�U��(�ːi)�N�[���^�C��
    static public bool isTimeOver = false;//true = �^�C�}�[��~
    public bool animeOver = true;
    public float displayTime = 0;  //�\������
    public float Animetime = 0;
    public float animerushtime = 2.0f;

    //�N�[���^�C���΋�
    public bool K_isCountDown = true; //true = ���Ԃ��J�E���g�_�E���v�Z����
    private float Onbures = 2.0f;     //�U���i�΋��j�N�[���^�C��
    static public bool K_isTimeOver = false;//true = �^�C�}�[��~
    public float buresutime = 0;      //�\������
    float K_timesnow = 0;             //���ݎ���

    float times = 0;               //���ݎ���
    float Anitimes = 0;

    //�A�j���[�V�����Ή�
    Animator animator; //�A�j���[�^�[
    public string stopAnime  = "PlayerStop";
    public string moveAnime  = "PlayerMove";
    public string jumpAnime  = "PlayerJump";
    public string rushAnime  = "PlayerRush";
    public string clearAnime = "PlayerClear";
    string nowAnime = "";
    string oldAnime = "";

    //�Q�[���X�e�[�^�X�Ǘ��t���O
    static public bool pose = false;

    //SE�p
    [SerializeField]
    AudioSource flameAudioSource;

    //�V�[���؂�ւ��p
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        stop = true;

        //Rigidbody2D�������Ă���
        rb = GetComponent<Rigidbody2D>();

        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        //�Q�[���̏�Ԃ��v���C���ɂ���
        gameState = "playing";

        if(SougenBoss)
        {
            D_HP = V_D_HP;
            Max_D_HP = V_D_HP;
            slider.maxValue = 2;
            slider.value = 2;
            if (VillageBoss)
            {
                D_HP = C_D_HP;
                Max_D_HP = C_D_HP;
                slider.maxValue = 3;
                slider.value = 3;
            }
        }
        else
        {
            slider.value = 1;
            D_HP = S_D_HP;
            Max_D_HP = S_D_HP;
        }
        if (isCountDown)
        {
            //�J�E���g�_�E��
            displayTime = rush_time;
        }
        if(AnimeCount)
        {
            Animetime = animerushtime;
        }
        if(K_isCountDown)
        {
            buresutime = Onbures;
        }
        //�ːi
        Animetime = 0.0f;
        animeOver = true;  //�t���O�����낷
        gorush = false; //�U���t���O�����낷
        isTimeOver = false;
        Anitimes = 0;
        times = 0;

        //�΋�
        K_timesnow = 0;
        K_isTimeOver = false;
        Fireball_F = false;

    }

    // Update is called once per frame
    void Update()
    {
       
      
        //�Q�[�����ȊO�ƃ_���[�W���͉������Ȃ�
        if (stop)
        {
            //�Q�[���X�e�[�^�X�Ǘ�
            if (pose)
            {
                Time.timeScale = 0;
                gameState = "posing";
                rb.isKinematic = true;
            }
            else
            {
                Time.timeScale = 1;
                gameState = "playing";
                rb.isKinematic = false;
            }
            //�Q�[�����ȊO�ƃ_���[�W���͉������Ȃ�
            if (gameState != "playing")
            {
                rb.velocity = new Vector2(0, 0);
                animator.Play(stopAnime);
                return;
            }
            //���������̃`�F�b�N
            axisH = Input.GetAxisRaw("Horizontal");

            //�����̒���
            if (axisH > 0.0f)
            {
                //�E�ړ�
                // Debug.Log("�E�ړ�");
                transform.localScale = new Vector2(5, 5);
                horizon = true;
            }
            if (axisH < 0.0f)
            {
                //���ړ�
                //Debug.Log("���ړ�");
                transform.localScale = new Vector2(-5, 5);
                horizon = false;
            }
            //�L�����N�^�[�̃W�����v
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            //�L�����N�^�[�̓ːi�U��
            if (ongrond)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Rush();
                }
            }
            //�L�����N�^�[�̉΋�
            if (Input.GetMouseButtonDown(1) && SougenBoss)
            {
                Fireball();
            }

            //���Ԍo�߃^�b�N��
            if (isTimeOver == false)
            {
                times += Time.deltaTime;//�o�ߎ��Ԃ����Z
                if (isCountDown)
                {
                    //�J�E���g�_�E��
                    displayTime = rush_time - times;
                    if (displayTime <= 0.0f)
                    {
                        displayTime = 0.0f;
                        isTimeOver = true;  //�t���O�����낷
                    }
                }
            }
            //���Ԍo�߉΋�
            if (K_isTimeOver == false)
            {
                K_timesnow += Time.deltaTime;//�o�ߎ��Ԃ����Z
                if (K_isCountDown)
                {
                    //�J�E���g�_�E��
                    buresutime = Onbures - K_timesnow;
                    if (buresutime <= 0.0f)
                    {
                        buresutime = 0.0f;
                        K_isTimeOver = true;//�t���O������
                    }
                }

            }
        }
        else
        {
            if (Global.Clear)
            {
                StartCoroutine(ClearMove());
            }
            else
            {
                animator.Play(stopAnime);
                Debug.Log("stop");
            }
        }
        
    }



    private void FixedUpdate()
    {
        //�n�㔻��
        ongrond = Physics2D.Linecast(transform.position,
                                     transform.position - (transform.up * 0.1f),
                                     GroundLayer);

        //�Q�[�����ȊO�͉������Ȃ�
        if (stop)
        {
            if(gameState != "playing")
            {
                return;
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
            if (inrecovery)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 3, 255);
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
            if (ongrond || axisH != 0 || gorush == true)
            {
                //�n��or���x���O�ł͂Ȃ�or�U�����ł͂Ȃ�
                //���x���X�V
                rb.velocity = new Vector2(axisH * speed, rb.velocity.y);
            }
            if (ongrond && gojump)
            {
                //�n�ォ�W�����v�L�[�������ꂽ�Ƃ�
                //�W�����v����
                Debug.Log("�W�����v");
                Vector2 jumpPw = new Vector2(0, jump);      //�W�����v������x�N�g��
                rb.AddForce(jumpPw, ForceMode2D.Impulse);   //�u�ԓI�ȗ͂�������
                gojump = false; //�W�����v�t���O�����낷
            }
            if (VillageBoss && gojump)
            {
                Debug.Log("�W�����v");
                Vector2 jumpPw2 = new Vector2(0, jump);      //�W�����v������x�N�g��
                rb.AddForce(jumpPw2, ForceMode2D.Impulse);   //�u�ԓI�ȗ͂�������
                gojump = false; //�W�����v�t���O�����낷
            }

            if (gorush && horizon == true)
            {
                //�n�ォ���N���b�N�������ꂽ�Ƃ����E����
                //�ːi����
                Debug.Log("�ːi");

                //SE�@�ːi
                GetComponent<AudioSource>().Play();

                Vector2 rushPw = new Vector2(rush, 0);
                rb.AddForce(rushPw, ForceMode2D.Impulse);

                if (animeOver == false)//���Ԍo��
                {
                    Anitimes += Time.deltaTime;//�o�ߎ��Ԃ����Z
                    if (AnimeCount)
                    {
                        //�J�E���g�_�E��
                        Animetime = animerushtime - Anitimes;
                        if (Animetime <= 0.0f)
                        {
                            Animetime = 0.0f;
                            animeOver = true;  //�t���O�����낷
                            gorush = false; //�U���t���O�����낷
                            displayTime = rush_time;
                            Animetime = animerushtime;
                            isTimeOver = false;
                            Anitimes = 0;
                            times = 0;
                            rb.velocity = Vector2.zero;//�ǉ�
                        }

                    }
                    //   Debug.Log("TIMES:" + Animetime);
                }
            }
            else if (gorush && horizon == false)
            {
                //�n�ォ���N���b�N�������ꂽ�Ƃ���������
                //�ːi����
                Debug.Log("�ːi");

                //SE �ːi
                GetComponent<AudioSource>().Play();

                Vector2 rushPw = new Vector2(-rush, 0);
                rb.AddForce(rushPw, ForceMode2D.Impulse);

                if (animeOver == false)//���Ԍo��
                {
                    Anitimes += Time.deltaTime;//�o�ߎ��Ԃ����Z
                    if (AnimeCount)
                    {
                        //�J�E���g�_�E��
                        Animetime = animerushtime - Anitimes;
                        if (Animetime <= 0.0f)
                        {
                            Animetime = 0.0f;
                            animeOver = true;  //�t���O�����낷
                            gorush = false; //�U���t���O�����낷
                            displayTime = rush_time;
                            Animetime = animerushtime;
                            isTimeOver = false;
                            Anitimes = 0;
                            times = 0;
                            rb.velocity = Vector2.zero;//�ǉ�
                        }

                    }
                    Debug.Log("TIMES:" + Animetime);
                }
            }
            if (Fireball_F)
            {
                //��l���̍��W��ϐ�pos�ɕۑ�
                var posR = this.gameObject.transform.position + (transform.up * 1.5f) + transform.right * 1.2f;
                var posL = this.gameObject.transform.position + (transform.up * 1.5f) - transform.right * 1.2f;
                //�e�̃v���n�u���쐬
                var t = Instantiate(tama) as GameObject;
                //�e�̃v���n�u�̈ʒu���ʒu�ɂ���

                if (horizon)
                {
                    t.transform.position = posR;
                    t.AddComponent<Playerboll>();
                }
                else
                {
                    t.transform.position = posL;
                    t.AddComponent<Playerboll2>();
                }
                flameAudioSource.Play();
                Debug.Log("�΋�");
                buresutime = Onbures;   //�J�E���g�_�E�����Ԃ̃��Z�b�g
                K_timesnow = 0;         //�\�����Ԃ̃��Z�b�g
                K_isTimeOver = false;   //�t���O��������
                Fireball_F = false;     //�t���O�����낷
                                        //SE�@�΋�
                GetComponent<AudioSource>().Play();

            }

            //�A�j���[�V����
            if (ongrond)
            {
                //�n��̂���
                if (axisH == 0)
                {
                    nowAnime = stopAnime; //��~��
                }
                else
                {
                    nowAnime = moveAnime; //�ړ�
                }
                if (gorush)
                {
                    nowAnime = rushAnime;
                    Debug.Log("�A�j���[�V�����I");
                }
            }
            else
            {
                //��
                nowAnime = jumpAnime;
            }



            if (nowAnime != oldAnime)
            {
                oldAnime = nowAnime;
                animator.Play(nowAnime);    //�A�j���[�V�����Đ�
            }
        }
        else
        {
            if (Global.Clear)
            {
                StartCoroutine(ClearMove());
            }
            else
            {
                animator.Play(stopAnime);
                Debug.Log("stop");
            }
        }
    }

    //��l���ɓ���
    void Jump()//�W�����v
    {
        gojump = true; //�W�����v�t���O�𗧂Ă�
    }
    void Rush()//�ːi
    {
        if (displayTime == 0)
        {
            gorush = true; //�U��(�ːi)�t���O�𗧂Ă�
            animeOver = false;

        }
    }
    void Fireball()//�΋�
    {
        if (buresutime == 0)
        {
            Fireball_F = true;
        }
    }


    //�ڐG�J�n�_���[�W
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inDamage)
        {
            if (collision.gameObject.tag == "damege_s")//�X���C��
            {
                Debug.Log("�ڐG");
                if (gorush == false)
                {
                    D_HP -= Suraimu;    //HP�����炷
                    slider.value = (float)D_HP / (float)S_D_HP; ;
                    Debug.Log("slider.value : " + slider.value);
                    GetDamage(collision.gameObject);
                }
            }
            if (collision.gameObject.tag == "damage_g")//�S�u����
            {
                Debug.Log("�ڐG");
                if (gorush == false)
                {
                    D_HP -= Goburin;    //HP�����炷�i�S�u�����̍U���j
                    GetDamage(collision.gameObject);
                    slider.value = (float)D_HP / (float)S_D_HP; ;
                    Debug.Log("slider.value : " + slider.value);
                    GetDamage(collision.gameObject);
                }
            }
            if (collision.CompareTag("tama"))
            {
                D_HP -= touzokugan;     //HP�����炷�i�����̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("TLeaf"))
            {
                D_HP -= TLeaf;     //HP�����炷�i�����̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("artillery"))
            {
                D_HP -= artillery;     //HP�����炷�i��C�̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("bird"))
            {
                D_HP -= bird;       //HP�����炷�i�����̒��j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == "cane")
            {
                D_HP -= cane;       //HP�����炷�i�����̏�j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("stone"))
            {
                D_HP -= stone;       //HP�����炷�i�����̏�j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("stone"))
            {
                D_HP -= stone;       //HP�����炷�i�΂̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("arrow"))
            {
                D_HP -= arrow;       //HP�����炷�i�|�g���̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("famer"))
            {
                D_HP -= famer;       //HP�����炷�i�����̏�j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("mercenary"))
            {
                D_HP -= mercenary;       //HP�����炷�i�b���̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("knight"))
            {
                D_HP -= knight;       //HP�����炷�i�R�m�̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("Explosion"))
            {
                D_HP -= Explosion;       //HP�����炷�i���U���̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("caliver"))
            {
                D_HP -= caliver;       //HP�����炷�i�R���̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("witch"))
            {
                D_HP -= witch;       //HP�����炷�i�����̍U���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == ("upper"))
            {
                D_HP -= toge;       //HP�����炷�i�����̍U���j
                GetDamage(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("heroattack"))
            {
                D_HP -= heroattack;       //HP�����炷�i�E�҂̎a���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.CompareTag("thunder"))
            {
                D_HP -= thunder;       //HP�����炷�i�E�҂̖��@���j
                GetDamage(collision.gameObject);
                Destroy(collision.gameObject);
                slider.value = (float)D_HP / (float)S_D_HP; ;
                Debug.Log("slider.value : " + slider.value);
                GetDamage(collision.gameObject);
            }
            if (collision.gameObject.tag == "meat")//Hp����
            {
                if(D_HP != Max_D_HP)
                {
                    D_HP += meat;
                    Destroy(collision.gameObject);//���������I�u�W�F�N�g���폜
                    if(D_HP > Max_D_HP)
                    {
                        D_HP = Max_D_HP;
                    }
                    slider.value = (float)D_HP / (float)S_D_HP; ;
                    Debug.Log("slider.value : " + slider.value);
                    GetRecovery();
                } 
            }
        }
        if (collision.gameObject.tag == "dead")
        {
            GameOver();
        }
    }

    //�_���[�W���󂯂����̓���
    public void GetDamage(GameObject @object)
    {

        Debug.Log("Player HP" + D_HP);
        if (D_HP > 0)
        {
            //�ړ���~
            rb.velocity = new Vector2(0, 0);
            //�G�L�����̔��Α��Ƀq�b�g�o�b�N������
            Vector3 v = (this.transform.position - @object.transform.position).normalized;
            rb.AddForce(new Vector2(v.x * 5, v.y * 5), ForceMode2D.Impulse);
            //�_���[�W�t���OON
            inDamage = true;
            Invoke("DamageEnd", 0.5f);
        }
        else
        {
            //�Q�[���I�[�o�[
            GameOver();
        }
    }
    //�_���[�W�I��
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //�񕜒�
    void GetRecovery()
    {
        inrecovery = true;
        Invoke("RecoveryEnd", 0.5f);
    }
    //�񕜏I��
    void RecoveryEnd()
    {
        //�񕜃t���O���낷
        inrecovery = false;
        //�X�v���C�g�����Ƃɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
    //�Q�[���I�[�o�[
    void GameOver()
    {
        Debug.Log("�Q�[���I�[�o�[");
        gameState = "gameover";
        stop = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(GameOverT());
    }
    IEnumerator GameOverT()
    {
        
        new Vector3(transform.position.x, 0, 0);
        bx.enabled = false;
        transform.localRotation = new Quaternion(180.0f, 0.0f, 0.0f, 0.0f);
        rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
       

        
        yield return new WaitForSeconds(2.0f);
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        //SceneManager.LoadScene("Gameover");
    }
    IEnumerator ClearMove()
    {
        bx.enabled = false;
        rb.isKinematic = true;
        Debug.Log("����������������������������������������");
        this.transform.position = new Vector3(transform.position.x,-2.2f, 0);
        animator.Play(clearAnime);
        yield break;
    }
}