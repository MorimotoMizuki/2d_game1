using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    ParticleSystem particlehosi;

    // Start is called before the first frame update
    void Start()
    {
        particlehosi = this.GetComponent<ParticleSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //if(particlehosi.isStopped)//�p�[�e�B�N�����I������������
        //{
        //    Destroy(this.gameObject);//�p�[�e�B�N���p�Q�[���I�u�W�F�N�g���폜
        //}
    }
}
