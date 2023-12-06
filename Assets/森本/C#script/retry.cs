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
        if (Sougenretry == false)
        {
            MuraretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(false);
            SougenretryButton.gameObject.SetActive(true);

            Sougenretry = true;
        }
        //���X�e�[�W��Gameover
        if (Muraretry == false)
        {
            SougenretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(false);
            MuraretryButton.gameObject.SetActive(true);

            Muraretry = true;
        }
        //��X�e�[�W��Gameover
        if (SiroretryButton == false)
        {
            SougenretryButton.gameObject.SetActive(false);
            MuraretryButton.gameObject.SetActive(false);
            SiroretryButton.gameObject.SetActive(true);

            Siroretry = true;
        }
    }
}
