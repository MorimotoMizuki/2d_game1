using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private float playerSpeed;
    public int hp;

    private float jumpForce = 350f;
    private int jumpCount = 0;

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        // ���L�[���������獶�����֐i��
        if (Input.GetKey(KeyCode.LeftArrow)) playerSpeed = -speed;
        // �E�L�[����������E�����֐i��
        else if (Input.GetKey(KeyCode.RightArrow)) playerSpeed = speed;
        // ���������Ȃ�������~�܂�
        else playerSpeed = 0;

        rigidbody2D.velocity = new Vector2(playerSpeed, rigidbody2D.velocity.y);

        //if (Input.GetMouseButtonDown(0) && this.jumpCount < 1)
        //{
        //    this.rbody2D.AddForce(transform.up * jumpForce);
        //    jumpCount++;
        //}
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Floor"))
    //    {
    //        jumpCount = 0;
    //    }
    //}
}