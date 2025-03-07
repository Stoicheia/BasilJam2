using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cutscene
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CutsceneManager : MonoBehaviour
    {
        private PlayableDirector _director;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();
        }

        public void PlayFromStart()
        {
            _director.time = 0;
            _director.Play();
        }

        public void Pause()
        {
            _director.Pause();
        }

        public void Unpause()
        {
            _director.Play();
        }
    }
}