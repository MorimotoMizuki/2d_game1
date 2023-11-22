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
    public GameObject DangerArea1;
    public GameObject DangerArea2;
    public GameObject DangerArea3;


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
                //�댯�G���A�\��
                DangerArea1.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Thunder1());
            }
            if (rnd == 2)
            {
                //�댯�G���A�\��
                DangerArea2.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Thunder2());
            }
            if (rnd == 3)
            {   //�댯�G���A�\��
                DangerArea3.gameObject.SetActive(true);
                //�R���[�`���Ăяo��
                StartCoroutine(Thunder3());
            }
        }
    } 
    private IEnumerator Thunder1()
    {
        yield return new WaitForSeconds(3.0f);

        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point1.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea1.gameObject.SetActive(false);

    }
    private IEnumerator Thunder2()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point2.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea2.gameObject.SetActive(false);
    }
    private IEnumerator Thunder3()
    {
        yield return new WaitForSeconds(3.0f);
        //Point1�̍��W��ϐ�pos�ɕۑ�
        var pos = Point3.gameObject.transform.position;
        //�e�̃v���n�u���쐬
        var t = Instantiate(thunder) as GameObject;
        //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
        t.transform.position = pos;
        //�댯�G���A��\��
        DangerArea3.gameObject.SetActive(false);
    }

}
