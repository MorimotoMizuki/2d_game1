using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTutorialc : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[

    static public bool OyaF = false;
    static public int cnt = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt == 1)
        {
            OyaF = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (cnt == 0)
            {
                animator.Play("Heroani");
                cnt = 1;
                
            }
        }
    }
}
