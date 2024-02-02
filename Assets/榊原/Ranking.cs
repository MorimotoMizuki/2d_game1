using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    //���ꂼ��̃X�e�[�W�̍ő��^�C���̋L���p�ϐ�
    static public int Sfastminute; 
    static public float SfastSecond;

    static public int Vfastminute;
    static public float VfastSecond;

    static public int Cfastminute;
    static public float CfastSecond;

    public Text timeSText;
    public Text timeVText;
    public Text timeCText;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.Splaying)//�����X�e�[�W
        {
            //�c�莞�Ԃ��傫���ق����L��
            if (Global.Ssecond > SfastSecond)
            {
                SfastSecond = Global.Ssecond;
                if (Global.Sminute > Sfastminute)
                {
                    Sfastminute = Global.Sminute;
                }
            }
        }
        else if (Global.Vplaying)//���X�e�[�W
        {
            //�c�莞�Ԃ��傫���ق����L��
            if (Global.Vsecond > VfastSecond)
            {
                VfastSecond = Global.Vsecond;
                if (Global.Vminute > Vfastminute)
                {
                    Vfastminute = Global.Vminute;
                }
            }
        }
        else if (Global.Cplaying)//��X�e�[�W
        {
            //�c�莞�Ԃ��傫���ق����L��
            if (Global.Csecond > CfastSecond)
            {
                CfastSecond = Global.Csecond;
                if (Global.Cminute > Cfastminute)
                {
                    Cfastminute = Global.Cminute;
                }
            }
        }

        if (SfastSecond < 10)
        {
            timeSText.text = Sfastminute.ToString("00") + ":0" + SfastSecond.ToString("f2");
        }
        else
        {
            timeSText.text = Sfastminute.ToString("00") + ":" + SfastSecond.ToString("f2");
        }

        if (VfastSecond < 10)
        {
            timeVText.text = Vfastminute.ToString("00") + ":0" + VfastSecond.ToString("f2");
        }
        else
        {
            timeVText.text = Vfastminute.ToString("00") + ":" + VfastSecond.ToString("f2");
        }

        if (CfastSecond < 10)
        {
            timeCText.text = Cfastminute.ToString("00") + ":0" + CfastSecond.ToString("f2");
        }
        else
        {
            timeCText.text = Cfastminute.ToString("00") + ":" + CfastSecond.ToString("f2");
        }

    }
}
