using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRule : MonoBehaviour
{
    public GameObject rule;
    bool Text = true;
    bool one;

    // Start is called before the first frame update
    void Start()
    {
        Text = true;
        one = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Text)
        {
            if (collision.gameObject.tag == "Player")//Player�ɓ���������
            {
                rule.gameObject.SetActive(true);
                one = true;
            }
        }
        if(one)
        {
            Text = false;
        }
    }    
}
