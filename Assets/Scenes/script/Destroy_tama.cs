using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_tama : MonoBehaviour
{
    float count = 3.0f;
    private void Update()
    {
        count -= Time.deltaTime;
        if(count <=0 )
        {
            count = 3.0f;
            Destroy(this.gameObject);
        }
    }
    //�v���C���[��tama�̃^�O���������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("tama"))
        {
            Destroy(collision.gameObject);
        }
        
    }

    //��ʊO�ɏo���������
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
