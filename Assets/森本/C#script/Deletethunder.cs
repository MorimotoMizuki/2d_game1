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

   
    //��ʊO�ɏo���������
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
