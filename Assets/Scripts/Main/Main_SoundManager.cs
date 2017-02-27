using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_SoundManager : MonoBehaviour
{
    static Main_SoundManager instance;

    public AudioSource shot;
    public AudioSource hit;
    public AudioSource explosion;
    public AudioSource powerup;

    static public Main_SoundManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }
}
