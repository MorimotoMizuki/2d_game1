using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyaDra : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    static public bool OsaF = false;

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (AniTutorialc.OyaF)
            {
                animator.Play("Dragonani");
                OsaF = true;
                AniTutorialc.OyaF = false;
            }
        }
    }
}
