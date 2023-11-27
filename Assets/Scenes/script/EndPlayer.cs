using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EndPlayer : MonoBehaviour
{
    Rigidbody2D rb;              //Rigidbody�^�̕ϐ�

    //
    private int rush = 8;
    private float speed = 2.5f;

    //�t���O�֌W
    private bool move = false;
    private bool Onrush = false;

    //�A�j���[�V�����Ή�
    Animator animator; //�A�j���[�^�[
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string rushAnime = "PlayerRushend";
    string nowAnime = "";
    string oldAnime = "";

    //SE�p
    [SerializeField]
    AudioSource flameAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D�������Ă���
        rb = GetComponent<Rigidbody2D>();

        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;

        move = true;//�t���O��������
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.velocity = new Vector2(1.0f * speed, rb.velocity.y);
            animator.Play(moveAnime);
        }
        else if(Onrush)
        {
            animator.Play(rushAnime);
        }
        else
        {  
            animator.Play(stopAnime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "kingStart")
        {
            move = false;
           
            StartCoroutine(Rush());
        }
    }
    IEnumerator Rush()
    {
        rb.velocity = new Vector2(0, 0);//���x���~�߂�
        yield return new WaitForSeconds(1.0f);//�ҋ@
       
        Vector2 rushPw = new Vector2(rush, 0);
        rb.AddForce(rushPw, ForceMode2D.Impulse);
        Onrush = true;//�t���O���グ��
        yield return new WaitForSeconds(1.0f);//�ҋ@

        rb.velocity = new Vector2(0, 0);//�~�߂�
        move = true;//�t���O���グ��
        Onrush = false;//�t���O�����낷
        
        yield break;
    }
}