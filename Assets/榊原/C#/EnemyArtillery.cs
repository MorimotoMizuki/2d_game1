using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArtillery : MonoBehaviour
{
    public GameObject tama;
    //�v���C���[�I�u�W�F�N�g
    public GameObject player;

    private float Count_artillery = 0;
    private float artillery = 3.0f;

    private bool inDamage = false;
    private bool isActive = false;
    private bool move = true;

    private float reactionDistance = 10.0f;//��������

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.gameState != "playing")
        {
            return;
        }
        if (player != null)
        {
            if (isActive && move)
            {
                //��l���̍��W��ϐ�pos�ɕۑ�
                var posL = this.gameObject.transform.position + transform.right * 1.5f + transform.up * 1.5f;
                //�e�̃v���n�u���쐬
                Count_artillery += Time.deltaTime;
                if (Count_artillery > artillery)
                {
                    var t = Instantiate(tama) as GameObject;
                    //�e�̃v���n�u�̈ʒu���ʒu�ɂ���
                    t.transform.position = posL;
                    t.AddComponent<LeftArtillery>();
                    Count_artillery = 0;
                    Debug.Log("��C�̋ʍ쐬");
                }
            }
            else
            {
                //�v���C���[�Ƃ̋��������߂�
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true; //�A�N�e�B�u�ɂ���
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
        }
        if (inDamage)
        {
            //�_���[�W���_�ł�����
            float val = Mathf.Sin(Time.time * 50);
            // Debug.Log(val);
            if (val > 0)
            {
                //�X�v���C�g��\��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�X�v���C�g���\��
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return;//�_���[�W���͑���ɂ��ړ��͂����Ȃ�
        }
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("OntriggerEnter2D:" + collider2D.gameObject.name);

        //�ːi�U���Ƃ̐ڐG
        if (collider2D.gameObject.tag == "rushWall")
        {
            //�_���[�W
            inDamage = true;
            move = false;
            EnemyDamage();//�|��Ă��邩���ׂ�
        }
        //�΋��U���Ƃ̐ڐG
        if(collider2D.gameObject.tag == "Fireball")
        {
            //�_���[�W
            inDamage = true;
            move = false;
            EnemyDamage();//�|��Ă��邩���ׂ�
        }
    }

    void EnemyDamage()
    {
        Invoke("DamageEnd", 0.25f);
            Debug.Log("�G���|��Ă���");
            Destroy(gameObject, 0.2f);//0.2�����ēG������
    }
    void DamageEnd()
    {
        //�_���[�W�t���OOFF
        inDamage = false;
        //�X�v���C�g���ɖ߂�
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
