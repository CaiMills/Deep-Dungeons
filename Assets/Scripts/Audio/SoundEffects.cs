using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [Serializable]

    public class soundEffect
    {
        public AudioSource soundFile;
        public string name;
        public float volume;
    }
}
