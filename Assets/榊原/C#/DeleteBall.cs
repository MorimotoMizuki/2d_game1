using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBall : MonoBehaviour
{
    float count = 2.0f;
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            count = 2.0f;
            Destroy(this.gameObject);
        }
    }

   

    //��ʊO�ɏo���������
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
