using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    GameObject player;
    //��ʐ���p
    private int CPosleftx = -3;
    private int PPosleftx = 7;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("��l��");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//player�̃|�W�V�������擾
       
        if(transform.position.x < CPosleftx)
        {
            transform.position = new Vector3
           (transform.position.x, transform.position.y, transform.position.z);//��ʂ��Œ�
        }
        else if(playerPos.x > PPosleftx)
        {
            transform.position = new Vector3
           (playerPos.x, transform.position.y, transform.position.z);//player�ɒǏ]
        }

        this.player = GameObject.Find("��l��");
    }
}
