using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoburin : MonoBehaviour
{

    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("��ʊO�ł��s������")] public bool nonVisibleAct;
    [Header("�ڐG����")] public EnemyCollisionCheck checkCollision;
    #endregion

    #region//�v���C�x�[�g�ϐ�
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;

    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;

    int xVector;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                  rightTleftF = true;
                xVector = 1;
            }
                else if(checkCollision.isOn == false)
                {
                     rightTleftF = false;
                 xVector = -1;
            }
               
                if (rightTleftF)
                {
                    
                    transform.localScale = new Vector3(-7, 7, 1);
                }
                else
                {
                    transform.localScale = new Vector3(7, 7, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
            }
    }
}

