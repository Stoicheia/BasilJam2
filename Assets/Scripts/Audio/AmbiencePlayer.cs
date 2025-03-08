using System;
using System.Collections.Generic;
using Cams;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Audio
{
    public class AmbiencePlayer : SerializedMonoBehaviour
    {
        [SerializeField] private int _ambienceIndex;
        [SerializeField] private Dictionary<SubCamera, AmbienceCollection> _collections;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CameraSwitcher _cameraSwitcher;

        private void OnEnable()
        {
            CameraSwitcher.OnSwitchCamera += HandleSwitchCamera;
        }

        private void OnDisable()
        {
            CameraSwitcher.OnSwitchCamera -= HandleSwitchCamera;
        }

        private void Start()
        {
            Init();
        }

        [Button]
        public void Init()
        {
            _ambienceIndex = 0;
            HandleSwitchCamera(_cameraSwitcher.ActiveSubCamera);
        }
        
        [Button]
        public void NextAmbience()
        {
            _ambienceIndex++;
            HandleSwitchCamera(_cameraSwitcher.ActiveSubCamera);
        }

        public void HandleSwitchCamera(SubCamera newCamera)
        {
            AmbienceCollection collection = _collections[newCamera];
            AmbienceInfo info = collection.GetAmbience(_ambienceIndex);
            _audioSource.volume = info.Volume;
            if (!info.HasAudio)
            {
                _audioSource.Pause();
                Debug.Log("No audio");
            }
            else if (info.HasAudio && info.OneShot)
            {
                _audioSource.Pause();
                _audioSource.PlayOneShot(info.Clip);
                Debug.Log("One shot");
            }
            else if (info.HasAudio && !info.OneShot)
            {
                _audioSource.clip = info.Clip;
                _audioSource.loop = true;
                _audioSource.Play();
                Debug.Log("Normal audio");
            }
        }
    }
}