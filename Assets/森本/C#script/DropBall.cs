using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropBall : MonoBehaviour
{
    public GameObject ball;
    private float count = 1.0f;
    private int vecX;
    public int hp = 100;
    public float reactionDistance = 4.0f;//��������

    private int Torent_Hp;

    private int rushdamage = 10;
    private bool inDamage = false;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Torent_Hp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState != "playing")
        {
            return;
        }
        //Player�@�̃Q�[���I�u�W�F�N�g�𓾂�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive && Torent_Hp > 0)
            {
                // PLAYER�̈ʒu���擾
                Vector2 targetPos = player.transform.position;
                // PLAYER��x���W
                float x = targetPos.x;
                // ENEMY�́A�n�ʂ��ړ�������̂ō��W�͏��0�Ƃ���
                float y = 0;
                // �ړ����v�Z�����邽�߂̂Q�����̃x�N�g�������
                Vector2 direction = new Vector2(
                    x - transform.position.x, y).normalized;
                ////1�b�o���Ƃɒe�𔭎�
                //currentTime += Time.deltaTime;

                //if (targetTime < currentTime)
                //{
                //    currentTime = 0;
                //    //�G�̍��W��ϐ�pos�ɕۑ�
                //    var pos = this.gameObject.transform.position;

                //    //�e�̃v���n�u���쐬
                //    var t = Instantiate(tama) as GameObject;

                //    //�e�̃v���n�u�̈ʒu��G�̈ʒu�ɂ���
                //    t.transform.position = pos;

                //    //�G����v���C���[�Ɍ������x�N�g�������
                //    //�v���C���[�̈ʒu����G�̈ʒu(�e�̈ʒu)������
                //    Vector2 vec = player.transform.position - pos;

                //    //�e��RigidBody2D�R���|�l���g��velocity�ɂ��������߂��x�N�g�������ė͂�������
                //    t.GetComponent<Rigidbody2D>().velocity = vec;
                //}
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
        count -= Time.deltaTime;
        if(count <= 0)
        {
            vecX = Random.Range(210, 229);

            Instantiate(ball, new Vector3(vecX, -4.3f, 0), Quaternion.identity);

            count = 1.0f;
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


       
    
    private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("OntriggerEnter2D:" + other.gameObject.name);

    //�ːi�U���Ƃ̐ڐG
    if (other.gameObject.tag == "rushWall")
    {
        //�_���[�W
        Torent_Hp -= rushdamage;
        Debug.Log(Torent_Hp);
        inDamage = true;
    }
    EnemyDamage();//�|��Ă��邩���ׂ�
}

void EnemyDamage()
{
    Invoke("DamageEnd", 0.25f);
    if (Torent_Hp <= 0)
    {
        Debug.Log("�G���|��Ă���");
            PlayerController.gameState = ("gameclear");

                Debug.Log("�Q�[���N���A");
                SceneManager.LoadScene("GameClear");
            Destroy(gameObject, 0.2f);//0.2�����ēG������
          
        }
}
void DamageEnd()
{
    //�_���[�W�t���OOFF
    inDamage = false;
    //�X�v���C�g���ɖ߂�
    gameObject.GetComponent<SpriteRenderer>().enabled = true;
}
}




