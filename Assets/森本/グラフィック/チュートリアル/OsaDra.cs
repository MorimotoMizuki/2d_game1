using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsaDra : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    static public bool OsamF = false;

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
            if(OsamF)
            {
                animator.Play("OsaDraMove");
            }

            if (OyaDra.OsaF)
            {
                animator.Play("OsaDoraani");
                OsamF = true;
            }
        }

    }
}
