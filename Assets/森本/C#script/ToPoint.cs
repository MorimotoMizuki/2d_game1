using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPoint : MonoBehaviour
{
    public GameObject Player;
    public GameObject Point;

    //���{�^������������
    public void OnClickStartButton()
    {
        Player.transform.position = Point.transform.position;
    }

}
