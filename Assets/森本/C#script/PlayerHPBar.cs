using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    //�ő�HP�ƌ��݂�HP
    int maxHp = 50;
    int currentHp;

    //Slider������
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //Slider�𖞃^���ɂ���
        slider.value = 1;

        //���݂�HP���ő�HP�Ɠ����ɂ���
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }

    //Colleder�I�u�W�F�N�g��IsTrigger�Ƀ`�F�b�N�����邱��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemy�^�O�̃I�u�W�F�N�g�ɐG���Ɣ���
        if (collision.CompareTag("Enemy"))
        {
            //�_���[�W��10
            int damage = 10;
            Debug.Log("damage : " + damage);

            //���݂�HP����_���[�W������
            currentHp = currentHp - damage;
            Debug.Log("After curretHp : " + currentHp);

            //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f
            //int���m�̊���Z�͏����_�ȉ���0�ɂȂ�̂ŁA
            //float������float�ϐ��Ƃ��Đk�킹��B
            slider.value = (float)currentHp / (float)maxHp; ;
            Debug.Log("slider.value : " + slider.value);
        }
    }

}
