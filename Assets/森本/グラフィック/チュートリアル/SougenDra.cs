using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SougenDra : MonoBehaviour
{
    //�A�j���[�V�����Ɏg��
    Animator animator; //�A�j���[�^�[
    public GameObject SIRO;
    public GameObject KURO;
    public GameObject KUSA;

    public GameObject Ara2;
    public GameObject Ara3;

    static public bool ToHome = false;

    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Animator ���Ƃ��Ă���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AniTutorialc.cnt == 6)
        {
            ToHome = true;//Home�V�[���Ƀt�F�[�h�����邽�߂̃t���O��true�ɂ���
        }

        if (Input.GetMouseButtonDown(0))
        {
           if(OsaDra.SougenD && AniTutorialc.cnt == 5)//�T�Ԗ�
           {
                animator.Play("SougenDra");//�����̃h���S����������o�� 
                AniTutorialc.cnt = 6;

                SIRO.SetActive(true);//�����̔w�i���o��
                KURO.SetActive(true);
                KUSA.SetActive(true);

                Ara2.SetActive(false);//���炷��2������
                Ara3.SetActive(true);//���炷��3���o��

           }
           else if(ToHome && AniTutorialc.cnt == 6)// �U�Ԗ�
            {
                Initiate.Fade(sceneName, fadeColor, fadeSpeed);//Home�ɃV�[����؂�ւ��� �t�F�[�h�A�E�g
            }
        }

    }
}

