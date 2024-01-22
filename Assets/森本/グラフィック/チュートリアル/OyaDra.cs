using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyaDra : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    static public bool OsaF = false;
    static public bool OsaDs = false;
    public GameObject Ara1;
    public GameObject Ara2;

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 2)
        {
            OsaDs = true; //�Q�Ԗڂɗ���� DesDra �̃t���O OsaDs �� true �ɂ���
        }

        if (AniTutorialc.cnt == 3)
        {
            OsaF = true;//OsaDra.cs �� OsaF �� true �ɂ���
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (AniTutorialc.OyaF )//�e�̃h���S�����o�� �P�Ԗ�
            {
                animator.Play("Dragonani");        
                AniTutorialc.OyaF = false;//�Q��ڂ�����Ȃ��悤��false�ɂ���
                
                AniTutorialc.cnt = 2;
                
            }
            else if(OsaDs && AniTutorialc.cnt == 2)//�Q�Ԗ�
            {
                Ara1.SetActive(false);//���炷��1��������
                Ara2.SetActive(true); //���炷��2���o��

                animator.Play("DesDra");//�e�̃h���S����������
                AniTutorialc.cnt = 3;

            }

        }
    }
}
