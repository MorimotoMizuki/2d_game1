using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] GameObject home;
    [SerializeField] GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        //�z�[���ւ͍ŏ��A��\��false
        //�l���e�L�X�g�͍ŏ��A�\��true

        home.SetActive(false);
        text.SetActive(true);
        Invoke("homeSet", 2.0f);//���z�[���ւ��o������܂ł̕b��
    }

    void homeSet()
    {
        home.SetActive(true);
        Invoke("textSet", 2.0f);
    }

    void textSet()
    {
        text.SetActive(true);
    }
}
