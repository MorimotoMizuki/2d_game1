using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonvillage : MonoBehaviour
{
    public GameObject SpawnButtonv;
    public GameObject SpawnButtonc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.SougenBoss)
        {
            //���{�^��
            SpawnButtonv.gameObject.SetActive(true);

            if(PlayerController.VillageBoss)
            {
                //��{�^��
                SpawnButtonc.gameObject.SetActive(true);
            }

        }

    }

}
