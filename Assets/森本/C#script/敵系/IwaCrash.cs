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
    static public bool Iwaflag = false;

    static public bool Iwakieflag = false;

    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Iwaflag)
        {
            currentTime += Time.deltaTime;
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("����ꂩ��");//���ꂩ����Animation����
            }
            if (targetTime < currentTime)//2.0f
            {
               Iwa.gameObject.SetActive(false);
            }
        }
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
