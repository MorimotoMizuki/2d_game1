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
            OsaDs = true;
        }

        if (AniTutorialc.cnt == 3)
        {
            OsaF = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (AniTutorialc.OyaF )//�e�̃h���S�����o��
            {
                animator.Play("Dragonani");        
                AniTutorialc.OyaF = false;
                
                AniTutorialc.cnt = 2;
                
            }
            else if(OsaDs && AniTutorialc.cnt == 2)
            {
                Ara1.SetActive(false);
                Ara2.SetActive(true);

                animator.Play("DesDra");
                AniTutorialc.cnt = 3;

            }

        }
    }
}
