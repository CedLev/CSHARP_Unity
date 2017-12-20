using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _Instance;

    private static AudioManager Instance
    {
        get { return _Instance; }
    }

    public AudioClip _MusicMenu;
    public AudioClip _MusicGame;

    public AudioSource _Audiosource;


    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else Destroy(gameObject);
    }




    void Update()
    {

    }
}
