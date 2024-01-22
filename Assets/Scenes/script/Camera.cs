using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public BoxCollider2D bx;
    GameObject player;
    //��ʐ���p
    private float   CPosleftx = -2.79f;
    private float CPosright = 131.5f;
    private float   PPosleftx = -2.79f;
    private float CScrollx = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("��l��");
        bx = GetComponent<BoxCollider2D>();
        bx.enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (PlayerController.pose == false)
        {
            Vector3 playerPos = this.player.transform.position;//player�̃|�W�V�������擾

            if (transform.position.x < CPosleftx)
            {
                transform.position = new Vector3
               (transform.position.x, transform.position.y, transform.position.z);//�X�^�[�g��ʂ��Œ�
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
                    (transform.position.x + CScrollx, transform.position.y, transform.position.z);//��ʂ��X�N���[��
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
