using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsaDra : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    static public bool OsamF = false;
    static public bool SougenD = false;


    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 5)
        {
            SougenD = true;//SougenDra.cs �� SougenD �� true �ɂ���
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (OyaDra.OsaF && AniTutorialc.cnt == 3)//�R�Ԗ�
            {
                animator.Play("OsaDoraani");//�h���S�����t������
                OsamF = true;//�h���S����������t���O OsamF �� true �ɂ���
                AniTutorialc.cnt = 4;
            }
            else if (OsamF && AniTutorialc.cnt == 4)//�S�Ԗ�
            {
                animator.Play("OsaDraMove");//�c���h���S����������
                AniTutorialc.cnt = 5;
            }
        }

    }
}
