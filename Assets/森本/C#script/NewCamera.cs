using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamera : MonoBehaviour
{
    public BoxCollider2D bx;
    GameObject player;
    //��ʐ���p
    private int CPosleftx = -3;
    private float CPosright = 224.5f;
    private int PPosleftx = 7;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("��l��");
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
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
        else if (transform.position.x > CPosright)
        {
            transform.position = new Vector3
           (transform.position.x, transform.position.y, transform.position.z);//�{�X��ʂ��Œ�
            bx.enabled=true;
        }
        else if(playerPos.x > PPosleftx)
        {
            transform.position = new Vector3
           (playerPos.x, transform.position.y, transform.position.z);//player�ɒǏ]
        }
       

        this.player = GameObject.Find("��l��");
    }
}
