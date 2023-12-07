using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retry : MonoBehaviour
{

    static public bool Sougenretry;
    static public bool Muraretry;
    static public bool Siroretry;
    public GameObject SougenretryButton;
    public GameObject MuraretryButton;
    public GameObject SiroretryButton;

    // Start is called before the first frame update
    void Start()
    {
        Sougenretry = PlayerController.SougenBoss;
        Muraretry = PlayerController.VillageBoss;
        Siroretry = PlayerController.CastleBoss;
    }

    // Update is called once per frame
    void Update()
    {
        //�����X�e�[�W��Gameover
        if (Sougenretry)
        {
            SougenretryButton.gameObject.SetActive(false);
            MuraretryButton.gameObject.SetActive(true);

            Sougenretry = false;
        }
        //���X�e�[�W��Gameover
        if (Muraretry)
        {
            MuraretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(true);

            Muraretry = false;
        }
        //��X�e�[�W��Gameover
        //if (SiroretryButton == false)
        //{
        //    Siroretry = true;
        //}
    }
}
