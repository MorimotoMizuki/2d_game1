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
    private float i = 0.05f;

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
        if (PlayerController.pose == false)
        {
            Vector3 playerPos = this.player.transform.position;//player�̃|�W�V�������擾

            if (transform.position.x < CPosleftx)
            {
                transform.position = new Vector3
               (transform.position.x, transform.position.y, transform.position.z);//��ʂ��Œ�
            }
            else if (BGM.BossStart)
            {
                if (transform.position.x > CPosright)
                {
                    transform.position = new Vector3
                   (transform.position.x, transform.position.y, transform.position.z);//�{�X��ʂ��Œ�
                }
                else
                {
                    bx.enabled = true;
                    transform.position = new Vector3
                    (transform.position.x + i, transform.position.y, transform.position.z);//��ʂ��X�N���[��
                }
            }
            else if (playerPos.x > PPosleftx && BGM.BossStart == false)
            {
                transform.position = new Vector3
               (playerPos.x, transform.position.y, transform.position.z);//player�ɒǏ]
            }


            this.player = GameObject.Find("��l��");
        }
    }
}
