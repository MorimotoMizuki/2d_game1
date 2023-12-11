using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeSpeed;

    public void OnClickStartButton()
    {
        PlayerController.pose = false;//�t���O���낷
        TimeCounter.isPose = false;//�t���O���낷
        Initiate.Fade(sceneName, fadeColor, fadeSpeed);
        //SceneManager.LoadScene("Title");
    }
}
