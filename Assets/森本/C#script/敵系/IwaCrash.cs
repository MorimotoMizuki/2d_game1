using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwaCrash : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[

    private float targetTime = 2.0f;
    private float currentTime = 0;
    private float kamaetime = 1.5f;
    private bool Iwaflag = false;

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
            if (currentTime > (targetTime - kamaetime))//1.0f
            {
                animator.Play("����ꂩ��");
            }
            if (targetTime < currentTime)
            {
                Destroy(this.gameObject);
                Iwaflag = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("�̂���");
        if (collision.gameObject.tag == "Player")
        {
            Iwaflag = true;
            Debug.Log("Player��������");
        }

    }
}
