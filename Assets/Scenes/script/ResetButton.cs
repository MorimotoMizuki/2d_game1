using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{    public void OnClickStartButton()//���Z�b�g�{�^������������
    {
        //�S�Ă̍U���t���O��false�ɂ���
        PlayerController.SougenBoss     = false;
        PlayerController.VillageBoss    = false;
        PlayerController.CastleBoss     = false;
    }
}
