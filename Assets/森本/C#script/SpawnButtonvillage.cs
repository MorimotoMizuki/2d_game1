using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButtonvillage : MonoBehaviour
{
    public GameObject SpawnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ボスが倒されたらの条件を書く
    public void OnClickStartButton()
    {
        SpawnButton.gameObject.SetActive(true);

    }

}
