using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{
    bool Sougenretry;
    bool Muraretry;
    bool Siroretry;
    public GameObject SougenretryButton;
    public GameObject MuraretryButton;
    public GameObject SiroretryButton;

    // Start is called before the first frame update
    void Start()
    {
        Sougenretry = PlayerController.SougenBoss;
        Muraretry = PlayerController.VillageBoss;
        //Siroretry = 

    }

    // Update is called once per frame
    void Update()
    {
        //�����X�e�[�W��Gameover
        if (Sougenretry == false)
        {
            MuraretryButton.gameObject.SetActive(false);//�����g���C���ꉞfalse�ɂ���
            SiroretryButton.gameObject.SetActive(false);//�郊�g���C���ꉞfalse�ɂ���
            SougenretryButton.gameObject.SetActive(true);
            Sougenretry = true;
        }
        //���X�e�[�W��Gameover
        else if (Muraretry == false)
        {
            SougenretryButton.gameObject.SetActive(false);//�������g���C���ꉞfalse�ɂ���
            SiroretryButton.gameObject.SetActive(false);//�郊�g���C���ꉞfalse�ɂ���
            MuraretryButton.gameObject.SetActive(true);
            Muraretry = true;
        }
        //��X�e�[�W��Gameover
        else if(SiroretryButton == false)
        {
            SougenretryButton.gameObject.SetActive(false);//�������g���C���ꉞfalse�ɂ���
            MuraretryButton.gameObject.SetActive(false);//�����g���C���ꉞfalse�ɂ���
            SiroretryButton.gameObject.SetActive(true);
            Siroretry = true;
        }
    }
}
