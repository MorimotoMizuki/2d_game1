using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCamera : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("��l��");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;//player�̃|�W�V�������擾
        transform.position = new Vector3
                 (playerPos.x, transform.position.y, transform.position.z);//player�ɒǏ]
    }
}
