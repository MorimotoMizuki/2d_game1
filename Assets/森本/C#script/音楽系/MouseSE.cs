using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSE : MonoBehaviour
{
    [SerializeField]
    AudioSource MouseAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        MouseAudioSource = GetComponent<AudioSource>();
        MouseAudioSource.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.SougenBoss)
        {
            //���{�^��

            MouseAudioSource.mute = !MouseAudioSource.mute;

            if (PlayerController.VillageBoss)
            {
                //��{�^��
                MouseAudioSource.mute = !MouseAudioSource.mute;
            }

        }

    }

}
