using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesIwa : MonoBehaviour
{
    public GameObject Iwa;

    //�u���b�N��������܂ł̎���
    private float targetTime = 2.0f;

    //�J�E���g���Ă��鎞�Ԃ�����ϐ�
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IwaCrash.Iwakieflag)
        {
            Debug.Log("��t���Otrue");
            currentTime += Time.deltaTime;
            Iwa.gameObject.SetActive(false);

            if (targetTime < currentTime)//2.0f
            {
                Iwa.gameObject.SetActive(true);
                Debug.Log("����");
                IwaCrash.Iwakieflag = false;
                IwaCrash.Iwaflag = false;
                currentTime = 0.0f;
            }
        }
    }
}
