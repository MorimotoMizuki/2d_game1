using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeEND : MonoBehaviour
{
    Animator animator; //�A�j���[�^�[
    public GameObject Endscene;
    public GameObject Button;
    public bool ones = true;

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ones)
        {
            if (ToEnd.END_ANI)
            {
                animator.Play("ENDscenes");
                Button.gameObject.SetActive(true);
                ones = false;
            }
        }
    }
}
