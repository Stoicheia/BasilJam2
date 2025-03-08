using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct AmbienceInfo
    {
        public int Index;
        public bool HasAudio => Clip != null;
        public bool OneShot;
        public AudioClip Clip;
        [Range(0, 1)] public float Volume;
    }
}