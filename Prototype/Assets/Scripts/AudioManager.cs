using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
