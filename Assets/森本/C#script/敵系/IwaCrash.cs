using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaCrash : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[

    public GameObject Iwa;

    //�u���b�N��������܂ł̎���
    private float targetTime = 2.0f;

    //�J�E���g���Ă��鎞�Ԃ�����ϐ�
    private float currentTime = 0;

    //���ꂩ����Animation�𗬂��܂ł̎��Ԃ���̕ϐ���������̕ϐ�
    private float kamaetime = 1.5f;

    //Player���̂�����グ��t���O
    private bool Iwaflag = false;

    //�u���b�N���������鎞��
    private float revival = 2.0f;

    //��������܂ł̕b����}�鎞��
    private float revtime = 0.0f;

    private bool revflag = false;

    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Iwaflag)
        {
            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("����ꂩ��");//���ꂩ����Animation����
            }
            if (targetTime < currentTime)//2.0f
            {
               Iwa.gameObject.SetActive(false);
                //col.enabled = false;
                //Iwaflag = false;

                revflag = true;//����flag���グ��
            }
            if(4 <currentTime)
            {
                Debug.Log("����");
                Iwa.gameObject.SetActive(true);
                //col.enabled = true;
            }
        }

        //if(revflag)
        //{
        //    revtime += Time.deltaTime;
        //    Debug.Log(revtime);

        //    if(revtime>revival)
        //    {
        //        this.gameObject.SetActive(true);
        //        col.enabled = true;
        //    }
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("�̂���");
        if (collision.gameObject.tag == "Player")
        {
            Iwaflag = true;
            //Debug.Log("Player��������");
        }

    }
}
