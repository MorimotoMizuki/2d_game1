using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBlock : MonoBehaviour
{
    public BoxCollider2D Box;
    static public bool OnOffswitch = false;
    // Start is called before the first frame update
    void Start()
    {
        Box = GetComponent<BoxCollider2D>();
        Box.enabled = true;//�A�N�e�B�u
    }

    // Update is called once per frame
    void Update()
    {
        if(OnOffswitch)
        {
            Box.enabled = false;//��A�N�e�B�u
        }
    }
}
