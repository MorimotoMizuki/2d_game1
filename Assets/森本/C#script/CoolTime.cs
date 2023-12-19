using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime : MonoBehaviour
{
    bool kari = false;
    bool kariK = false;
    public GameObject TossinCoolTime;
    public GameObject KakyuCoolTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.isTimeOver == true)//�N�[���^�C������Ȃ����͒ʏ�
        {
            TossinCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        else if(PlayerController.isTimeOver == false)//�N�[���^�C�����͔�������
        {
            TossinCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 130);
        }

        if(PlayerController.SougenBoss)//�P�X�e�[�W��CLEAR���Ă��Ȃ��Ɖ΋��̃N�[���^�C���O���t�B�b�N��\�������Ȃ�
        {
            KakyuCoolTime.SetActive(true);

            if (PlayerController.K_isTimeOver == true)//�N�[���^�C������Ȃ����͒ʏ�
            {
                KakyuCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            else if (PlayerController.K_isTimeOver == false)//�N�[���^�C�����͔�������
            {
                KakyuCoolTime.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 130);
            }

        }
        else
        {
            KakyuCoolTime.SetActive(false);
        }
    }
}
