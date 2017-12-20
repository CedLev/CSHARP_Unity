using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    public GameObject _Manager;


    private void Awake()
    {
        DontDestroyOnLoad(_Manager);

    }
}
