using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;
    public GameObject thunder;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rnd = 1;

        if (collision.gameObject.tag == "Player")//Player�ɓ���������
        {
            rnd = Random.Range(1, 4);

            if (rnd == 1)
            {
                //�G�̍��W��ϐ�pos�ɕۑ�
                var pos = Point1.gameObject.transform.position;
                //�e�̃v���n�u���쐬
                var t = Instantiate(thunder) as GameObject;
                //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                t.transform.position = pos;

            }
            if (rnd == 2)
            {
                //�G�̍��W��ϐ�pos�ɕۑ�
                var pos = Point2.gameObject.transform.position;
                //�e�̃v���n�u���쐬
                var t = Instantiate(thunder) as GameObject;
                //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                t.transform.position = pos;
            }
            if (rnd == 3)
            {
                //�G�̍��W��ϐ�pos�ɕۑ�
                var pos = Point3.gameObject.transform.position;
                //�e�̃v���n�u���쐬
                var t = Instantiate(thunder) as GameObject;
                //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                t.transform.position = pos;
            }
        }
    } 
}
