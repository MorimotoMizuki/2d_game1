using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletethunder : MonoBehaviour
{
    // �����x
    [SerializeField] private Vector3 _acceleration;

    // �����x
    [SerializeField] private Vector3 _initialVelocity;

    // ���ݑ��x
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        // �����x�ŏ�����
        _velocity = _initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        // �����x�̎��Ԑϕ����瑬�x�����߂�
        _velocity += _acceleration * Time.deltaTime;

        // ���x�̎��Ԑϕ�����ʒu�����߂�
        transform.position += _velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//Player�ɓ���������
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")//ground�ɓ���������
        {
            Destroy(this.gameObject);
        }
    }
}
