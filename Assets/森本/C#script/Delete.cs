using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>();
    }

    //�v���C���[��tama�̃^�O���������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tama"))
        {
            Destroy(this.gameObject);
        }

    }
}

