﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public AudioClip clip;
    public string name;
    public bool loop;
    [Range(0,1f)]
    public float volume;
    //[Range(.1f,3f)] 
    //public float pitch=1;
    [HideInInspector]
    public AudioSource source;

    

}