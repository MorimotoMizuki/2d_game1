using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraMap3 : MonoBehaviour
{
    public BoxCollider2D bxleft;
    public BoxCollider2D bxup;
    GameObject player;
    //��ʐ���p
    private int CPosleftx = -20;
    private float CPosright = 217.0f;
    private int PPosleftx = 0;
    private bool Oncamera = false;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("��l��");
        bxleft = GetComponent<BoxCollider2D>();
        bxleft.enabled = false;
        bxup = GetComponent<BoxCollider2D>();
        bxup.enabled = false;
        Oncamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//player�̃|�W�V�������擾

        if (playerPos.x > PPosleftx || playerPos.y > 8 || playerPos.x >217)
        {
            transform.position = new Vector3
           (playerPos.x, playerPos.y + 3.5f, transform.position.z);//player�ɒǏ]
            Debug.Log("false3");
            Oncamera = false;
        }
        //else if (transform.position.x > CPosright)
        //{
        //    transform.position = new Vector3
        //   (transform.position.x, transform.position.y, transform.position.z);//�{�X��ʂ��Œ�
        //    bxleft.enabled = true;
        //    Debug.Log("false2");
        //}
        else
        {
           transform.position = new Vector3
           (transform.position.x, 0, transform.position.z);//��ʂ��Œ�
           
        }


        this.player = GameObject.Find("��l��");
    }
}
